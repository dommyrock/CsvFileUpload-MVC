using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CsvFileReaderApp.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CsvFileReaderApp.OData
{
    [RoutePrefix("csv")]
    public class CsvFileReaderController : /*ODataController*/ ApiController
    {
        //Full path https://localhost ...odata/controller/api/file{id}

        //If a controller action returns an IHttpActionResult, Web API calls the ExecuteAsync method to create an HttpResponseMessage.
        //Then it converts the HttpResponseMessage into an HTTP response message.

        private CsvContext dbContext = new CsvContext();

        //http://localhost:62174/all (here we just replace path directly with action "all" !!!!
        [HttpGet]
        [Route("all")]
        public IQueryable<Podaci> GetPodacis()
        {
            return dbContext.Podacis;
        }

        ///api/csvfilereader/{id}
        [HttpGet]
        public IHttpActionResult GetSingleUser(int id)
        {
            Podaci dataRow = dbContext.Podacis.SingleOrDefault(item => item.Id == id);
            if (dataRow == null)
            {
                return NotFound();
            }
            return Ok(dataRow);
        }

        //This Action contains DI , thats why in webApiConfig -DI is enabled !
        [Route("file")]
        public IHttpActionResult GetFile(ODataQueryOptions<Podaci> oDataQueryOptions)//need to pass ?$top,filter,orderby ... options to new method that filters data(DM has example) "PagedResultCreator" + "PagedResult" classes (Add em )
        {
            List<Podaci> list = dbContext.Podacis.ToList();
            IQueryable<CsvFileReaderModel> podaciModels = list.Select(item => new CsvFileReaderModel
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                City = item.City,
                ZipCode = item.ZipCode
            }).AsQueryable();

            if (podaciModels == null)
            {
                return NotFound();
            }
            return Ok(podaciModels);
        }
    }
}