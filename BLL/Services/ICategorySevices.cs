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
        Task<List<responseCategory>> GetAllCategory();
        Task<List<CategoryUserResponse>> GetAllCategoryForUser(string lan = "en");
        Task<responseCategory> CreateCategory(requestCategory Request);
        Task<BaseResponse> DeleteCategory(int id);
        Task<BaseResponse> UpdateCategory(int id, requestCategory Request);
        Task<BaseResponse> TaggelStateuse(int id);



    }
}
