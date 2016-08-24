using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using DirectoryBroserApi.dtos;
using DirectoryBroserApi.Models;

namespace DirectoryBroserApi.Controllers
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
