using demoApi.Common.Helper;
using demoApi.Model.Login;
using demoApi.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demoApi.Services
{
    public class LoginServices
    {
        private readonly UnityOfRepository _unityOfRepository;


        public LoginServices(UnityOfRepository unityOfRepository)
        {
            _unityOfRepository = unityOfRepository;

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login_Model"></param>
        public async Task<PwdAndRole_Model> Login(Login_Model login_Model)
        {
            PwdAndRole_Model pwdAndRole_Model = await _unityOfRepository._loginRepository.GetPasswordAndRole(login_Model.Username);

            if (string.Compare(pwdAndRole_Model.Message, "该用户不存在").Equals(0))
            {
                return pwdAndRole_Model;
            }

            if (MD5Hepler.MD5Encrypt32(login_Model.Password).Equals(pwdAndRole_Model.Password))
            {
                return new PwdAndRole_Model
                {
                    RoleName = pwdAndRole_Model.RoleName,
                    Message = "登录成功"
                };
            }
            else
            {
                return new PwdAndRole_Model
                {
                    Message = "登录失败,密码错误"
                };
            }

        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<Menus_Model>> GetMenus(string roleName)
        {
            int id = 100;
            var permission_list = await _unityOfRepository._loginRepository.GetMenus(roleName);
            List<Menus_Model> menus_s = new List<Menus_Model>();
            var level1_list = permission_list.Where(x => x.NodeLevel == 1).ToList();//获取一级菜单的节点
            foreach (var item in level1_list)
            {
                Menus_Model menus_Model = new Menus_Model();
                menus_Model.id = id;
                menus_Model.authName = item.authName;
                menus_Model.path = item.path;
                var submenus_list = permission_list.Where(x => x.UpId == item.Id && x.NodeLevel == 2).ToList();//获取二级菜单节点 
                List<Menus_Model> submenus_s = new List<Menus_Model>();
                foreach (var subitem in submenus_list)
                {
                    Menus_Model submenus_Model = new Menus_Model();
                    submenus_Model.id = subitem.Id;
                    submenus_Model.authName = subitem.authName;
                    submenus_Model.path = subitem.path;
                    submenus_Model.children = null;
                    submenus_s.Add(submenus_Model);
                }
                menus_Model.children = submenus_s;
                menus_s.Add(menus_Model);
                id++;
            }
            return menus_s;
        }
    }
}
