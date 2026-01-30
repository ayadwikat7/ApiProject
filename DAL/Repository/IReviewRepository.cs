using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IReviewRepository
    {
        Task<bool> HasUserReviewProducts(string UserId, int PrdouctId);
        Task<Review> CreateAsync(Review request);
    }
}
