using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProvideApiReference_Models.Helpers;

namespace ProvideApiReference.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
