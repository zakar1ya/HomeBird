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
        public const int IncubatorAlreadyExist = 1;

        static ErrorCodes()
        {
            _messages = new Dictionary<int, string>();

            _messages.Add(LotNotFound, "Партия не найдена");
            _messages.Add(IncubatorNotFound, "Инкубатор не найден");
            _messages.Add(IncubatorAlreadyExist, "Инкубатор с таким именем уже существует");
        }
    }
}
