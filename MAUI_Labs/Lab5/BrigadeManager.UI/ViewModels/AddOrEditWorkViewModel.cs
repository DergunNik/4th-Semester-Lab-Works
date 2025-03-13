using BrigadeManager.Application.BrigadeUseCases.Commands;
using BrigadeManager.Application.BrigadeUseCases.Queries;
using BrigadeManager.Application.WorkUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrigadeManager.UI.ViewModels
{
    public partial class AddOrEditWorkViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
    {
        IAddOrEditWorkRequest _request;
        private static int _imagesCounter = 0;
        private const string DEFAULT_IMAGE = "default_image.jpg";

        [ObservableProperty] string name = "";
        [ObservableProperty] DateTime startDate;
        [ObservableProperty] DateTime endDate;
        [ObservableProperty] int rating = 1;
        [ObservableProperty] string imageSrc = DEFAULT_IMAGE;

        public ObservableCollection<Brigade> Brigades { get; set; } = new();
        [ObservableProperty] Brigade? selectedBrigade;
        [ObservableProperty] bool isBrigadeIdChangeable = true;
        [ObservableProperty] string pickerTitle = "Select brigade";

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _request = query["Request"] as IAddOrEditWorkRequest;
            if (_request == null)
                return;
            Name = _request.Work.Name;
            StartDate = _request.Work.StartDate;
            EndDate = _request.Work.EndDate;
            ImageSrc = _request.Work.ImageSrc ?? ImageSrc;
            Rating = _request.Work.Rating <= 10 && _request.Work.Rating > 0 ? _request.Work.Rating : 1;
            if (_request.Work.BrigadeId != 0 && String.IsNullOrEmpty(_request.Work.Name))
            {
                SelectedBrigade = await mediator.Send(new GetBrigadeByIdRequest(_request.Work.BrigadeId));
                IsBrigadeIdChangeable = false;
                PickerTitle = "Brigade is selected";
            } 
            else if (_request.Work.BrigadeId != 0)
            {
                PickerTitle = $"Brigade with Id {_request.Work.BrigadeId} was originally selected";
            }
            _request.Work.ImageSrc = _request.Work.ImageSrc ?? DEFAULT_IMAGE;

            await GetBrigades();
        }

        [RelayCommand]
        public async Task GetBrigades()
        {
            var brigades = await mediator.Send(new GetAllBrigadesRequest());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Brigades.Clear();
                foreach (var brigade in brigades)
                    Brigades.Add(brigade);
            });
        }

        [RelayCommand]
        public async Task AddOrEditWork()
        {
            if (String.IsNullOrEmpty(Name))
            {
                await Shell.Current.DisplayAlert("Error", "Name cannot be empty.", "OK");
                return;
            }
            if (Rating < 1 || Rating > 10)
            {
                await Shell.Current.DisplayAlert("Error", "Rating must be between 1 and 10.", "OK");
                return;
            }
            if (EndDate < StartDate)
            {
                await Shell.Current.DisplayAlert("Error", "End date cannot be earlier than start date.", "OK");
                return;
            }

            _request.Work.Name = Name;
            _request.Work.StartDate = StartDate;
            _request.Work.EndDate = EndDate;
            _request.Work.Rating = Rating;
            _request.Work.BrigadeId = SelectedBrigade?.Id ?? _request.Work.BrigadeId;
            _request.Work.ImageSrc = ImageSrc;
            await mediator.Send(_request);
            await GoBack();
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task SelectImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Choose image"
            });

            if (result != null)
            {
                string targetFolder = Path.Combine(FileSystem.AppDataDirectory, "Images");
                Directory.CreateDirectory(targetFolder);

                string targetFile = Path.Combine(targetFolder, $"image{++_imagesCounter}.jpg");

                using (var sourceStream = await result.OpenReadAsync())
                using (var destinationStream = File.Create(targetFile))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }
                ImageSrc = targetFile;
            }
        }
    }
}
