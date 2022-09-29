using Demkin.Core.Exceptions;
using Demkin.Listen.Domain.Interfaces;

namespace Demkin.Listen.Domain
{
    public class ListenDomainService : IDenpendencyScope
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAlbumRepository _albumRepository;

        public ListenDomainService(ICategoryRepository categoryRepository, IAlbumRepository albumRepository)
        {
            _categoryRepository = categoryRepository;
            _albumRepository = albumRepository;
        }

        #region Category

        public async Task<Category> AddNewCategory(string title, string cover, int sequenceNum)
        {
            // 判断有没有这个Category
            var objIsExist = await _categoryRepository.IsExistAsync(item => item.Title == title);
            if (objIsExist)
            {
                throw new DomainException($"{title}已经存在");
            }

            Uri? coverUrl = cover == "" ? null : new Uri(cover);

            Category category = new Category(title, coverUrl, sequenceNum);
            return category;
        }

        public async Task<Category> GetCategoryById(long id)
        {
            var entity = await _categoryRepository.FindAsync(id);
            return entity;
        }

        #endregion Category

        #region Album

        public async Task<Album> AddNewAlbum(string title, string cover, int sequenceNum, long categoryId)
        {
            var objIsExist = await _albumRepository.IsExistAsync(item => item.Title == title);
            if (objIsExist)
            {
                throw new DomainException($"{title}已经存在");
            }
            Uri? coverUrl = cover == "" ? null : new Uri(cover);

            Album album = new Album(title, coverUrl, sequenceNum, categoryId);

            return album;
        }

        #endregion Album
    }
}