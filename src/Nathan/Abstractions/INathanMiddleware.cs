using System.Threading.Tasks;

namespace Nathan.Abstractions
{
    public interface INathanMiddleware
    {
        Task Invoke(NathanRequestContext nathanRequestContext, NathanRequestDelegate next);
    }
}