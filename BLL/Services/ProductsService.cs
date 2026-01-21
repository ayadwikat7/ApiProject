using DAL.Data;
using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Migrations;
using DAL.Models;
using DAL.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Services
{
    public class ProductsService : IProductService
    {
        private readonly IproductsRepository _productsRepository;
        private readonly IFileService _file;

        public ProductsService(IproductsRepository productsRepository, IFileService file)
        {
            _productsRepository = productsRepository;
            _file = file;
        }
        public async Task<ProductsResponse> CreateProduct(ProductsRequest request)
        {
            var product = new Product
            {
                Price = request.Price,
                Discount = request.Discount,
                Quantity = request.Quantity,
                CategoryId = request.CategoryId,
            };

            if (request.ProductsTRansulations != null && request.ProductsTRansulations.Any())
            {
                product.ProductTransulations = request.ProductsTRansulations
                    .Select(t => new ProductTransulation
                    {
                        Name = t.Name,
                        Description = t.Description,
                        Language = t.Language
                    })
                    .ToList();
            }

            if (request.MainImage != null)
            {
                var imagePath = await _file.UploadFileAsync(request.MainImage);
                product.MainImage = imagePath;
            }

            if (request.SubImages != null && request.SubImages.Any())
            {
                product.SubImages = new List<ProductsImage>();

                foreach (var file in request.SubImages)
                {
                    var subImagePath = await _file.UploadFileAsync(file);
                    if (string.IsNullOrEmpty(subImagePath))
                        continue;

                    product.SubImages.Add(new ProductsImage
                    {
                        ImagName = subImagePath
                    });
                }
            }

            await _productsRepository.AddAsync(product);

            var response = product.Adapt<ProductsResponse>();

            response.SubImages = product.SubImages?
                .Select(s => s.ImagName)
                .ToList();

            return response;
        }

        public async Task<List<ProductsResponse>> GetAllproductsForAdmin()
        {
            var products = await _productsRepository.GetAllAsy();
            var response = products.Adapt<List<ProductsResponse>>();
            return response;
        }

        public async Task<PaginatedResponse<ProductsUserResponse>> GetAllProductsForUser(
            string lan = "en", int page = 1, 
            int limit = 3,string?search=null,
                int? categoryId = null,
    decimal? minPrice = null,
    decimal? maxPrice = null)

        {
            var qyuery = _productsRepository.Query();
            if (search is not null) { 
                qyuery = qyuery.Where(p => p.ProductTransulations
                .Any(t => t.Language == lan && t.Name.Contains(search)|| t.Description.Contains(search)));
            }
            if (categoryId is not null)
            {
                qyuery = qyuery.Where(p => p.CategoryId == categoryId);
            }

            if (minPrice is not null)
            {
                qyuery = qyuery.Where(p => p.Price >= minPrice);
            }

            if (maxPrice is not null)
            {
                qyuery = qyuery.Where(p => p.Price <= maxPrice);
            }

            qyuery = _productsRepository.Query();

            var totalCount = await qyuery.CountAsync();

            qyuery = qyuery.Skip((page - 1) * limit).Take(limit);

            var response = qyuery
                .BuildAdapter()
                .AddParameters("lan", lan)
                .AdaptToType<List<ProductsUserResponse>>();

            return new PaginatedResponse<ProductsUserResponse>
            {

                TotalCount = totalCount,
                Page = page,
                Limit = limit,
                Data = response
            };


        }

        public async Task<ProductsUserDetailes> GetAllProductsDetailsForUser(int id, string lan = "en")
        {
            var product = await _productsRepository.FindByIdAsync(id);

            var response = product
                .BuildAdapter()
                .AddParameters("lan", lan)
                .AdaptToType<ProductsUserDetailes>();

            return response;
        }


    }
}
