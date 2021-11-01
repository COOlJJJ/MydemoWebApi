using demoApi.Model.User;
using demoApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demoApi.Services
{
    public class UserServices
    {
        private readonly UnityOfRepository _unityOfRepository;


        public UserServices(UnityOfRepository unityOfRepository)
        {
            _unityOfRepository = unityOfRepository;

        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public dynamic GetUserInfo(string query, int pagesize, int pagenum)
        {
            var userList = _unityOfRepository._userRepository.GetUserInfo(query);
            return userList;
        }
        /// <summary>
        /// 改变该用户状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ChangeStatus(int userid, string status)
        {
            int result = _unityOfRepository._userRepository.ChangeStatus(userid, status);
            return result;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user_Model"></param>
        /// <returns></returns>
        public int AddUser(AddUser_Model user_Model)
        {
            int result = _unityOfRepository._userRepository.AddUser(user_Model);
            return result;
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="editUser_Model"></param>
        /// <returns></returns>
        public int EditUser(EditUser_Model editUser_Model)
        {
            int result = _unityOfRepository._userRepository.EditUser(editUser_Model);
            return result;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            int result = _unityOfRepository._userRepository.DeleteUser(id);
            return result;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public List<RoleList_Model> GetRoleList()
        {
            return _unityOfRepository._userRepository.GetRoleList();
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="role_Model"></param>
        /// <returns></returns>
        public int GiveRole(GiveRole_Model role_Model)
        {
            return _unityOfRepository._userRepository.GiveRole(role_Model);

        }
    }
}
