namespace UJM.Services
{
    public class ApiResult<TResult> : IApiResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TResult Data { get; set; }

    }
}
