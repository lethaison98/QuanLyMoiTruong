
namespace QuanLyMoiTruong.Application.ViewModels
{
    /// <summary>
    /// Wrapper around the real business API data (in Data property) to add API exec error information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }

        public ApiResult(T data)
        {
            Data = data;
        }

        public ApiResult(T data, bool isSuccess = true, string message = "") :
            this(isSuccess, message)
        {
            Data = data;
            Success = isSuccess;
            Message = message;
        }

        public ApiResult(bool isSuccess = true, string message = "")
        {
            Success = isSuccess;
            Message = message;
        }

        /// <summary>
        /// false: API exec Success
        /// true: API exec error
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// if IsError is true: contains error message
        /// if IsError is false: empty or contains additional API exec information
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The real data return by the API
        /// </summary>
        public T Data { get; set; }

        public virtual ApiResult<T> OnError(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
            return this;
        }

        public virtual ApiResult<T> OnSuccess(string SuccessMessage = "")
        {
            Success = true;
            Message = SuccessMessage;
            return this;
        }

    }

    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
        }

        public ApiErrorResult(string message)
        {
            Success = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationErrors)
        {
            Success = false;
            ValidationErrors = validationErrors;
        }
    }

    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            Success = true;
            Data = resultObj;
        }

        public ApiSuccessResult()
        {
            Success = true;
        }
    }
}

