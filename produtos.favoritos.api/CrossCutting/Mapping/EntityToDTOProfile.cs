using AutoMapper;
using Domain.DTOs.Client;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mapping
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<ClientDTO, ClientEntity>().ReverseMap();
            CreateMap<ClientCreateResultDTO, ClientEntity>().ReverseMap();
            CreateMap<ClientUpdateResultDTO, ClientEntity>().ReverseMap();
        }
    }
}
