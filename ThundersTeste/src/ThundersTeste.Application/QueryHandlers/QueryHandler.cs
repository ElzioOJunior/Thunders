using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace ThundersTeste.Application.QueryHandlers;

public abstract class QueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IMediator _bus;

    protected QueryHandler(IMediator bus)
    {
        _bus = bus;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
