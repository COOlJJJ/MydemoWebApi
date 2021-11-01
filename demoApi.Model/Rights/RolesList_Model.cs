using System;
using System.Collections.Generic;
using System.Text;

namespace demoApi.Model.Rights
{
    public class RolesList_Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PermissionList_Model> Children { get; set; }
    }
}
