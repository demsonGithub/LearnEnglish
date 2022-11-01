using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.Listen.WebApi.Admin.Controllers
{
    [Authorize(Policy = "policy1")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResult<List<CategoryDto>>> GetCategoryListByCondiations([FromQuery] GetCategoryListQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);
            return ApiResult<List<CategoryDto>>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> AddCategory([FromBody] AddCategoryCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return ApiResult<string>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return ApiResult<string>.Build(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> DeleteCategory([FromBody] DeleteCategoryCommand command)
        {
            var result = await _mediator.Send(command, HttpContext.RequestAborted);

            return ApiResult<string>.Build(result);
        }
    }
}