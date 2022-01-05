using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace bc_localiza_api_ci_cd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        
        private readonly ILogger<ImageController> _logger;
        private readonly IConfiguration _configuration;

        public ImageController(ILogger<ImageController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult Get(bool image)
        {
            var random = new Random();
            var url = _configuration.GetValue<string>("URLBase") + random.Next(1, 10) + ".jpg";
            if (image)
            {
                using var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(url);
                return File(imageBytes, "image/jpg");
            }
            else
            {
                return Ok(url);
            }
        }
    }
}
