using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.Application.WorkUseCases.Commands
{
    public sealed class EditWorkRequest() : IAddOrEditWorkRequest
    {
        public Work Work { get; set; }
    }

    internal class EditWorkRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditWorkRequest>
    {
        public async Task Handle(EditWorkRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.WorkRepository.UpdateAsync(request.Work, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}
