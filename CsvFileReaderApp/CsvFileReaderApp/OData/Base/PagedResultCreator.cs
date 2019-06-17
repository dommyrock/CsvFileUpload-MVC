using Microsoft.AspNet.OData.Query; //because using System.Web.Http.OData doesnt have "Count"
using System.Linq;

namespace CsvFileReaderApp.OData
{
    public static class PagedResultCreator
    {
        /// <summary>
        /// Creates a paged set of results.(custom method--> because default ODataConventionModelBuilder is limited)
        /// </summary>
        /// <typeparam name="TGridQuery">The type of the returned paged results.</typeparam>
        /// <param name="queryable">The source IQueryable.</param>
        /// <param name="odataOptions">OData query options.</param>
        /// <returns>Returns a paged set of results.</returns>
        public static PagedResult CreatePagedResults<TGridQuery>(IQueryable<TGridQuery> queryable, ODataQueryOptions odataOptions)
        {
            int? skip = null;
            if (odataOptions.Skip != null) skip = odataOptions.Skip.Value;

            IQueryable<object> output = ApplyODataOptions(queryable, odataOptions);

            int? count = null;

            bool showCount = odataOptions.Count != null && odataOptions.Count.Value;
            if (showCount)
            {
                if (odataOptions.Filter != null)
                {
                    IQueryable<TGridQuery> countOutput = (IQueryable<TGridQuery>)odataOptions.Filter.ApplyTo(queryable, new ODataQuerySettings());
                    count = countOutput.Count();
                }
                else
                {
                    count = queryable.Count();
                }
            }

            int? top = null;
            if (odataOptions.Top != null) top = odataOptions.Top.Value;

            return new PagedResult
            {
                Data = output,
                Top = top,
                Skip = skip,
                Count = count
            };
        }

        private static IQueryable<object> ApplyODataOptions(IQueryable queryable, ODataQueryOptions odataOptions)
        {
            //this returns sub-optimal SQL queries (adds all selected column names to selected columns) when using $select in odata:
            return (IQueryable<object>)odataOptions.ApplyTo(queryable);

            //this is workaround: apply everything except the $select part and then apply the custom select part
            //IQueryable query = odataOptions.ApplyTo(queryable, AllowedQueryOptions.Select);

            //if (odataOptions.SelectExpand != null)
            //{
            //    List<string> fields = odataOptions.SelectExpand.RawSelect.Split(',').Select(item => item.Trim()).ToList();
            //    query = query.SelectDynamic(fields);
            //}
            //return (IQueryable<object>)query;
        }
    }
}