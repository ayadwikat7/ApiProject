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

        public Category CreateCategory(Category Request)
        {
            _context.Caregories.Add(Request);
            _context.SaveChanges();
            return Request;
        }

        public List<Category> GetAll()
        {
            return _context.Caregories.Include(c => c.CategorTransoulations).ToList();
        }
    }
}
