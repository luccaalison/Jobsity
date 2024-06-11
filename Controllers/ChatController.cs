using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        [Route("api/chatroom")]
        public IActionResult ChatRoom() {
            // Implementar a lógica do chat
            return View();
        }
    }
}
