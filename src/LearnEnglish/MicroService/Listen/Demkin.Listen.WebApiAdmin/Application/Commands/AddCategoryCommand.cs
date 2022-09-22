namespace Demkin.Listen.WebApiAdmin.Application.Commands
{
    public class AddCategoryCommand : IRequest<ApiResponse<string>>
    {
        public string Name { get; set; }

        public string? CoverUrl { get; set; }

        public int SequenceNumber { get; set; }
    }

    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ApiResponse<string>>
    {
        private readonly ListenDomainService _domainService;
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ListenDomainService domainService, ICategoryRepository categoryRepository)
        {
            _domainService = domainService;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _domainService.AddNewCategory(request.Name, request.CoverUrl, request.SequenceNumber);

            // 添加到数据库
            var entity = await _categoryRepository.AddAsync(category);
            var result = await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (result)
            {
                return ApiResultBuilder.Success(entity.Id.ToString());
            }
            else
            {
                return ApiResultBuilder.Fail("保存数据库失败");
            }
        }
    }
}