using Lab1.Entities;
using Lab1.Services;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Lab1;

public partial class Page3 : ContentPage
{
	public Page3(IDbService dbService)
	{
		InitializeComponent();
		this.dbService = dbService;
	}

	private IDbService dbService;
	private IEnumerable<string> authors_names;
	private IEnumerable<Book> selected_author_books;

    public void SelectedAuthorChanged(object sender, EventArgs e)
	{
		var author_id = ((Author)picker.SelectedItem).Id;
        selected_author_books = dbService.GetAuthorBooks(author_id);
        collection_view.ItemsSource = selected_author_books;

    }

    public void PageLoaded(object sender, EventArgs e)
    {
		picker.ItemsSource = new ObservableCollection<Author>(dbService.GetAllAuthors());
    }
}