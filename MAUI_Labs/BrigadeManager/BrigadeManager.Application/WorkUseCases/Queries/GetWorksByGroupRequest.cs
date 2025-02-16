using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.WorkUseCases.Queries
{
    public sealed record GetWorksByGroupRequest(int Id) : IRequest<IEnumerable<Work>>;

    internal class GetWorksByGroupRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetWorksByGroupRequest, IEnumerable<Work>>
    {
        public async Task<IEnumerable<Work>> Handle(
            GetWorksByGroupRequest request,
            CancellationToken cancellationToken)
        {
            return await unitOfWork.WorkRepository.ListAsync(x => x.BrigadeId == request.Id);
        }
    }
}

