using Microsoft.AspNet.SignalR;

namespace DACN.Common
{
    public interface IUserIdProvider
    {
        string GetUserId(IRequest request);
    }
}