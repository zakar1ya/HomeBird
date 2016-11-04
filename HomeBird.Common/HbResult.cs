using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBird.Common
{
    public class HbResult<T>
    {
        public HbResult(T result)
        {
            Result = result;
        }

        public HbResult(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }

        public string ErrorMessage
        {
            get
            {
                return ErrorCodes.GetMessage(ErrorCode);
            }
        }

        public bool IsCorrect
        {
            get
            {
                return ErrorCode == 0;
            }
        }

        public T Result { get; set; }
    }
}
