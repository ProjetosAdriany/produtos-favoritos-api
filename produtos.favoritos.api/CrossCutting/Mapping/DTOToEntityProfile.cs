using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Client;
using Domain.DTOs.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mapping
{
    public class DTOToEntityProfile : Profile
    {
        public DTOToEntityProfile()
        {
            CreateMap<ClientEntity, ClientDTO>().ReverseMap();
            CreateMap<ClientEntity, ClientCreateDTO>().ReverseMap();
            CreateMap<ClientEntity, ClientUpdateDTO>().ReverseMap();
            CreateMap<ProductEntity, ProductCreateDTO>().ReverseMap();
        }
    }
}
