using AutoMapper;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using System.ComponentModel;

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
                .ForMember(to => to.PersonType, map => map.MapFrom(from =>  GetDescription( from.DocumentType)))
                .ForMember(to => to.Accounts, map => map.MapFrom(from => from.Accounts));


            CreateMap<Account, AccountViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Status, map => map.MapFrom(from => GetDescription(from.Status)))
                .ForMember(to => to.Number, map => map.MapFrom(from => from.Number))
                .ForMember(to => to.Agency, map => map.MapFrom(from => from.Agency))
                .ForMember(to => to.Balance, map => map.MapFrom(from => from.Balance));

            CreateMap<Agency, AgencyViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id))
                .ForMember(to => to.Number, map => map.MapFrom(from => from.Code));

            CreateMap<TransactionBank, TransactionViewModel>()
                .ForMember(to => to.Id, map => map.MapFrom(from => from.Id));
        }

        private static string GetDescription(Enum @enum)
        {
            var description = @enum.GetType().GetField(@enum.ToString());

            var attributes = (DescriptionAttribute[])description.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            
            return @enum.ToString();
        }
    }
}