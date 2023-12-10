using Kafka.Demo.Producer.Models;
using Kafka.Demo.Producer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Demo.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MessageController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public MessageController(IProducerService producerService) => _producerService = producerService;

        [HttpPost]
        public async Task<IActionResult> Push(MessageDto messageDto)
        {
            await _producerService.SendAsync(messageDto.Value, messageDto.SpecifiedTopicName);
            return NoContent();
        }
    }
}
