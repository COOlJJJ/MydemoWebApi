using demoApi.Model;
using demoApi.Model.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npoi.Mapper;
using System.Collections.Generic;
using System.IO;

namespace demoApi.Controllers
{
    /// <summary>
    /// 简单的导入导出Excel Npoi.Mapper 复杂的用EPPlus.Core
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        //微软Bug
        //接收不到邮件
        //方案二：在[FromForm] 里添加Name属性:[FromForm(name = "file")]，这个属性需跟file参数保持一致


        /// <summary>
        /// Excel导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ImportExcel")]
        public MessageModel<List<ImportExcel_Model>> ImportExcel([FromForm(Name = "file")] IFormFile formFile)
        {
            //Excel文件的路径
            var mapper = new Mapper(formFile.OpenReadStream());
            //读取的sheet信息
            var models = mapper.Take<ImportExcel_Model>("sheet1");
            List<ImportExcel_Model> excel_Models = new List<ImportExcel_Model>();
            foreach (var row in models)
            {
                //映射的数据保留在value中
                ImportExcel_Model model = row.Value;
                excel_Models.Add(model);
            }
            return new MessageModel<List<ImportExcel_Model>>
            {
                msg = "导入Excel成功",
                status = 200,
                response = excel_Models
            };
        }



        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ExportExcel")]
        public ActionResult ExportExcel()
        {
            List<ImportExcel_Model> students = new List<ImportExcel_Model>
            {
                new ImportExcel_Model{ name="夫子",email="cdj2714@qq.com",mobile_phone="15151989759" },
                new ImportExcel_Model{ name="余帘",email="cdj2714@qq.com",mobile_phone="15151989759" },
                new ImportExcel_Model{ name="李慢慢",email="cdj2714@qq.com",mobile_phone="15151989759" },
             };

            var mapper = new Mapper();
            MemoryStream stream = new MemoryStream();
            //将students集合生成的Excel直接放置到Stream中
            mapper.Save(stream, students, "sheet1", overwrite: true, xlsx: true);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Demo.xlsx");

        }


    }
}

