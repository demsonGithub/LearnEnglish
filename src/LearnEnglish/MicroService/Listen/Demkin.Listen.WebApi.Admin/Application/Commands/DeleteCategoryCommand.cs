namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class DeleteCategoryCommand : IRequest<string>
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.RemoveAsync(request.Id);

            var result = await _categoryRepository.UnitOfWork.SaveEntitiesAsync();

            return result ? "删除成功" : "删除失败";
        }
    }
}