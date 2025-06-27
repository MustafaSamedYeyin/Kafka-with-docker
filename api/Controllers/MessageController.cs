using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    [HttpGet]
    public IActionResult GetMessages()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult PostMessage([FromBody] string message)
    {
        return CreatedAtAction(nameof(GetMessages), new { id = 1 }, new { Message = message });
    }
}