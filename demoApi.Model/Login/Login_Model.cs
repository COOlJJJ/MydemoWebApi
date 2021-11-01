using System.ComponentModel.DataAnnotations;

namespace demoApi.Model.Login
{
    public class Login_Model
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
}
