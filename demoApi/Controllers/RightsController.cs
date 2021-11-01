using Dapper;
using demoApi.Common.Helper;
using demoApi.Model;
using demoApi.Model.Rights;
using demoApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demoApi.Controllers
{
    /// <summary>
    /// 权限模块
    /// </summary>
    [Route("api/Rights")]
    [ApiController]
    [Authorize(Policy = "TestorAdmin")]
    public class RightsController : ControllerBase
    {

        private readonly UnityOfServices _unityOfServices;

        public RightsController(UnityOfServices unityOfServices)
        {
            _unityOfServices = unityOfServices;
        }


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRightsList")]
        public MessageModel<List<Permission_Model>> GetRightsList()
        {
            List<Permission_Model> permission_list = _unityOfServices._rightsServices.GetRightsList().AsList<Permission_Model>();
            return new MessageModel<List<Permission_Model>>
            {
                msg = "获取权限列表成功",
                status = 200,
                response = permission_list
            };
        }

        /// <summary>
        /// 添加新权限
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddNewRights")]
        public async Task<MessageModel<string>> AddNewRights(dynamic obj)
        {
            Permission_Model permission_Model = new Permission_Model();
            //3.1 dynamic无法自动序列化
            dynamic objdyn = JsonConvert.DeserializeObject(Convert.ToString(obj));
            permission_Model.authName = objdyn.authName;
            permission_Model.NodeLevel = objdyn.NodeLevel;
            permission_Model.path = objdyn.path;
            permission_Model.UpId = objdyn.UpId;
            int result = await _unityOfServices._rightsServices.AddNewRights(permission_Model);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "添加成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "添加失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRolesList")]
        public MessageModel<List<RolesList_Model>> GetRolesList()
        {
            List<RolesList_Model> rolesList_s = _unityOfServices._rightsServices.GetRolesList().AsList();
            return new MessageModel<List<RolesList_Model>>
            {
                msg = "获取成功",
                status = 200,
                response = rolesList_s

            };

        }

        /// <summary>
        /// 编辑角色名字
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("EditRoleName")]
        public async Task<MessageModel<string>> EditRoleName(int roleid, string name)
        {
            int result = await _unityOfServices._rightsServices.EditRoleName(roleid, name);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "添加成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "添加失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRightsTree")]
        public async Task<MessageModel<List<PermissionList_Model>>> GetRightsTree()
        {
            IEnumerable<PermissionList_Model> permissionList_Models = await _unityOfServices._rightsServices.GetRightsTree();
            return new MessageModel<List<PermissionList_Model>>
            {
                msg = "获取成功",
                status = 200,
                response = permissionList_Models.AsList()
            };
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="permissionid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GiveRights")]
        public async Task<MessageModel<string>> GiveRights(int roleid, string permissionid)
        {
            string[] permissionidlist = permissionid.Split(',');
            int result = await _unityOfServices._rightsServices.GiveRights(roleid, permissionidlist);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "编辑权限成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "编辑权限失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 添加新角色 默认无权限 进行配置
        /// </summary>
        /// <param name="name">角色名</param>
        /// <returns></returns>
        [HttpGet]
        [Route("AddNewRole")]
        public async Task<MessageModel<string>> AddNewRole(string name)
        {
            int result = await _unityOfServices._rightsServices.AddNewRole(name);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "添加角色成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "添加角色失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteRole")]
        public async Task<MessageModel<string>> DeleteRole(int roleID)
        {
            int result = await _unityOfServices._rightsServices.DeleteRole(roleID);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "删除角色成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "删除角色失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 检验三级权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("CheckPermission")]
        public MessageModel<List<bool>> CheckPermission(permissionModel model)
        {
            List<string> Rightslist = new List<string>();

            List<bool> returnarr = new List<bool>();
            //token
            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            string roleName = JwtHelper.SerializeJwt(token).Role;
            //管理员不卡关
            if (roleName != "admin")
            {

                List<Permission_Model> permission_s = _unityOfServices._rightsServices.MatchPermission(roleName).AsList();
                for (int i = 0; i < permission_s.Count; i++)
                {
                    Rightslist.Add(permission_s[i].path);
                }
                for (int j = 0; j < Rightslist.Count; j++)
                {
                    if (!Rightslist.Contains(model.arr[j]))
                    {
                        returnarr.Add(false);
                    }
                    else
                    {
                        returnarr.Add(true);
                    }

                }

            }
            else
            {
                for (int j = 0; j < model.arr.Count; j++)
                {
                    returnarr.Add(true);
                }
            }

            return new MessageModel<List<bool>>
            {
                msg = "获取成功",
                status = 200,
                response = returnarr
            };
        }

        public class permissionModel
        {
            public List<string> arr { get; set; }

        }
    }
}
