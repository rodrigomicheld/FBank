using AutoMapper;
using FBank.Application.Queries;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;

namespace FBank.Application.Mappers
{
    public class BaseEntityToViewModelMapping : Profile
    {
        public BaseEntityToViewModelMapping() 
        {
            CreateMap<Client, ClientViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Name, map => map.MapFrom(from => from.Name))
                .ForMember(to => to.Document, map => map.MapFrom(from => from.Document))
                .ForMember(to => to.DocumentType, map => map.MapFrom(from => from.DocumentType))
                .ForMember(to => to.Accounts, map => map.MapFrom(from => from.Accounts));
        }
    }
}
