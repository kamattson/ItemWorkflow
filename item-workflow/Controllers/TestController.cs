using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using item_workflow.Database;
using item_workflow.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace item_workflow.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly TestDbContext _testDbContext;

        public TestController(ILogger<TestController> logger, TestDbContext testDbContext)
        {
            _logger = logger;
            _testDbContext = testDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("this is hello - ----------");

            return Ok(await _testDbContext.Item.ToListAsync());

            //return "hello";
        }



     

    }


}
