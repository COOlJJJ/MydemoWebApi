using System;
using System.Collections.Generic;
using System.Text;

namespace demoApi.Model
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permission_Model
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string authName { get; set; }
        /// <summary>
        /// 节点等级
        /// </summary>
        public int NodeLevel { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public int UpId { get; set; }
        /// <summary>
        /// Route路径
        /// </summary>
        public string path { get; set; }
    }
}
