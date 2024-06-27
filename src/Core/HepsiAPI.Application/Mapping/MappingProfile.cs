using AutoMapper;
using HepsiAPI.Application.Dtos;
using HepsiAPI.Application.Features.Auths.Commands.Register;
using HepsiAPI.Application.Features.ProductFeatures.Commands.CreateProduct;
using HepsiAPI.Application.Features.ProductFeatures.Commands.UpdateProduct;
using HepsiAPI.Application.Features.ProductFeatures.Queries.GetProduct;
using HepsiAPI.Domain.Entities;

namespace HepsiAPI.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, GetProductQueryResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryProducts.Select(cp => cp.Category)))
                .ReverseMap();


            CreateMap<CreateProductCommandRequest, Product>()
                .ForMember(
                dest => dest.CategoryProducts,
                opt => opt.MapFrom(src => src.CategoryIds.Select(id => new CategoryProduct() { CategoryId = id }))
                );

            CreateMap<UpdateProductCommandRequest, Product>()
                .ForMember(
                dest => dest.CategoryProducts,
                opt => opt.MapFrom(src => src.CategoryIds.Select(id => new CategoryProduct() { CategoryId = id }))
                );


            CreateMap<RegisterCommandRequest, AppUser>()
                .ForMember(
                dest => dest.SecurityStamp,
                opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));


            CreateMap<BrandDto, Brand>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();


        }
    }

}
