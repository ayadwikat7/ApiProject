using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICategorySevices
    {
        List<responseCategory> GetAllCategory();
        responseCategory CreateCategory(requestCategory Request);
    }
}
