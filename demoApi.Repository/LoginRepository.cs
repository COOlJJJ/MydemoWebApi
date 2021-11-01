using Dapper;
using demoApi.Common.Helper;
using demoApi.Model;
using demoApi.Model.Login;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace demoApi.Repository
{
    /// <summary>
    /// 登录仓储层
    /// </summary>
    public class LoginRepository
    {

        private string connStr = Appsettings.app("SqlConnection", "SqlStr");
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public async Task<PwdAndRole_Model> GetPasswordAndRole(string username)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                var model = await connection.QueryFirstOrDefaultAsync<PwdAndRole_Model>($"select ID,Password,is_admin from [User] where Name ='{username}' and status=1");
                if (model == null)
                {
                    return new PwdAndRole_Model
                    {
                        Password = string.Empty,
                        Message = "该用户不存在"
                    };
                }
                else
                {
                    if (!model.is_admin.Equals(1))
                    {
                        var roleName = await connection.QueryFirstOrDefaultAsync<string>($"select c.Name from (User_Role as a inner join [User] as b on a.Uid=b.ID) right join[Role] as c on a.role_id=c.id where a.status=1 and b.Name='{username}'");
                        model.RoleName = roleName;

                    }
                    else
                    {
                        model.RoleName = "admin";
                    }
                }
                return model;
            }
        }
        /// <summary>
        /// 获取Menus
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Permission_Model>> GetMenus(string roleName)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {

                //roleName为admin 则为管理员全部权限
                if (roleName == "admin")
                {
                    IEnumerable<Permission_Model> model = await connection.QueryAsync<Permission_Model>("select * from Access");
                    return model;
                }
                else
                {
                    int roleId = await connection.QueryFirstOrDefaultAsync<int>($@"SELECT Id FROM [Role] where status=1 and Name='{roleName}'");

                    IEnumerable<Permission_Model> prermission_Model = await connection.QueryAsync<Permission_Model>(@$"select b.Id,b.authName,b.NodeLevel,b.UpId,b.path from Role_Access as a  join Access as b on 
                                                 a.Access_Id=b.Id where a.Role_Id={roleId} and a.status=1");
                    return prermission_Model;
                }



            }
        }

    }
}
