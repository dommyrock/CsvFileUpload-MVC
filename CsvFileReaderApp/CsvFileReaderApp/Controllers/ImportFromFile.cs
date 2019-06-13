using CsvFileReaderApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.RegularExpressions;

namespace CsvFileReaderApp.Controllers
{
    /// <summary>
    /// Class for importing data from Csv
    /// </summary>
    public class ImportFromFile
    {
        /// <summary>
        /// Outputs list of lines from Csv file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns></returns>
        public List<CsvFields> ImportCSV(string path)
        {
            //Read CSV
            List<string> lines = new List<string>(File.ReadLines(path));
            //Output all CSV rows
            List<CsvFields> output = new List<CsvFields>();
            foreach (string line in lines)
            {
                string[] columns = line.Split(';');
                CsvFields row = new CsvFields(columns[0], columns[1], columns[2], columns[3], columns[4]);//Map
                if (!Regex.IsMatch(row.ZipCode, @"^\d{5}$"))//match zipCode
                {
                    row.ErrorMsg = "Zip code Invalid";
                }
                output.Add(row);
            }
            return output;
        }

        public List<CsvFields> SaveIfValid(List<CsvFields> list, CsvContext dbContext)
        {
            //Outputs only valid /saved rows
            List<CsvFields> output = new List<CsvFields>();
            foreach (var item in list)
            {
                try
                {
                    if (Regex.IsMatch(item.ZipCode, @"^\d{5}$"))//match zipCode
                    {
                        dbContext.Proc_InsertData(item.FirstName, item.LastName, item.ZipCode, item.City, item.Phone);

                        output.Add(item);
                    }
                    else
                    {
                        item.ErrorMsg = "Zip code is not valid!!";
                    }
                }
                catch (Exception ex)//handle duplicate item
                {
                    item.ErrorMsg = ex.Message;//this is kinda usless ...never gets saved anywhere (since we dont save it to DB )
                }
            }
            return output;
        }
    }

    public class CsvFields
    {
        public CsvFields(string name, string surname, string zip, string city, string phone)
        {
            this.FirstName = name;
            this.LastName = surname;
            this.ZipCode = zip;
            this.City = city;
            this.Phone = phone;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// If zip code doesn't match , ErrorMsg="Zip code Invalid!"
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}