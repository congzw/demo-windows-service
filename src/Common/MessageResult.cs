namespace Common
{
    public class MessageResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        
        public static MessageResult MethodResult(string method, bool success, object data = null)
        {
            var result = new MessageResult();
            result.Success = success;
            result.Data = data;
            result.Message = string.Format("{0}: {1} => {2}", method, success ? " Success" : " Fail", data);
            return result;
        }

        public static MessageResult SuccessResult(string message, object data = null)
        {
            var result = new MessageResult() { Message = message, Success = true, Data = data };
            return result;
        }

        public static MessageResult FailResult(string message, object data = null)
        {
            var result = new MessageResult() { Message = message, Success = false, Data = data };
            return result;
        }
    }
}