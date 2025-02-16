namespace BrigadeManager.UI.Pages;

public partial class AddOrEditWork: ContentPage
{
	public AddOrEditWork(ViewModels.AddOrEditWorkViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}