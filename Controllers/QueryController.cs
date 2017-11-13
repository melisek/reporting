using Microsoft.AspNetCore.Mvc;
using szakdoga.BusinessLogic;
using szakdoga.Models;

namespace szakdoga.Controllers
{
    [Route("api/queries")]
    public class QueryController : Controller
    {
        private IQueryRepository _queryRepository;

        public QueryController(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetQueries()
        {
            using (var queryMan = new QueryManager(_queryRepository))
            {
                var queries = queryMan.GetAll();
                return Ok(queries);
            }
        }
        [HttpGet("GetQueryColumns/{queryGUID}")]
        public IActionResult GetQueryColumns(string queryGUID)
        {
            if (string.IsNullOrEmpty(queryGUID))
                return BadRequest();

            using (var queryMan = new QueryManager(_queryRepository))
            {
                var result = queryMan.GetColumnNames(queryGUID);
                return Ok(result);
            }
        }
    }
}