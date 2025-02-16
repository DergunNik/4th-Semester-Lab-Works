using BrigadeManager.Application.WorkUseCases.Commands;
using BrigadeManager.Application.WorkUseCases.Queries;
using BrigadeManager.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.UI.ViewModels
{
    [QueryProperty("Work", "Work")]
    public partial class WorkDetailsViewModel(IMediator mediator) : ObservableObject
    {
        [ObservableProperty] private Work work;

        [ObservableProperty] string name;
        [ObservableProperty] int rating;
        [ObservableProperty] DateTime startDate;
        [ObservableProperty] DateTime endDate;
        [ObservableProperty] string imageSrc;

        [RelayCommand]
        async Task GetWorkById()
        {
            Work = await mediator.Send(new GetWorkByIdRequest(Work.Id));
            Name = Work.Name;
            Rating = Work.Rating;
            StartDate = Work.StartDate;
            EndDate = Work.EndDate;
            ImageSrc = Work.ImageSrc;
        }

        [RelayCommand]
        async Task EditWork()
        {
            await Shell.Current.GoToAsync(nameof(AddOrEditWork), new Dictionary<string, object>()
            {
                { "Request", new EditWorkRequest() { Work = work } }
            });
        }
    }
}
