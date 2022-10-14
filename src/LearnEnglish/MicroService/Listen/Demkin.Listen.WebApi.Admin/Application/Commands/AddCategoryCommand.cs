namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AddCategoryCommand : IRequest<string>
    {
        public string Title { get; set; }

        public string? CoverUrl { get; set; }

        public int SequenceNumber { get; set; }
    }

    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, string>
    {
        private readonly DomainService _domainService;
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(DomainService domainService, ICategoryRepository categoryRepository)
        {
            _domainService = domainService;
            _categoryRepository = categoryRepository;
        }

        public async Task<string> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _domainService.AddNewCategory(request.Title, request.CoverUrl, request.SequenceNumber);

            // 添加到数据库
            var entity = await _categoryRepository.AddAsync(category);
            var result = await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                throw new DomainException("保存数据库失败");

            return entity.Id.ToString();
        }
    }
}