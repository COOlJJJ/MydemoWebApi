using demoApi.Common.Helper;
using demoApi.Log;
using demoApi.Model;
using demoApi.Services;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;
using static demoApi.Filter.GlobalExceptionFilter;

namespace demoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //log4net
            Repository = LogManager.CreateRepository("");//需要获取日志的仓库名，也就是你的当前项目名

            //指定配置文件
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));//配置文件
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository Repository { get; set; }

        public string ApiName { get; set; } = "MyDemoApi.Core";
        private readonly string apiVersionName = "V1";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //注入全局异常捕获
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            //log日志注入
            services.AddSingleton<ILoggerHelper, LoggerHelper>();
            services.AddSingleton(new Appsettings(Configuration));
            services.AddSingleton(new UnityOfServices());
            services.AddControllers();
            //跨域配置
            services.AddCors(options => { options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()); });
          
            #region 认证配置
            //策略配置
            services.AddAuthorization(options =>
            {
                //参考
                //options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());//单独角色
                //options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                //options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));//或的关系
                //options.AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));//且的关系

                options.AddPolicy("TestorAdmin", policy => policy.RequireRole("admin"));

            });
            //认证配置
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(option =>
                    {
                        //RequireHttpsMetadata: 限定认证操作是否必须通过https来做
                        option.RequireHttpsMetadata = false;
                        option.SaveToken = true;
                        var token = Configuration.GetSection("tokenParameter").Get<tokenParameter>();

                        option.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,//是否验证失效时间
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                            ValidIssuer = token.Issuer,
                            ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                        };
                    });
            #endregion

            #region Swagger
            var basePath = ApplicationEnvironment.ApplicationBasePath;
            //注册Swagger服务
            services.AddSwaggerGen(c =>
            {
                //添加文本信息
                c.SwaggerDoc(apiVersionName, new OpenApiInfo()
                {
                    Version = apiVersionName,//版本号
                    Title = $"{ApiName}接口文档----Netcore 3.1",//标题
                    Description = $"{ApiName}框架说明文档",//描述
                    Contact = new OpenApiContact() { Url = new System.Uri("http://www.baidu.com"), Name = "Test", Email = "Swagger.Test@xxx.com" }
                });
                //就是这里！！！！！！！！！
                var xmlPath = Path.Combine(basePath, "demoApi.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                        new OpenApiSecurityScheme
                        {
                                Reference = new OpenApiReference
                                {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                }
                        },
                        new string[] {}
                     }
        });
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");//

            #region Swagger
            //中间件 将Api以Json格式Response 路由结点/swagger/v1/swagger.json
            app.UseSwagger();
            //中间件 将Api以html形式Response
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{apiVersionName}/swagger.json", $"{apiVersionName}doc");
                //要在应用的根(http://localhost:<port>/) 处提供 Swagger UI，将 RoutePrefix 属性设置为空字符串
                c.RoutePrefix = "";
                //c.HeadContent = "";配置头部内容
                //c.IndexStream 配置自定义的静态页面文件
            });
            #endregion

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
