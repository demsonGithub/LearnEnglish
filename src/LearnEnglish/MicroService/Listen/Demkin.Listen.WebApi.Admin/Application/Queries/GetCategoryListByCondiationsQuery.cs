using Demkin.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace Demkin.Listen.WebApi.Admin.Application.Queries
{
    public class GetCategoryListByCondiationsQuery : IRequest<List<CategoryDto>>
    {
        public string? Title { get; set; }
    }

    public class GetCategoryListByCondiationsQueryHandler : IRequestHandler<GetCategoryListByCondiationsQuery, List<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ListenDbContext _dbContext;

        public GetCategoryListByCondiationsQueryHandler(IMapper mapper, IDbContextFactory<ListenDbContext> dbContextFactory)
        {
            _mapper = mapper;
            _dbContext = dbContextFactory.CreateDbContext();
        }

        public async Task<List<CategoryDto>> Handle(GetCategoryListByCondiationsQuery request, CancellationToken cancellationToken)
        {
            List<Category> categories = new List<Category>();
            if (string.IsNullOrEmpty(request.Title))
            {
                categories = await _dbContext.Categories.ToListAsync();
            }
            else
            {
                categories = await _dbContext.Categories.Where(item => item.Title.Contains(request.Title)).ToListAsync();
            }

            var result = _mapper.Map<List<CategoryDto>>(categories);

            return result;
        }
    }
}