using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProdutosCia.API.Controllers.Base;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[Produces("application/json")]
public class BaseController : ControllerBase
{ 
}