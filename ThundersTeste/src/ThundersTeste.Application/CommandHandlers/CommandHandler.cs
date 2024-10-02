using MediatR;
using System.Threading.Tasks;
using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Application.Commands;
using System.Threading;
using System;

namespace ThundersTeste.Application.CommandHandlers;

public abstract class CommandHandler
{
    private readonly IUnitOfWork _uow;
    protected CommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<bool> CommitAsync()
    {
        if (await _uow.CommitAsync()) return true;

        throw new Exception("Erro ao salvar os dados");

    }

}
