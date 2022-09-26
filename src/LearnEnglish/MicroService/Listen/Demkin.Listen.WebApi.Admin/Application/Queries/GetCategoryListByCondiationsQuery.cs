using Demkin.Infrastructure.Core;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetCategoryListByCondiationsQuery : IRequest<ApiResult<List<CategoryViewModel>>>
    {
        public string? Title { get; set; }
    }

    public class GetCategoryListByCondiationsQueryHandler : IRequestHandler<GetCategoryListByCondiationsQuery, ApiResult<List<CategoryViewModel>>>
    {
        private readonly ListenDomainService _domainService;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryListByCondiationsQueryHandler(ListenDomainService domainService, ICategoryRepository categoryRepository)
        {
            _domainService = domainService;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResult<List<CategoryViewModel>>> Handle(GetCategoryListByCondiationsQuery request, CancellationToken cancellationToken)
        {
            var result = await _domainService.GetCategoryList(request.Title);

            var viewEntity = (result.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CoverUrl = x.CoverUrl == null ? "" : x.CoverUrl.ToString(),
                SequenceNumber = x.SequenceNumber,
                CreateTime = x.CreateTime,
            })).ToList();

            return ApiResultBuilder<List<CategoryViewModel>>.Success(viewEntity);
        }
    }
}