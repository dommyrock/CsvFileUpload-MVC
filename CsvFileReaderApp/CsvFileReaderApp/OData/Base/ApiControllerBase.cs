using Microsoft.AspNet.OData.Query;
using System.Linq;
using System.Web.Http;

namespace CsvFileReaderApp.OData
{
    //[RequireLogin] attributes for validation would go here
    //[HandleActor]
    public abstract class ApiControllerBase : ApiController
    {
        //public new User User { get; set; }

        protected virtual IHttpActionResult CreatePagedActionResult<TGridQuery>(IQueryable<TGridQuery> queryable, ODataQueryOptions odataOptions)
        {
            PagedResult result = PagedResultCreator.CreatePagedResults(queryable, odataOptions);
            return Ok(result);
        }
    }
}