using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Model
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProducToBeSaved, Product>(); 
        }
    }
}
