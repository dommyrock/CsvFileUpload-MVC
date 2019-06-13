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
        private string path = AppDomain.CurrentDomain.BaseDirectory + @"\Content\podaci.csv";//get app base dir and concat to csv folder

        public ActionResult Index()
        {
            return View();
        }

        private ImportFromFile import = new ImportFromFile();

        [HttpGet]
        public ActionResult LoadFile()
        {
            List<CsvFields> list = import.ImportCSV(path); //maybe static list instead or store to cache ?

            return View(list);
        }

        //Returns view of only saved users
        public ActionResult SaveFile()
        {
            List<CsvFields> list = import.ImportCSV(path);
            var savedUsers = import.SaveIfValid(list, dbContext);

            return View("SavedUsersView", savedUsers);
        }
    }
}