using Microsoft.AspNetCore.Mvc;

namespace Demkin.Listen.WebApi.Admin.Controllers
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
        public async Task<ApiResult<string>> AddCategory([FromBody] AddCategoryCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return result ? ApiResultBuilder.Success() : ApiResultBuilder.Fail();
        }

        [HttpGet]
        public async Task<ApiResult<List<CategoryViewModel>>> GetCategoryListByCondiations([FromQuery] GetCategoryListByCondiationsQuery query)
        {
            return await _mediator.Send(query, HttpContext.RequestAborted);
        }
    }
}