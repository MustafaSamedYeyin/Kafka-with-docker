using core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class KafkaController : ControllerBase
{
    public IKafka _kafka { get; set; }

    public KafkaController(IKafka kafka)
    {
        _kafka = kafka;
    }

    [HttpPost]
    public async Task<IActionResult> ProduceAsync([FromQuery] string message)
    {
        return Ok(await _kafka.ProduceMessage(message));
    }

    [HttpGet]
    public IActionResult ConsumeAsync(int bookmark, int offset)
    {
        return Ok(_kafka.ConsumeMessages(bookmark,offset));
    }
}