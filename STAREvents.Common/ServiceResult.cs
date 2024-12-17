namespace STAREvents.Common
{
    public class ServiceResult
    {
        public bool Succeeded { get; }
        public List<string> Errors { get; }

        protected ServiceResult(bool succeeded, List<string> errors = null)
        {
            Succeeded = succeeded;
            Errors = errors ?? new List<string>();
        }

        public static ServiceResult Success() => new ServiceResult(true);

        public static ServiceResult Failure(List<string> errors) => new ServiceResult(false, errors);

        public static ServiceResult Failure(string error) => new ServiceResult(false, new List<string> { error });
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; }

        private ServiceResult(bool succeeded, T data = default, List<string> errors = null)
            : base(succeeded, errors)
        {
            Data = data;
        }

        public static ServiceResult<T> Success(T data) => new ServiceResult<T>(true, data);

        public static ServiceResult<T> Failure(List<string> errors) => new ServiceResult<T>(false, default, errors);

        public static ServiceResult<T> Failure(string error) => new ServiceResult<T>(false, default, new List<string> { error });
    }
}
