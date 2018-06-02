

namespace HomeBird.Common
{
    public class HbResult<T>
    {
        public HbResult(T result)
        {
            Result = result;
        }

        public HbResult(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;

        }

        public ErrorCodes ErrorCode { get; set; }

        public string ErrorMessage
        {
            get
            {
                return ErrorCode.GetMessage();
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
