using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetItemById([FromRoute] int id)
        {
            if (id > 5)
                return Ok();

            throw new Exception("fail");
        }
    }
}