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
            await _categoryRepository.SwitchSlaveDb();

            var result = await _domainService.GetCategoryList(request.Title);

            Category test = new Category("卧槽卧槽", null, 100);
            await _categoryRepository.SwitchMasterDb();

            await _categoryRepository.AddAsync(test);

            var aa = await _categoryRepository.UnitOfWork.SaveEntitiesAsync();
            Log.Information(aa.ToString());

            var viewEntity = (result.Select(x => new CategoryViewModel
            {
                Title = x.Title,
                CoverUrl = x.CoverUrl == null ? "" : x.CoverUrl.ToString(),
                SequenceNumber = x.SequenceNumber,
                CreateTime = x.CreateTime,
            })).ToList();

            return ApiResultBuilder<List<CategoryViewModel>>.Success(viewEntity);
        }
    }
}