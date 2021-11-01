using System;
using System.Collections.Generic;
using System.Text;

namespace demoApi.Model.Login
{
    public class PwdAndRole_Model
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否为admin
        /// </summary>
        public int is_admin { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Token { get; set; }
    }
}
