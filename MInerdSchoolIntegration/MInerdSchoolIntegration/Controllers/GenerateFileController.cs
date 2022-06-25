using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using MinerdSchoolIntegration.Data.Entities;
using MInerdSchoolIntegration.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MInerdSchoolIntegration.Controllers
{
    public class GenerateFileController : Controller
    {
        private readonly DataContext _context;

        public GenerateFileController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Generate(string schoolCode = "00639")
        {
            School school = _context.School.Where(s => s.Code == schoolCode).FirstOrDefault();
            string fileName = $"{school.Name}.txt";
            DateTime CurrentDate = DateTime.Now;
           ICollection<Student> students =  _context.Student.Where(s => s.School.Code == "00639").ToList();

            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);

            tw.WriteLine($"|E|{CurrentDate.Day.ToString().PadLeft(2, '0')}{CurrentDate.Month.ToString().PadLeft(2, '0')}{CurrentDate.Year}|{schoolCode}|{school.CampusCode}|{students.Count}|");

            foreach (Student student in students)
            {
                string academycInformation = string.Empty;

                var grades = _context.Grade.Where(g => g.Student.Id == student.Id).Include(s => s.Subject).ToList();

                foreach (var grade in grades)
                {
                    academycInformation = $"{academycInformation},{grade.Subject.Name.Substring(0, 3)}:{grade.ObtainedGrade}";
                }

                tw.WriteLine($"|D||{student.Rne}|{student.Town}|{student.AcademicPeriod}|{student.AcademicLevel}|{student.AcademicGrade}|{academycInformation.Substring(1, academycInformation.Length - 1)}|{student.BloodType}|{student.Disabilities}|");
            }

            tw.Flush();

            var length = memoryStream.Length;
            tw.Close();
            var toWrite = new byte[length];
            Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

            return File(toWrite, "text/plain", $"{school.Name}.txt");
        }
    }
}
