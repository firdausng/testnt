using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Interface;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.Middleware.Behaviours
{
    public class TenantIdInjectorPreProcessor<TRequest> : IRequestPreProcessor<TRequest> //where TRequest : BaseRequest
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ILogger<TenantIdInjectorPreProcessor<TRequest>> logger;

        public TenantIdInjectorPreProcessor(ICurrentUserService currentUserService, ILogger<TenantIdInjectorPreProcessor<TRequest>> logger)
        {
            this.currentUserService = currentUserService;
            this.logger = logger;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            //logger.LogInformation("Injecting tenant id {@TenantId} to request {@Request} for user {@User}",
            //    currentUserService.TenantId, request, currentUserService.Name);
            //request.TenantId = currentUserService.TenantId;
            return Task.CompletedTask;
        }
    }
}
