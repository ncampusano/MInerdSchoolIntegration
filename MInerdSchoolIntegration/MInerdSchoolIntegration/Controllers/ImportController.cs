using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MInerdSchoolIntegration.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MInerdSchoolIntegration.Controllers
{
    public class ImportController : Controller
    {
        private readonly DataContext _context;

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Import(List<IFormFile> files)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(files.FirstOrDefault().OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            long size = files.Sum(f => f.Length);








            // full path to file in temp location
            var filePath = Path.GetTempFileName();


            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(result.ToString());
        }
    }
}
