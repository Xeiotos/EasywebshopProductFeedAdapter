using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasywebshopProductFeedAdapter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        // GET: api/Feed
        [HttpGet]
        public IActionResult Get()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "RSS", "rss.xml");

            return PhysicalFile(file, "application/xml");
        }
    }
}
