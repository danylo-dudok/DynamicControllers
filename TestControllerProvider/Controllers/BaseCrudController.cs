using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Providers.Attributes;

namespace TestControllerProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [GenericControllerNameConvention]
    public class BaseCrudController<T> : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() =>
            Ok(typeof(T)
                .GetProperties()
                .Select(x => x.Name));
    }
}
