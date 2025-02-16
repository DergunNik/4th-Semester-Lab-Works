using CommunityToolkit.Mvvm.ComponentModel;
using Lab1.Entities;
using Lab1.Services;
using Microsoft.Extensions.Primitives;
using System.Collections.ObjectModel;

namespace Lab1;

public partial class Page4 : ContentPage
{
	IRateService rateService;
    private readonly HashSet<string> tracked_currencies_names = ["RUB", "EUR", "USD", "CHF", "CNY", "GBP"];
    public List<Rate> tracked_rates;
	public List<Rate> rates;
    private string fst_cur_name;
    private string snd_cur_name;
    private readonly Rate BYN = new Rate()
    {
        Cur_ID = -1,
        Cur_Abbreviation = "BYN",
        Cur_Scale = 1,
        Cur_OfficialRate = 1
    };
    private bool is_text_changing = false;

    public Page4(IRateService rateService)
	{
		InitializeComponent();
        BindingContext = this;
		this.rateService = rateService;
        UpdateRates(DateTime.Now);
    }

	public async void OnPickerSelectedÑurrencyChanged(object sender, EventArgs e)
	{
        try
        {
            var s = sender as Picker;
            var r = s.SelectedItem as Rate;
            bool is_fst_entry_changed = ReferenceEquals(s, fst_cur_picker);
            if (is_fst_entry_changed)
            {
                fst_cur_name = r.Cur_Abbreviation;
            }
            else
            {
                snd_cur_name = r.Cur_Abbreviation;
            }

            if (!String.IsNullOrEmpty(fst_cur_name) && !String.IsNullOrEmpty(snd_cur_name))
            {
                ChangeOtherEntry(is_fst_entry_changed);
            }
        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Message}", "OK");
        }
    }

    public async void OnDateSelected(object sender, DateChangedEventArgs e)
	{
        if (e.NewDate > DateTime.Now) 
        {
            ((DatePicker)sender).Date = DateTime.Today;
        }

        if (e.OldDate == e.NewDate)
        {
            return;
        }

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
            return;
        }

        UpdateRates(((DatePicker)sender).Date);
    }

    public void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (is_text_changing)
            return;

        is_text_changing = true;

        if (!string.IsNullOrEmpty(e.NewTextValue) && !double.TryParse(e.NewTextValue, out _))
        {
            ((Entry)sender).Text = e.OldTextValue;
            is_text_changing = false;
            return;
        }
        
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            fst_cur_entry.Text = "0";
            snd_cur_entry.Text = "0";
            is_text_changing = false;
            return;
        }

        if (e.NewTextValue.Length > 1 && e.NewTextValue[1] != ',' && e.NewTextValue[0] == '0')
            ((Entry)sender).Text = ((Entry)sender).Text.Remove(0, 1);

        if (!String.IsNullOrEmpty(fst_cur_name) && !String.IsNullOrEmpty(snd_cur_name))
        {
            var s = sender as Entry;
            ChangeOtherEntry(s.Placeholder == "1");
        }

        is_text_changing = false;
    }

    private void ChangeOtherEntry(bool is_fst_entry)
    {
        is_text_changing = true;
        var fst_rate = rates.First(a => a.Cur_Abbreviation == fst_cur_name);
        var snd_rate = rates.First(a => a.Cur_Abbreviation == snd_cur_name);
        var coefficient = (double)fst_rate.Cur_OfficialRate / fst_rate.Cur_Scale /
                          (double)snd_rate.Cur_OfficialRate * snd_rate.Cur_Scale;

        double num = 0;
        if (is_fst_entry)
        {
            if (!Double.TryParse(fst_cur_entry.Text, out num))
                num = 0;
            snd_cur_entry.Text = Math.Round(num * coefficient, 4).ToString();
        }
        else
        {
            if (!Double.TryParse(snd_cur_entry.Text, out num))
                num = 0;
            fst_cur_entry.Text = Math.Round(num / coefficient, 4).ToString();
        }

        is_text_changing = false;
    }

    private async void UpdateRates(DateTime date)
    {
        try
        {
            rates = new List<Rate>(await rateService.GetRatesAsync(date));
            tracked_rates = new List<Rate>(rates.
                Where(c => tracked_currencies_names.Contains(c.Cur_Abbreviation)));
            rates.Add(BYN);
            fst_cur_picker.ItemsSource = rates;
            snd_cur_picker.ItemsSource = rates;
            rates_view.ItemsSource = tracked_rates;
            if (!String.IsNullOrEmpty(fst_cur_name) && !String.IsNullOrEmpty(snd_cur_name))
            {
                ChangeOtherEntry(true);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Message}", "OK");
        }
    }
}