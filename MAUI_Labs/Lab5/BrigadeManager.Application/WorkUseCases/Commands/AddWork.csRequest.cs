using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.WorkUseCases.Commands
{
    public sealed class AddWorkRequest() : IAddOrEditWorkRequest
    {
        public Work Work { get; set; }
    }

    internal class AddWorkRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddWorkRequest>
    {
        public async Task Handle(AddWorkRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.WorkRepository.AddAsync(request.Work, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}
