namespace UJM.Services
{
    public interface IApiResult
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
    }
}
