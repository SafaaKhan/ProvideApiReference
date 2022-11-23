using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProvideApiReference.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
