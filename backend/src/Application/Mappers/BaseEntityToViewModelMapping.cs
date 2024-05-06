using AutoMapper;
using Application.Extensions;
using Application.ViewMoldels;
using Domain.Entities;

namespace Application.Mappers
{
    public class BaseEntityToViewModelMapping : Profile
    {
        public BaseEntityToViewModelMapping()
        {
            CreateMap<Client, ClientViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Name, map => map.MapFrom(from => from.Name))
                .ForMember(to => to.Document, map => map.MapFrom(from => from.Document))
                .ForMember(to => to.PersonType, map => map.MapFrom(from => EnumExtensions.GetDescription(from.DocumentType)))
                .ForMember(to => to.Accounts, map => map.MapFrom(from => from.Accounts));


            CreateMap<Account, AccountViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Status, map => map.MapFrom(from => EnumExtensions.GetDescription(from.Status)))
                .ForMember(to => to.Number, map => map.MapFrom(from => from.Number))
                .ForMember(to => to.Agency, map => map.MapFrom(from => from.Agency))
                .ForMember(to => to.Balance, map => map.MapFrom(from => from.Balance));

            CreateMap<Agency, AgencyViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Number, map => map.MapFrom(from => from.Code));

            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(to => to.DateTransaction, map => map.MapFrom(f => f.CreateDateAt))
                .ForMember(to => to.Amount, map => map.MapFrom(f => f.Value))
                .ForMember(to => to.TransactionType, map => map.MapFrom(f => f.TransactionType))
                .ReverseMap();
        }
    }
}
