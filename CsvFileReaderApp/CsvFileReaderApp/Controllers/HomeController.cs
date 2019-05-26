using CsvFileReaderApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CsvFileReaderApp.Controllers
{
    public class HomeController : Controller
    {
        //Db context
        private CsvContext dbContext = new CsvContext();

        //Get file path
        private string path = AppDomain.CurrentDomain.BaseDirectory + @"\Content\podaci.csv";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadFile()
        {
            ImportFromFile import = new ImportFromFile();
            List<CsvFields> list = import.ImportCSV(path);

            return View(list);
        }

        //Returns view of only saved users
        public ActionResult SaveFile()
        {
            ImportFromFile import = new ImportFromFile();
            List<CsvFields> list = import.ImportCSV(path);
            var savedUsers = import.SaveIfValid(list, dbContext);

            return View("SavedUsersView", savedUsers);
        }
    }
}