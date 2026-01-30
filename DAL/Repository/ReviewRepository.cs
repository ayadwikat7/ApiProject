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
    public class ReviewRepository : IReviewRepository

    {
        private readonly ApplicationDpContext _context;

        public ReviewRepository(ApplicationDpContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserReviewProducts(string UserId, int PrdouctId)
        {
            return await _context.Reviews
    .AnyAsync(r => r.UserId == UserId && r.ProductId == PrdouctId);

        }
        public async Task<Review> CreateAsync(Review request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

    }
}
