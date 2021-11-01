using demoApi.Common.Helper;
using demoApi.Model;
using demoApi.Model.Login;
using demoApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demoApi.Controllers
{

    /// <summary>
    /// 登陆模块
    /// </summary>
    [AllowAnonymous]
    [Route("api/Login")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        private readonly UnityOfServices _unityOfServices;

        public LoginController(UnityOfServices unityOfServices)
        {
            _unityOfServices = unityOfServices;

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<MessageModel<TokenModel>> LoginAsync([FromBody] Login_Model login_Model)
        {

            if (!string.IsNullOrWhiteSpace(login_Model.Username) && !string.IsNullOrWhiteSpace(login_Model.Password))
            {
                PwdAndRole_Model pwdAndRole_Model = await _unityOfServices._loginServices.Login(login_Model);

                //string.Compare相等为0 不相等为-1
                if (string.Compare(pwdAndRole_Model.Message, "登录成功").Equals(0))
                {

                    string token = JwtHelper.IssueJwt(new TokenModelJwt
                    {
                        Role = pwdAndRole_Model.RoleName
                    });

                    return new MessageModel<TokenModel>
                    {
                        status = 200,
                        msg = "登陆成功",
                        response = new TokenModel
                        {
                            Token = token,
                        }
                    };
                }
                else
                {

                    return new MessageModel<TokenModel>
                    {
                        status = 201,
                        msg = "登陆失败",
                    };
                }
            }
            else
            {

                return new MessageModel<TokenModel> { status = 201, msg = "用户名或密码不能为空", response = null };
            }

        }
       
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Menus")]
        public async Task<MessageModel<List<Menus_Model>>> GetMenus()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            string roleName = JwtHelper.SerializeJwt(token).Role;
            return new MessageModel<List<Menus_Model>>
            {
                msg = "获取菜单列表成功",
                status = 200,
                response = await _unityOfServices._loginServices.GetMenus(roleName)
            };
        }


        public  class TokenModel
        {
            public string Token { get; set; }
            public string UserId { get; set; }
        }

    }
}
