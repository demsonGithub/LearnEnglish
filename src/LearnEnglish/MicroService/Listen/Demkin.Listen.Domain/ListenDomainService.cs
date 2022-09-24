using Demkin.Core.Exceptions;
using Demkin.Infrastructure.Core;
using Demkin.Listen.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Listen.Domain
{
    public class ListenDomainService : IDenpendencyScope
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListenDomainService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

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

        public async Task<List<Category>> GetCategoryList(string title)
        {
            IEnumerable<Category> result;

            if (string.IsNullOrWhiteSpace(title))
            {
                result = await _categoryRepository.SwitchSlaveDb().FindListAsync(item => true);
            }
            else
            {
                result = await _categoryRepository.SwitchSlaveDb().FindListAsync(item => item.Title.Contains(title));
            }

            return result.ToList();
        }
    }
}