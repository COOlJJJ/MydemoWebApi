using System;
using System.Collections.Generic;
using System.Text;

namespace demoApi.Model.Login
{
    public class Menus_Model
    {
        public int id { get; set; }
        public string authName { get; set; }
        public string path { get; set; }
        public List<Menus_Model> children { get; set; }
    }
}
