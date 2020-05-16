using System;

namespace Nathan.Abstractions
{
    public interface INathanMiddlewareProvider
    {
        INathanMiddleware GetMiddleware(Type middlewareType, params object[] parameters);
    }
}