using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;

namespace rh_admin.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public  RedirectResult Get()
        {
            _logger.LogInformation("redirecionando para documentação do projeto");
            return Redirect("/swagger/index.html");
        }
    }
}