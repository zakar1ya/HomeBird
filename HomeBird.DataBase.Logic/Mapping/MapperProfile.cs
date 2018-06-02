using AutoMapper;
using HomeBird.Common.Extensions;
using HomeBird.DataBase.EfCore.Models;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using System.Linq;

namespace HomeBird.DataBase.Logic.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HbLots, HbLot>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.IdentifierNumber, opt => opt.MapFrom(u => u.Identifier))
                .ForMember(u => u.AvgAdultPrice, opt => opt.MapFrom(u => u.AvgAdultPrice))
                .ForMember(u => u.AvgDailyPrice, opt => opt.MapFrom(u => u.AvgDailyPrice))
                .ForMember(u => u.CreationDate, opt => opt.MapFrom(u => u.CreationDate))
                .ForMember(u => u.Loses, opt => opt.MapFrom(u => u.Loses))
                .ForMember(u => u.Profit, opt => opt.MapFrom(u => u.Profit))
                .ForMember(u => u.SoldCount, opt => opt.MapFrom(u => u.SoldCount));

            CreateMap<HbLot, UpdateLotForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.IdentifierNumber, opt => opt.MapFrom(u => u.IdentifierNumber));

            CreateMap<HbIncubators, HbIncubator>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Title, opt => opt.MapFrom(u => u.Title));

            CreateMap<HbIncubator, UpdateIncubatorForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Title, opt => opt.MapFrom(u => u.Title));

            CreateMap<HbPurchases, HbPurchase>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Address, opt => opt.MapFrom(u => u.Address))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.Lot, opt => opt.MapFrom(u => u.Lot))
                .ForMember(u => u.PurchaseDate, opt => opt.MapFrom(u => u.PurchaseDate));

            CreateMap<HbPurchase, UpdatePurchaseForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Address, opt => opt.MapFrom(u => u.Address))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.Lot.Id))
                .ForMember(u => u.PurchaseDate, opt => opt.MapFrom(u => u.PurchaseDate));

            CreateMap<HbOverheads, HbOverhead>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.Comment, opt => opt.MapFrom(u => u.Comment))
                .ForMember(u => u.Lot, opt => opt.MapFrom(u => u.Lot))
                .ForMember(u => u.OverheadDate, opt => opt.MapFrom(u => u.OverheadDate));

            CreateMap<HbOverhead, UpdateOverheadsForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.Comment, opt => opt.MapFrom(u => u.Comment))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.Lot.Id))
                .ForMember(u => u.OverheadDate, opt => opt.MapFrom(u => u.OverheadDate));

            CreateMap<HbSales, HbSale>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.SaleDate, opt => opt.MapFrom(u => u.SaleDate))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.Buyer, opt => opt.MapFrom(u => u.Buyer))
                .ForMember(u => u.Comment, opt => opt.MapFrom(u => u.Comment))
                .ForMember(u => u.Lot, opt => opt.MapFrom(u => u.Lot))
                .ForMember(u => u.Type, opt => opt.MapFrom(u => u.Type));

            CreateMap<HbSale, UpdateSaleForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Amount, opt => opt.MapFrom(u => u.Amount))
                .ForMember(u => u.SaleDate, opt => opt.MapFrom(u => u.SaleDate))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.Buyer, opt => opt.MapFrom(u => u.Buyer))
                .ForMember(u => u.Comment, opt => opt.MapFrom(u => u.Comment))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.Lot.Id))
                .ForMember(u => u.Type, opt => opt.MapFrom(u => u.Type));

            CreateMap<HbLayings, HbLaying>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.CreationDate, opt => opt.MapFrom(u => u.CreationTime))
                .ForMember(u => u.EggPrice, opt => opt.MapFrom(u => u.EggPrice))
                .ForMember(u => u.Incubator, opt => opt.MapFrom(u => u.Incubator))
                .ForMember(u => u.Lot, opt => opt.MapFrom(u => u.Lot));

            CreateMap<HbLaying, UpdateLayingForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.LayingDate, opt => opt.MapFrom(u => u.CreationDate))
                .ForMember(u => u.IncubatorId, opt => opt.MapFrom(u => u.Incubator))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.Lot.Id));

            CreateMap<HbBroods, HbBrood>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.BroodDate, opt => opt.MapFrom(u => u.BroodDate))
                .ForMember(u => u.DeadCount, opt => opt.MapFrom(u => u.DeadCount))
                .ForMember(u => u.EmptyCount, opt => opt.MapFrom(u => u.EmptyCount))
                .ForMember(u => u.DeadPercent, opt => opt.MapFrom(u => u.DeadPercent))
                .ForMember(u => u.EmptyPercent, opt => opt.MapFrom(u => u.EmptyPercent))
                .ForMember(u => u.Percent, opt => opt.MapFrom(u => u.Percent))
                .ForMember(u => u.Lot, opt => opt.MapFrom(u => u.Lot))
                .ForMember(u => u.PlacePrice, opt => opt.MapFrom(u => u.PlacePrice));

            CreateMap<HbBrood, UpdateBroodForm>()
                .IgnoreOther()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(u => u.Count, opt => opt.MapFrom(u => u.Count))
                .ForMember(u => u.BroodDate, opt => opt.MapFrom(u => u.BroodDate))
                .ForMember(u => u.DeadCount, opt => opt.MapFrom(u => u.DeadCount))
                .ForMember(u => u.LotId, opt => opt.MapFrom(u => u.Lot.Id))
                .ForMember(u => u.EmptyCount, opt => opt.MapFrom(u => u.EmptyCount));
        }
    }
}
