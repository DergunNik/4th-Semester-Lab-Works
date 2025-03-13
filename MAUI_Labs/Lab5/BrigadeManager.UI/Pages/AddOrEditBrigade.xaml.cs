namespace BrigadeManager.UI.Pages;

public partial class AddOrEditBrigade : ContentPage
{
	public AddOrEditBrigade(ViewModels.AddOrEditBrigadeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}