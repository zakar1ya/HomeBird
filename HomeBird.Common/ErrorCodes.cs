using System.Collections.Generic;

namespace HomeBird.Common
{
    public enum ErrorCodes
    {
        Ok = 0,
        LotNotFound = 1,
        IncubatorNotFound = 2,
        IncubatorAlreadyExist = 3,
        PurchaseNotFound = 4,
        OverheadsNotFound = 5,
        SaleNotFound = 6,
        LayingNotFound = 7,
        BroodNotFound = 8,
        LotAlreadyExist = 9,
        BroodAmountMoreThanLayingsSum = 10,
        SalesCountMoreThanBroodCount = 11
    }

    public static class ErrorCodesExtensions
    {
        public static string GetMessage(this ErrorCodes code)
        {
            switch (code)
            {
                case ErrorCodes.Ok:
                    return "";
                case ErrorCodes.LotNotFound:
                    return "Партия не найдена";
                case ErrorCodes.IncubatorNotFound:
                    return "Инкубатор не найден";
                case ErrorCodes.IncubatorAlreadyExist:
                    return "Инкубатор с таким именем уже существует";
                case ErrorCodes.PurchaseNotFound:
                    return "Закупка не найдена";
                case ErrorCodes.OverheadsNotFound:
                    return "Расход не найден";
                case ErrorCodes.SaleNotFound:
                    return "Продажа не найдена";
                case ErrorCodes.LayingNotFound:
                    return "Закладка не найдена";
                case ErrorCodes.BroodNotFound:
                    return "Вывод не найден";
                case ErrorCodes.LotAlreadyExist:
                    return "Партия уже существует";
                case ErrorCodes.BroodAmountMoreThanLayingsSum:
                    return "Объем вывода не должен превышать объем закладки.";
                case ErrorCodes.SalesCountMoreThanBroodCount:
                    return "Объем продаж не должен превышать объем вывода.";
                default:
                    return "";
            }
        }
    }
}
