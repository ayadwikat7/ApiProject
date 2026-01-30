using DAL.DTOs.Request;
using DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReviewService
    {
        Task<BaseResponse> AddReviewAsync(string UserId, CreateReviewRequest reviewRequest,int ProductId);
    }
}
