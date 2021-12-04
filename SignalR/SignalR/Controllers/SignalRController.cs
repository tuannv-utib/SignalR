using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.DataStorage;
using SignalR.Hubs;
using SignalR.Models;
using SignalR.Repositories;
using System;
using static SignalR.TimeFeatures.TimeManager;

namespace SignalR.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private IHubContext<ChatHub> _hub;

        public SignalRController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        [HttpGet("request")]
        public IActionResult Request()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("chat_request", DataManager.GetData()));

            return Ok(new { Message = "Request Completed" });
        }

        [HttpGet("send/message/{content}")]
        public IActionResult SendMess(string content)
        {
            var mess = new MessageDetails()
            {
                id = "1",
                date = DateTime.Now,
                message = content

            };
            _hub.Clients.All.SendAsync("new_mess", mess);

            return Ok(new { Message = "Send mess success!" });
        }

        [HttpGet("token/{id}")]
        public IActionResult AuthGetToken(int id)
        {
            var token = new Auth().Authenticate(id);
            return Ok(new
            {
                token = token
            });
        }
    }
}
