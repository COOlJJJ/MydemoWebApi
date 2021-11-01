using System;
using System.Collections.Generic;
using System.Text;

namespace demoApi.Model.User
{
    /// <summary>
    /// 用户信息模型
    /// </summary>
    public class UserInfo_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public int is_admin { get; set; }
        public string role { get; set; }
        public string mobile_phone { get; set; }

    }
}
