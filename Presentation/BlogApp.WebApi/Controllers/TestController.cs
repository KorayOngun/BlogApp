﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Unicode;

namespace BlogApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
            
        [HttpGet("[action]")]
        public IActionResult GetContent()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<table class=\"table table-hover\">\r\n  <thead>\r\n    <tr>\r\n      <th scope=\"col\">#</th>\r\n      <th scope=\"col\">First</th>\r\n      <th scope=\"col\">Last</th>\r\n      <th scope=\"col\">Handle</th>\r\n    </tr>\r\n  </thead>\r\n  <tbody>\r\n    <tr>\r\n      <th scope=\"row\">1</th>\r\n      <td>Mark</td>\r\n      <td>Otto</td>\r\n      <td>@mdo</td>\r\n    </tr>\r\n    <tr>\r\n      <th scope=\"row\">2</th>\r\n      <td>Jacob</td>\r\n      <td>Thornton</td>\r\n      <td>@fat</td>\r\n    </tr>\r\n    <tr>\r\n      <th scope=\"row\">3</th>\r\n      <td colspan=\"2\">Larry the Bird</td>\r\n      <td>@twitter</td>\r\n    </tr>\r\n  </tbody>\r\n</table>"
        };
        }
    }
}
