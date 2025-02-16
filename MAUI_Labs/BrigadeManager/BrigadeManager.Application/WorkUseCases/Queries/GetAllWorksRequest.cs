using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.WorkUseCases.Queries
{
    public sealed record GetAllWorksRequest() : IRequest<IEnumerable<Work>>;

    internal class GetAllWorksRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllWorksRequest, IEnumerable<Work>>
    {
        public async Task<IEnumerable<Work>> Handle(
            GetAllWorksRequest request, 
            CancellationToken cancellationToken)
        {
            return await unitOfWork.WorkRepository.ListAllAsync();
        }
    }
}
