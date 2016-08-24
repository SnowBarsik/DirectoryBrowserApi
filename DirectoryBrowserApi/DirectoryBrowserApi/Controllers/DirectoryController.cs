using System;
using System.Web.Http;
using DirectoryBrowserApi.Models;

namespace DirectoryBrowserApi.Controllers
{
    public class DirectoriesController : ApiController
    {
        private readonly DirectoryBrowser _dirBrowser;
        public DirectoriesController()
        {
            _dirBrowser = new DirectoryBrowser();
        }

        [HttpGet]
        public IHttpActionResult Directories()
        {
            try
            {
                return Ok(_dirBrowser.GetDir());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult Directories(int id)
        {
            try
            {
                return Ok(_dirBrowser.GetDir(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
