namespace BrigadeManager.UI.Pages;

public partial class WorkDetails : ContentPage
{
	public WorkDetails(ViewModels.WorkDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}