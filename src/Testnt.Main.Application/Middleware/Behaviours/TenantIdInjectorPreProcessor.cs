using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Interface;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.Middleware.Behaviours
{
    public class TenantIdInjectorPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : BaseRequest
    {
        private readonly ICurrentUserService currentUserService;

        public TenantIdInjectorPreProcessor(ICurrentUserService currentUserService)
        {
            this.currentUserService = currentUserService;
        }
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.TenantId = currentUserService.TenantId;
            return Task.CompletedTask;
        }
    }
}
