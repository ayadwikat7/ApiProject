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
        public async Task<responseCategory> CreateCategory(requestCategory Request)
        {
            var ctaegory = Request.Adapt<Category>();

         await   _categoryRepository.CreateCategoryAsy(ctaegory);
            return ctaegory.Adapt<responseCategory>();

        }

        public async Task<List<responseCategory>> GetAllCategory()
        {
            var ctaegories = await _categoryRepository.GetAllAsy();
            var response = ctaegories.Adapt<List<responseCategory>>();
            return response;
        }

        public async Task<List<CategoryUserResponse>> GetAllCategoryForUser(string lan="en")
        {
            var ctaegories = await _categoryRepository.GetAllAsy();
            //foreach (var category in ctaegories)
            //{
            //    category.CategorTransoulations = category.CategorTransoulations
            //        .Where(t => t.Language == lan).ToList();
            //}
            //var response = ctaegories.Adapt<List<CategoryUserResponse>>();
            //var response = ctaegories.Select(c => new CategoryUserResponse
            //{

            //    id = c.Id,
            //    Name = c.CategorTransoulations.Where(t => t.Language == lan).Select(t=>t.Name).FirstOrDefault()

            //}).ToList();
            //var response= ctaegories.Adapt<List<CategoryUserResponse>>();
            var response = ctaegories.BuildAdapter().AddParameters("lan",lan).AdaptToType<List<CategoryUserResponse>>();

            return response;
        }
        public async Task<BaseResponse> TaggelStateuse(int id)
        {
            try {
                var Category = await _categoryRepository.GetById(id);
                if (Category is null)
                {
                    return new BaseResponse
                    {
                        Message = "Category not found",
                        Success = false,
                        error = new List<string> { "The category with the specified ID does not exist." }
                    };
                }
                Category.status=Category.status==statuse.Active? statuse.Inactive: statuse.Active;
                await _categoryRepository.UpdateCategory(Category);
                return new BaseResponse
                {
                    Message = "Category state toggled successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Message = "An error occurred while deleting the category",
                    Success = false,
                    error = new List<string> { ex.Message }
                };
            }
        }
        public async Task<BaseResponse> DeleteCategory(int id)//session 13 part1
        {

            try {

                var Category = await _categoryRepository.GetById(id);//session 13 part1
                if (Category is null)//session 13 part1
                {

                    return new BaseResponse//session 13 part1
                    {//session 13 part1
                        Message = "Category not found",//session 13 part1
                        Success = false,//session 13 part1
                        error = new List<string> { "The category with the specified ID does not exist." }//session 13 part1
                    };
                }
                else
                {
                    await _categoryRepository.DeleteCategory(Category);//session 13 part1
                    return new BaseResponse
                    {
                        Message = "Category deleted successfully",//session 13 part1
                        Success = true
                    };
                }
            
            }
            catch(Exception ex)
            {
                return new BaseResponse
                {
                    Message = "An error occurred while deleting the category",
                    Success = false,
                    error = new List<string> { ex.Message }
                };
            }

        }
        //session 13 part2
        public async Task<BaseResponse> UpdateCategory(int id, requestCategory Request)
        {
            try
            {
                var Category = await _categoryRepository.GetById(id);
                if (Category is null)
                {
                    return new BaseResponse
                    {
                        Message = "Category not found",
                        Success = false,
                        error = new List<string> { "The category with the specified ID does not exist." }
                    };
                }

                if (Request.CategorTransoulations != null)
                {
                    foreach (var t in Request.CategorTransoulations)
                    {
                        var existing = Category.CategorTransoulations
                            .FirstOrDefault(x => x.Language == t.Language);

                        if (existing != null)
                            existing.Name = t.Name;
                        else
                            Category.CategorTransoulations.Add(t.Adapt<CategorTransoulation>());
                    }
                }

                await _categoryRepository.UpdateCategory(Category);

                return new BaseResponse
                {
                    Message = "Category updated successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Message = "An error occurred while deleting the category",
                    Success = false,
                    error = new List<string> { ex.Message }
                };
            }
        }

    }
}
