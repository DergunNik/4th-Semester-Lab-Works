using BrigadeManager.Application.BrigadeUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BrigadeManager.UI.ViewModels
{
    public partial class AddOrEditBrigadeViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
    {
        IAddOrEditBrigadeRequest _request;

        [ObservableProperty] string name = "";
        [ObservableProperty] string leader = "";
        [ObservableProperty] int workersNumber = 0;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _request = query["Request"] as IAddOrEditBrigadeRequest;
            if (_request == null) 
                return;
            Name = _request.Brigade.Name;
            Leader = _request.Brigade.Leader;
            WorkersNumber = _request.Brigade.WorkersNumber;
        }

        [RelayCommand]
        public async Task AddOrEditBrigade()
        {
            if (String.IsNullOrEmpty(Name))
            {
                await Shell.Current.DisplayAlert("Error", "Name cannot be empty.", "OK");
                return;
            }
            if (String.IsNullOrEmpty(Leader))
            {
                await Shell.Current.DisplayAlert("Error", "Leader cannot be empty.", "OK");
                return;
            }
            if (WorkersNumber < 0)
            {
                await Shell.Current.DisplayAlert("Error", "Workers number cannot be negative.", "OK");
                return;
            }

            _request.Brigade.Name = Name;
            _request.Brigade.Leader = Leader;
            _request.Brigade.WorkersNumber = WorkersNumber;
            await mediator.Send(_request);
            await GoBack();
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
