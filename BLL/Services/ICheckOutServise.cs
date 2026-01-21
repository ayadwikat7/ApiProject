using DAL.DTOs.Request;
using DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICheckOutServise
    {
        Task<CheckOutResponse> HandleSuccessAsync(string sessionId);
       Task<CheckOutResponse> ProsessPayment(CheckOutRequest request,string UserId);
    }
}
