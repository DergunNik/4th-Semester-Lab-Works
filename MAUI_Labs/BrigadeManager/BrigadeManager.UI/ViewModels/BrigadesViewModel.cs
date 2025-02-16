using BrigadeManager.Application.BrigadeUseCases.Queries;
using BrigadeManager.Application.BrigadeUseCases.Commands;
using BrigadeManager.Application.WorkUseCases.Queries;
using BrigadeManager.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrigadeManager.Application.WorkUseCases.Commands;


namespace BrigadeManager.UI.ViewModels
{
    public partial class BrigadesViewModel(IMediator mediator) : ObservableObject
    {
        private readonly IMediator _mediator = mediator;

        public ObservableCollection<Brigade> Brigades { get; set; } = new();
        public ObservableCollection<Work> Works { get; set; } = new();

        [ObservableProperty]
        Brigade? selectedBrigade;

        [RelayCommand]
        public async Task UpdateGroupList() => await GetBrigades();

        [RelayCommand]
        public async Task UpdateMembersList() => await GetWorks();

        [RelayCommand]
        public async Task ShowDetails(Work work) => await GotoDetailsPage(work);

        [RelayCommand]
        public async Task AddBrigade()
        {
            await GoToAddOrEditPage(nameof(AddOrEditBrigade), new AddBrigadeRequest() { Brigade = new Brigade()});
            await GetBrigades();
        }

        [RelayCommand]
        public async Task AddWork()
        {
            if (selectedBrigade is null)
                return;
            await GoToAddOrEditPage(nameof(AddOrEditWork),
                   new AddWorkRequest() { Work = new Work() { BrigadeId = selectedBrigade.Id } });
            await GetWorks();
        }

        private async Task GetBrigades()
        {
            var brigades = await _mediator.Send(new GetAllBrigadesRequest());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Brigades.Clear();
                foreach (var brigade in brigades)
                    Brigades.Add(brigade);
            });
        }

        private async Task GetWorks()
        {
            if (SelectedBrigade == null) return;

            var works = await _mediator.Send(new GetWorksByGroupRequest(SelectedBrigade.Id));
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Works.Clear();
                foreach (var work in works)
                    Works.Add(work);
            });
        }

        private async Task GotoDetailsPage(Work work)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>()
                {
                    { "Work", work}
                };
            await Shell.Current.GoToAsync(nameof(WorkDetails), parameters);
        }

        private async Task GoToAddOrEditPage(string route, IRequest request)
        {
            await Shell.Current.GoToAsync(route, new Dictionary<string, object>()
            {
                { "Request", request }
            });
        }
    }
}
