using demoApi.Common.Helper;
using demoApi.Model;
using demoApi.Model.User;
using demoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demoApi.Controllers
{

    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnityOfServices _unityOfServices;

        public UserController(UnityOfServices unityOfServices)
        {
            _unityOfServices = unityOfServices;
        }


        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleUserInfo")]
        public MessageModel<UserInfo_Model> GetSingleUserInfo()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            string userid = JwtHelper.SerializeJwt(token).ID;
            UserInfo_Model result = _unityOfServices._userServices.GetSingleUserInfo(userid);
            if (result != null)
            {
                return new MessageModel<UserInfo_Model>
                {
                    msg = "获取成功",
                    status = 200,
                    response = result
                };
            }
            else
            {
                return new MessageModel<UserInfo_Model>
                {
                    msg = "获取失败",
                    status = 201,
                    response = null
                };
            }

        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserList")]
        public MessageModel<dynamic> GetUserList(string query, int pagenum, int pagesize)
        {
            List<UserInfo_Model> userList = _unityOfServices._userServices.GetUserInfo(query, pagesize, pagenum);
            return new MessageModel<dynamic>
            {
                msg = "获取成功",
                status = 200,
                response = new
                {
                    totalpage = userList.Count(),
                    userlist = userList.Skip(pagesize * (pagenum - 1)).Take(pagesize),
                }
            };
        }

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ChangeStatus")]
        public MessageModel<string> ChangeStatus(int userid, string status)
        {
            int j = _unityOfServices._userServices.ChangeStatus(userid, status);
            if (j > 0)
            {
                return new MessageModel<string>
                {
                    msg = "更新状态成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "更新状态失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user_Model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public MessageModel<string> AddUser(AddUser_Model user_Model)
        {
            user_Model.password = MD5Hepler.MD5Encrypt32(user_Model.password);
            int result = _unityOfServices._userServices.AddUser(user_Model);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "添加用户成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "添加用户失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user_Model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePassword")]
        public MessageModel<string> ChangePassword(dynamic obj)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            string userid = JwtHelper.SerializeJwt(token).ID;
            dynamic objdyn = JsonConvert.DeserializeObject(Convert.ToString(obj));
            string pwd = MD5Hepler.MD5Encrypt32(Convert.ToString(objdyn.newpwd));
            int result = _unityOfServices._userServices.ChangePassword(userid, pwd);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "修改密码成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "修改密码失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }


        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="editUser_Model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditUser")]
        public MessageModel<string> EditUser(EditUser_Model editUser_Model)
        {
            int result = _unityOfServices._userServices.EditUser(editUser_Model);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "编辑用户成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "编辑用户失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteUser")]
        public MessageModel<string> DeleteUser(int roleid)
        {
            int result = _unityOfServices._userServices.DeleteUser(roleid);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "删除用户成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "删除用户失败",
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
        [Route("GetRoleList")]
        public MessageModel<List<RoleList_Model>> GetRoleList()
        {
            List<RoleList_Model> roleList_s = _unityOfServices._userServices.GetRoleList();
            return new MessageModel<List<RoleList_Model>>
            {
                msg = "获取角色列表成功",
                status = 200,
                response = roleList_s
            };
        }

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="role_Model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GiveRole")]
        public MessageModel<string> GiveRole(GiveRole_Model role_Model)
        {
            int result = _unityOfServices._userServices.GiveRole(role_Model);
            if (result > 0)
            {
                return new MessageModel<string>
                {
                    msg = "分配用户角色成功",
                    status = 200,
                    response = string.Empty
                };
            }
            else
            {
                return new MessageModel<string>
                {
                    msg = "分配用户角色失败",
                    status = 201,
                    response = string.Empty
                };
            }
        }

    }
}
