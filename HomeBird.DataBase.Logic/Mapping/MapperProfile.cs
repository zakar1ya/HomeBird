using AutoMapper;
using HomeBird.DataBase.Ef6.Models;
using HomeBird.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Logic.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HbLots, HbLot>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.IdentifierNumber, opt => opt.MapFrom(u => u.IdentifierNumber))
                .ForMember(u => u.AvgAdultPrice, opt => opt.MapFrom(u => u.AvgAdultPrice))
                .ForMember(u => u.AvgDailyPrice, opt => opt.MapFrom(u => u.AvgDailyPrice))
                .ForMember(u => u.CreationDate, opt => opt.MapFrom(u => u.CreationDate))
                .ForMember(u => u.Loses, opt => opt.MapFrom(u => u.Loses))
                .ForMember(u => u.Profit, opt => opt.MapFrom(u => u.Profit))
                .ForMember(u => u.SoldCount, opt => opt.MapFrom(u => u.SoldCount))
                .ForAllOtherMembers(u => u.Ignore());

            CreateMap<HbIncubators, HbIncubator>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Title, opt => opt.MapFrom(u => u.Title));

            CreateMap<HbPurchases, HbPurchase>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Address, opt => opt.MapFrom(u => u.Address))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.LotId))
                .ForMember(u => u.PurchaseDate, opt => opt.MapFrom(u => u.PurchaseDate));

            CreateMap<HbOverheads, HbOverhead>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.Comment, opt => opt.MapFrom(u => u.Comment))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.LotId))
                .ForMember(u => u.OverheadDate, opt => opt.MapFrom(u => u.OverheadDate));

            CreateMap<HbSales, HbSale>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.SaleDate, opt => opt.MapFrom(u => u.SaleDate))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.Buyer, opt => opt.MapFrom(u => u.Buyer))
                .ForMember(u => u.Comment, opt => opt.MapFrom(u => u.Comment))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.LotId))
                .ForMember(u => u.Type, opt => opt.MapFrom(u => u.Type));

        }
    }
}
