using System;
using System.Reflection;

namespace DotNetCore.Mediator
{
    [Serializable]
    public sealed class RequestHandlerNotFoundException : ArgumentException
    {
        public RequestHandlerNotFoundException(MemberInfo request) : base($"{ typeof(IRequestHandler<>).Name }<{ request.Name }>") { }

        public RequestHandlerNotFoundException(MemberInfo request, MemberInfo response) : base($"{ typeof(IRequestHandler<>).Name }<{ request.Name }, { response.Name }>") { }
    }
}
