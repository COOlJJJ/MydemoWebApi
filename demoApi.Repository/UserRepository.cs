using Dapper;
using demoApi.Common.Helper;
using demoApi.Model.User;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace demoApi.Repository
{
    public class UserRepository
    {
        private string connStr = Appsettings.app("SqlConnection", "SqlStr");
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns></returns>
        public IEnumerable<UserInfo_Model> GetUserInfo(string query)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                string strSql = string.Format("select ID, Name, is_admin, status, email, mobile_phone from [User]");
                if (!string.IsNullOrWhiteSpace(query))
                {
                    strSql += $" where Name like '%{query}%' or email like '%{query}%' or mobile_phone like '%{query}%'";
                }
                var userlist = connection.Query<UserInfo_Model>(strSql);
                foreach (var item in userlist)
                {
                    if (item.is_admin.Equals(1))
                    {
                        item.role = "admin";
                    }
                    else
                    {
                        var role = connection.Query<string>($@"select a.Name  from [Role] as a inner join 
                                                    [User_Role] as b on b.role_id=a.Id where b.Uid={item.ID}").FirstOrDefault();
                        item.role = role;
                    }
                }
                return userlist;
            }
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="editUser_Model"></param>
        /// <returns></returns>
        public int EditUser(EditUser_Model editUser_Model)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                string sqlCommandText = @"Update [User] set Name=@Name,email=@email,mobile_phone=@phone where ID=@ID";

                int result = connection.Execute(sqlCommandText, new
                {
                    Name = editUser_Model.username,
                    email = editUser_Model.email,
                    phone = editUser_Model.mobile,
                    ID = editUser_Model.userid
                });
                return result;
            }
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public List<RoleList_Model> GetRoleList()
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                List<RoleList_Model> roleList_s = new List<RoleList_Model>();
                IEnumerable<RoleInfo_Model> roleInfos = connection.Query<RoleInfo_Model>("select Id,Name from [Role] where status=1");
                RoleList_Model Model = new RoleList_Model();
                Model.label = "admin";
                Model.value = 0;
                roleList_s.Add(Model);
                foreach (var item in roleInfos)
                {
                    RoleList_Model _Model = new RoleList_Model();
                    _Model.label = item.Name;
                    _Model.value = item.Id;
                    roleList_s.Add(_Model);
                }
                return roleList_s;
            }
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="role_Model"></param>
        /// <returns></returns>
        public int GiveRole(GiveRole_Model role_Model)
        {
            using IDbConnection connection = new SqlConnection(connStr);
            int result = 0;
            string sqlCommandText = @"Update [User_Role] set role_id=@roleid,status=1 where  Uid=@uid";
            if (role_Model.newrole.Equals(0))
            {
                result = connection.Execute(@$"Update [User] set is_admin=1 where  ID={role_Model.userid}");
            }
            else
            {
                int m = connection.QueryFirstOrDefault<int>(@$"select Id from User_Role where Uid={role_Model.userid}");
                if (m == 0)
                {
                    //如果不是管理员的话 需要先插入一笔,已经插入过一笔就不需要了
                    connection.Execute(@$"insert into User_Role (Uid,role_id,status) values({role_Model.userid},0,0)");
                }
                connection.Execute(@$"Update [User] set is_admin=0 where ID={role_Model.userid}");
                result = connection.Execute(sqlCommandText, new
                {
                    roleid = role_Model.newrole,
                    uid = role_Model.userid
                });
            }
            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                int result = connection.Execute(@$"Delete from [User] where ID={id}");
                return result;
            }

        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user_Model"></param>
        /// <returns></returns>
        public int AddUser(AddUser_Model user_Model)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                string sqlCommandText = @"insert into [User](Name,Password,is_admin,status,email,mobile_phone)
                                          values (@Name,@Password,@is_admin,@status,@email,@mobile_phone)";
                int result = connection.Execute(sqlCommandText, new
                {
                    Name = user_Model.username,
                    Password = user_Model.password,
                    is_admin = 0,
                    status = "1",
                    email = user_Model.email,
                    mobile_phone = user_Model.mobile
                });
                return result;
            }
        }

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ChangeStatus(int userid, string status)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                int j = connection.Execute($"update [User] set status='{status}' where ID={userid}");
                return j;
            }
        }
    }
}

