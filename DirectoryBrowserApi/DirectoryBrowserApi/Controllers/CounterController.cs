using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using DirectoryBrowserApi.Models;

namespace DirectoryBrowserApi.Controllers
{
    public class CounterController : ApiController
    {
        private readonly DirectoryTravereser _traverser;

        public CounterController()
        {
            _traverser = new DirectoryTravereser();
        }

        [HttpGet]
        public async Task<IHttpActionResult> CountFiles(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(_traverser.GetFilesCount(id, cancellationToken));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
