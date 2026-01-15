using Azure.Core;
using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategorySevices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public responseCategory CreateCategory(requestCategory Request)
        {
           var ctaegory= Request.Adapt<Category>();
            
            _categoryRepository.CreateCategory(ctaegory);
            return ctaegory.Adapt<responseCategory>();

        }

        public List<responseCategory> GetAllCategory()
        {
            var ctaegories = _categoryRepository.GetAll();
            var response = ctaegories.Adapt<List<responseCategory>>();
            return response;
        }
    }
}
