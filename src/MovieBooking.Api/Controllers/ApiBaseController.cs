using Microsoft.AspNetCore.Mvc;

namespace MovieBooking.Api;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("api/v1/")]
public abstract class ApiBaseController : ControllerBase { }