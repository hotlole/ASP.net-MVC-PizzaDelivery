using Dodic.Domain.Enum;


namespace Dodic.Domain.Response
{
    public class BaseResponse
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public object Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
