using AutoMapper;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;

namespace FBank.Application.Mappers
{
    public class BaseEntityToViewModelMapping : Profile
    {
        public BaseEntityToViewModelMapping() 
        { 
            CreateMap<Client, ClientViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id));

            CreateMap<TransactionBank, TransactionViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id));
        }
    }
}
