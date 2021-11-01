using Dapper;
using demoApi.Common.Helper;
using demoApi.Model;
using demoApi.Model.Rights;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace demoApi.Repository
{
    public class RightsRepository
    {
        private string connStr = Appsettings.app("SqlConnection", "SqlStr");

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permission_Model> GetRightsList()
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                IEnumerable<Permission_Model> permissions_list = connection.Query<Permission_Model>("select Id,authName,NodeLevel,UpId,path from [Access] order by NodeLevel");
                return permissions_list;
            }
        }

        /// <summary>
        /// 添加新的权限
        /// </summary>
        /// <param name="permission_Model"></param>
        /// <returns></returns>
        public async Task<int> AddNewRights(Permission_Model permission_Model)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                string sql = "insert into [Access](authName,NodeLevel,UpId,path) values(@authName,@NodeLevel,@UpId,@path)";
                int result = await connection.ExecuteAsync(sql, new
                {
                    authName = permission_Model.authName,
                    NodeLevel = permission_Model.NodeLevel,
                    UpId = permission_Model.UpId,
                    path = permission_Model.path
                });
                int accessid = await connection.QueryFirstOrDefaultAsync<int>($"select Id from Access where authName='{permission_Model.authName}'");
                //新增权限后给每个角色也添加该权限但是状态为0
                IEnumerable<Role_Model> role_s = connection.Query<Role_Model>("select Id from Role where status=1");
                foreach (var item in role_s)
                {
                    result = await connection.ExecuteAsync($"insert into Role_Access(Role_Id,Access_Id,status) values({item.Id},{accessid},0)");
                }
                return result;
            }
        }

        /// <summary>
        /// 添加新角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<int> AddNewRole(string name)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                //判断该角色之前是否已经添加过
                int m = connection.QueryFirstOrDefault<int>($"select Id from [Role] where Name='{name}'");
                if (m == 0)
                {
                    await connection.ExecuteAsync($"insert into Role (Name,status) values('{name}',1)");
                    IEnumerable<Permission_Model> models = connection.Query<Permission_Model>("select Id,authName from Access");
                    int id = await connection.QueryFirstOrDefaultAsync<int>($"select Id from Role where Name='{name}'");
                    //将每个权限分配给当前角色但默认是无权限
                    foreach (var item in models)
                    {
                        int i = await connection.ExecuteAsync($"insert into Role_Access (Role_Id,Access_Id,status) values({id},{item.Id},0)");
                    }
                }
                else
                {
                    //恢复角色 
                    await connection.ExecuteAsync($"update  Role set status=1 where Name='{name}'");
                }


                return 1;
            }
        }

        /// <summary>
        /// 删除角色 将该角色status置为0 权限角色表都为0
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public async Task<int> DeleteRole(int roleID)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                await connection.ExecuteAsync($"update Role set status=0 where Id={roleID}");
                await connection.ExecuteAsync($"update [User_Role] set status=0 where Role_Id={roleID}");
                int j = await connection.ExecuteAsync($"update Role_Access set status=0 where Role_Id={roleID}");
                return j;
            }
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public async Task<int> GiveRights(int roleid, string[] listId)
        {
            int result = 0;
            using (IDbConnection connection = new SqlConnection(connStr))
            {

                foreach (var item in listId)
                {
                    result = await connection.ExecuteAsync($"update Role_Access set status=1  where Role_Id = {roleid} and [Access_Id]={item} ");
                }
                var arr = connection.Query<string>("select Id from Access").AsList();
                List<string> listIds = new List<string>();
                for (int i = 0; i < arr.Count(); i++)
                {
                    listIds.Add(arr[i]);
                }
                var noRightsId = listIds.Except(listId);
                foreach (var item in noRightsId)
                {
                    result = await connection.ExecuteAsync($"update Role_Access set status= 0  where Role_Id = {roleid} and [Access_Id]={item} ");
                }
                return result;
            }
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PermissionList_Model>> GetRightsTree()
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                IEnumerable<PermissionList_Model> permission_Model_s = await connection.QueryAsync<PermissionList_Model>("select * from Access ");
                IEnumerable<PermissionList_Model> permission_Models = await connection.QueryAsync<PermissionList_Model>("select * from Access where NodeLevel = 1");
                foreach (var item in permission_Models)
                {
                    List<PermissionList_Model> permissionLists = permission_Model_s.Where(x => x.UpId == item.Id && x.NodeLevel == 2).AsList();
                    foreach (var item1 in permissionLists)
                    {
                        List<PermissionList_Model> permissionList_s = permission_Model_s.Where(x => x.UpId == item1.Id && x.NodeLevel == 3).AsList();
                        item1.Children = permissionList_s;
                    }
                    item.Children = permissionLists;

                }
                return permission_Models;
            }
        }

        /// <summary>
        /// 编辑角色名字
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<int> EditRoleName(int roleid, string name)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                string sql = "update Role set Name=@Name where Id=@Roleid ";
                int result = await connection.ExecuteAsync(sql, new
                {
                    Name = name,
                    Roleid = roleid
                });
                return result;
            }
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        public IEnumerable<RolesList_Model> GetRolesList()
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {

                IEnumerable<RolesList_Model> rolesList = connection.Query<RolesList_Model>("select Id,Name from Role where status=1");
                foreach (var item in rolesList)
                {
                    IEnumerable<PermissionList_Model> permissionList = connection.Query<PermissionList_Model>($"select b.Id,b.authName,b.path,b.UpId from Role_Access as a inner join Access as b on a.Access_Id = b.Id where Role_Id = {item.Id} and b.NodeLevel = 1 and status=1");
                    foreach (var item1 in permissionList)
                    {
                        IEnumerable<PermissionList_Model> permissionLists = connection.Query<PermissionList_Model>($"select b.Id,b.authName,b.path,b.UpId from Role_Access as a inner join Access as b on a.Access_Id = b.Id where Role_Id = {item.Id} and b.NodeLevel = 2 and status=1").Where(x => x.UpId == item1.Id);
                        foreach (var item2 in permissionLists)
                        {
                            IEnumerable<PermissionList_Model> permissionList_s = connection.Query<PermissionList_Model>($"select b.Id,b.authName,b.path,b.UpId from Role_Access as a inner join Access as b on a.Access_Id = b.Id where Role_Id = {item.Id} and b.NodeLevel = 3 and status=1").Where(x => x.UpId == item2.Id);
                            item2.Children = permissionList_s.AsList();
                        }
                        item1.Children = permissionLists.AsList();
                    }
                    item.Children = permissionList.AsList();
                }
                return rolesList;
            }

        }

        /// <summary>
        /// 根据角色Id去匹配权限获取路径
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public IEnumerable<Permission_Model> MatchPermission(string roleName)
        {
            using (IDbConnection connection = new SqlConnection(connStr))
            {
                int roleId = connection.QueryFirstOrDefault<int>($"select Id from [Role] where Name='{roleName}'");
                IEnumerable<Permission_Model> permissions = connection.Query<Permission_Model>(@$"select a.path from Access as a join Role_Access as b on a.Id = b.Access_Id
                                                                                                        where b.Role_Id = {roleId} and b.status=1 and a.path like '%api%'");
                return permissions;
            }
        }
    }
}
