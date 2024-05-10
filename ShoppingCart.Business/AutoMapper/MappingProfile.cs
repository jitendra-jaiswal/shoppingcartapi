using AutoMapper;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Discount, DiscountModel>();
        }
    }
}
