using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBird.Common
{
    public static class ErrorCodes
    {
        private static Dictionary<int, string> _messages;

        public const int Ok = 0;
        public const int LotNotFound = 1;
        public const int IncubatorNotFound = 2;
        public const int IncubatorAlreadyExist = 3;
        public const int PurchaseNotFound = 4;
        public const int OverheadsNotFound = 5;
        public const int SaleNotFound = 6;
        public const int LayingNotFound = 7;

        static ErrorCodes()
        {
            _messages = new Dictionary<int, string>();

            _messages.Add(LotNotFound, "Партия не найдена");
            _messages.Add(IncubatorNotFound, "Инкубатор не найден");
            _messages.Add(IncubatorAlreadyExist, "Инкубатор с таким именем уже существует");
            _messages.Add(PurchaseNotFound, "Закупка не найдена");
            _messages.Add(OverheadsNotFound, "Расход не найден");
            _messages.Add(SaleNotFound, "Продажа не найдена");
            _messages.Add(LayingNotFound, "Закладка не найдена");
        }
    }
}
