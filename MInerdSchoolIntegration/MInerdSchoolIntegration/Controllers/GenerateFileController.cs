using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using MinerdSchoolIntegration.Data.Entities;
using MInerdSchoolIntegration.Data;
using MInerdSchoolIntegration.Models;
using Newtonsoft.Json;
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
           ICollection<Student> students =  _context.Student.Where(s => s.School.Code == schoolCode).ToList();

            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            List<Detalle> detalles = new List<Detalle>();

            Encabezado encabezado = new Encabezado
            {
                TipoRegistro = "E",
                FechaRemision = $"{CurrentDate.Day.ToString().PadLeft(2, '0')}{CurrentDate.Month.ToString().PadLeft(2, '0')}{CurrentDate.Year}",
                CodigoCentro = schoolCode,
                CodigoPlantel = school.CampusCode,
                NumeroRegistros = students.Count.ToString()
            };

            foreach (Student student in students)
            {
                string academycInformation = string.Empty;

                var grades = _context.Grade.Where(g => g.Student.Id == student.Id).Include(s => s.Subject).ToList();

                foreach (var grade in grades)
                {
                    academycInformation = $"{academycInformation},{grade.Subject.Name.Substring(0, 3)}:{grade.ObtainedGrade}";
                }

                detalles.Add(new Detalle
                {
                    TipoRegistro = "D",
                    CodigoCentroMinerd = student.Id.ToString(),
                    Rne = student.Rne,
                    MunicipioResidencia = student.Town,
                    PeriodoAcademico = student.AcademicPeriod,
                    NivelAcademico = student.AcademicLevel,
                    GradoAcademico = student.AcademicLevel,
                    InformacionAcademica = academycInformation,
                    TipoSangre = student.BloodType,
                    CondicionesDiscapacidad = student.Disabilities
                });
            }

            string JSONEncabezado = JsonConvert.SerializeObject(encabezado);
            string JSONDetalles = JsonConvert.SerializeObject(detalles);

            var x = $"[{JSONEncabezado},{JSONDetalles.ToString().Substring(1, JSONDetalles.ToString().Length - 1)}";
            tw.Write(x);
            tw.Flush();
            var length = memoryStream.Length;
            tw.Close();
            var toWrite = new byte[length];
            Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

            return File(toWrite, "text/plain", $"{school.Name}.json");
        }
    }
}
