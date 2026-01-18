using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsy();
        Task<Category> CreateCategoryAsy(Category Request);
        Task<Category?> GetById(int id);//session 13 part1
        Task DeleteCategory(Category category);//session 13 part1

        Task UpdateCategory(Category category);//session 13 part2
    }

}
