using Microsoft.AspNetCore.Mvc;

namespace Module.SharedModule.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}