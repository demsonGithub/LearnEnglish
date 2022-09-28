namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string CoverUrl { get; set; }

        public int sequenceNumber { get; set; }
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
            var sourceCategory = await _domainService.GetCategoryById(request.Id);
            sourceCategory.ChangeTitle(request.Title);
            sourceCategory.ChangeCoverUrl(request.CoverUrl);
            sourceCategory.ChangeSequenceNumber(request.sequenceNumber);

            var targetCategory = await _categoryRepository.UpdateAsync(sourceCategory);
            var result = await _categoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
}