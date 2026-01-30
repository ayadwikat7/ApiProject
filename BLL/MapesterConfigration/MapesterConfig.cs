using DAL.DTOs.Request;
using DAL.DTOs.Response;
using DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapesterConfigration
{
    public static class MapesterConfig
    {
        public static void MapesterConfiguration()
        {
            // TypeAdapterConfig<Category, responseCategory>
            //     .NewConfig()
            //.Map(dest => dest.Id, src => src.Id);



            TypeAdapterConfig<Category, responseCategory>
                   .NewConfig()
              .Map(dest => dest.CreatedBy, src => src.User.UserName);
            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig()
    .Map(dest => dest.id, src => src.Id);                         
            TypeAdapterConfig<ProductsTRansulationRequest, ProductTransulation>
                .NewConfig();



            TypeAdapterConfig<Product, ProductsUserResponse>.NewConfig()
                .Map(dest => dest.MainImage,
                       src => src.MainImage != null
                          ? $"https://localhost:7229/images/{src.MainImage}"
                          : null)

             .Map(dest => dest.Name, source => source.ProductTransulations != null
                 ? source.ProductTransulations
                     .Where(t => t.Language == MapContext.Current.Parameters["lan"].ToString())
                     .Select(t => t.Name)
                     .FirstOrDefault()
                 : null);

            TypeAdapterConfig<Product, ProductsResponse>
                  .NewConfig()
                  .Map(dest => dest.CreatedBy, src => src.CreatedBy)

                  .Map(dest => dest.MainImage,
                       src => src.MainImage != null
                          ? $"https://localhost:7229/images/{src.MainImage}"
                          : null)

                  .Map(dest => dest.SubImages,
                       src => src.SubImages != null
                          ? src.SubImages
                              .Select(img => $"https://localhost:7229/images/{img.ImagName}")
                              .ToList()
                          : new List<string>())

                  .Map(dest => dest.ProductsTransulations,
                       src => src.ProductTransulations != null
                          ? src.ProductTransulations.Adapt<List<ProductsTransulationResponse>>()
                          : new List<ProductsTransulationResponse>());

            TypeAdapterConfig<Product,ProductsUserDetailes>
    .NewConfig()
    .Map(dest => dest.MainImage,
                       src => src.MainImage != null
                          ? $"https://localhost:7229/images/{src.MainImage}"
                          : null)
    .Map(dest => dest.Name,
         source => source.ProductTransulations
             .Where(t => t.Language == MapContext.Current.Parameters["lan"].ToString())
             .Select(t => t.Name)
             .FirstOrDefault())
    .Map(dest => dest.Description,
         source => source.ProductTransulations
             .Where(t => t.Language == MapContext.Current.Parameters["lan"].ToString())
             .Select(t => t.Description)
             .FirstOrDefault());
            TypeAdapterConfig<Order, OrderResponse>.NewConfig()
.Map(dest => dest.userName, source => source.User.UserName);

        }

    }
}
