using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Sample.Controllers
{
    public class DemoController : ControllerBase
    {
        private readonly IStringLocalizer<DemoController> _localizer;
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger, IStringLocalizer<DemoController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation(_localizer["hi"]);
            var message = _localizer["hi"].ToString();
            return Ok(message);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var message = string.Format(_localizer["welcome"], name);
            return Ok(message);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var message = _localizer.GetAllStrings();
            return Ok(message);
        }

        [HttpGet("GetCurrent")]
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, " +
                   $"CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }

        [HttpGet("RaiseException")]
        public IActionResult RaiseException()
        {
            try
            {
                throw new ArgumentException("Email");
            }
            catch (Exception exception)
            {
                return Ok(new
                {
                    StatusCode = 400,
                    Message = string.Format(_localizer["argumentException"], exception.Message)
                });
            }
        }
    }
}