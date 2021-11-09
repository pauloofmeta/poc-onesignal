using System.Threading.Tasks;
using Carguero.PocPushNotification.WebApi.Interfaces;
using Carguero.PocPushNotification.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carguero.PocPushNotification.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService) => 
            _notificationService = notificationService;

        [HttpPost("all")]
        public async Task<IActionResult> PostAll()
        {
            var result = await _notificationService.SendAllAsync();
            if (result.HasError)
                return BadRequest(result.Errror);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostOnly([FromBody] UserModel model)
        {
            var result = await _notificationService.SendByUserAsync(model);
            if (result.HasError)
                return BadRequest(result.Errror);
            return Ok();
        }
    }
}