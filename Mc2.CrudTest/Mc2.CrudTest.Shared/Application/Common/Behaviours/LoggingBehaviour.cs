using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Shared.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;


    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;

    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        string? userName = string.Empty;



        _logger.LogInformation("Mc2 Request: {Name} {@UserName} {@Request}",
            requestName, userName, request);
    }
}
