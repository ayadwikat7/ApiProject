using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDpContext _context;

        public CategoryRepository(ApplicationDpContext context )
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsy(Category Request)
        {
           await _context.Caregories.AddAsync(Request);
          await  _context.SaveChangesAsync();
            return Request;
        }

        public async Task<List<Category>> GetAllAsy()
        {
            return await _context.Caregories.Include(c => c.CategorTransoulations).Include(c => c.User).ToListAsync();
        }

        public async Task<Category?> GetById(int id)//session 13 part1
        {
            return await _context.Caregories.Include(c => c.CategorTransoulations).FirstOrDefaultAsync(c => c.Id == id);//session 13 part1
        }
        public async Task DeleteCategory(Category category)//session 13 part1
        {
            _context.Caregories.Remove(category);//session 13 part1
            await _context.SaveChangesAsync();//session 13 part1
        }
        public async Task UpdateCategory(Category category)//session 13 part2
        {
            _context.Caregories.Update(category);//session 13 part2
            await _context.SaveChangesAsync();//session 13 part2
        }

    }
}
