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
        List<Category> GetAll();
        Category CreateCategory(Category Request);
    }
}
