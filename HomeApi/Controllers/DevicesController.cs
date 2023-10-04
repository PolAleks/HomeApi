using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IHostEnvironment _env;

        public DevicesController(
            ILogger<DevicesController> logger,
            IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// поиск и загрузка инструкиций для работы с устройством
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpHead]
        [Route("{manufacturer}")]
        public IActionResult GetManual([FromRoute] string manufacturer)
        {
            var pathDirectory = Path.Combine(_env.ContentRootPath, "Static");
            var filePath = Directory.GetFiles(pathDirectory)
                .FirstOrDefault(f => f.Split("\\")
                .Last()
                .Split(".")[0] == manufacturer);
            if (string.IsNullOrEmpty(filePath))
            {
                return StatusCode(404, $"Инструкции производителя {manufacturer} не найдено на сервере. Проверьте название!");
            }

            string fileType = "application/pdf";
            string fileName = $"{manufacturer}.pdf";

            return PhysicalFile(filePath, fileType, fileName);
        }
    }
}
