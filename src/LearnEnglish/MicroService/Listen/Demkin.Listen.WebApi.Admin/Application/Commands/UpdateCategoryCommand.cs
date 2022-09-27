namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string CoverUrl { get; set; }

        public int SequenceNum { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ListenDomainService _domainService;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ListenDomainService domainService, ICategoryRepository categoryRepository)
        {
            _domainService = domainService;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = await _domainService.GetCategoryById(request.Id);
            categoryEntity.ChangeTitle(request.Title);
            categoryEntity.ChangeCoverUrl(request.CoverUrl);
            categoryEntity.ChangeSequenceNumber(request.SequenceNum);

            var result = await _categoryRepository.UpdateAsync(categoryEntity);
            return result == null;
        }
    }
}