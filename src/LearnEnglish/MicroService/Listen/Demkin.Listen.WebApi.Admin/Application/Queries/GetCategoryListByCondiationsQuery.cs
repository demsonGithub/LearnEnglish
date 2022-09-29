using Demkin.Infrastructure.Core;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetCategoryListByCondiationsQuery : IRequest<ApiResult<List<CategoryViewModel>>>
    {
        public string? Title { get; set; }
    }

    public class GetCategoryListByCondiationsQueryHandler : IRequestHandler<GetCategoryListByCondiationsQuery, ApiResult<List<CategoryViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetCategoryListByCondiationsQueryHandler(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ApiResult<List<CategoryViewModel>>> Handle(GetCategoryListByCondiationsQuery request, CancellationToken cancellationToken)
        {
            var connectionString = "server=192.168.0.143;uid=sa;pwd=abc123#;database=LearnEnglish_Listen;";
            var dbContext = new ListenDbContext(_mediator, connectionString);

            List<Category> categories = new List<Category>();
            if (string.IsNullOrEmpty(request.Title))
            {
                categories = dbContext.Categories.ToList();
            }
            else
            {
                categories = dbContext.Categories.Where(item => item.Title.Contains(request.Title)).ToList();
            }

            var result = _mapper.Map<List<CategoryViewModel>>(categories);

            //var result = await _domainService.GetCategoryList(request.Title);

            //var viewEntity = (result.Select(x => new CategoryViewModel
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    CoverUrl = x.CoverUrl == null ? "" : x.CoverUrl.ToString(),
            //    SequenceNumber = x.SequenceNumber,
            //    CreateTime = x.CreateTime,
            //})).ToList();

            return ApiResultBuilder<List<CategoryViewModel>>.Success(result);
        }
    }
}