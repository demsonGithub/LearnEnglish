using Microsoft.AspNetCore.Mvc;

namespace Demkin.Listen.WebApiAdmin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> AddCategory([FromBody] AddCategoryCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return result;
        }

        //[HttpGet]
        //public async Task<ApiResponse<List<Category>>> GetAllCategory()
        //{
        //    var result=await
        //}
    }
}