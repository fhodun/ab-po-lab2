using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace lab2;

public record SexOption(SexType Key, string Label);

public partial class PersonPopup : Popup<Person>
{
    public PersonPopup(Person? model = null)
    {
        InitializeComponent();

        SexPicker.ItemsSource = new[]
        {
            new SexOption(SexType.Male, "Mężczyzna"),
            new SexOption(SexType.Female, "Kobieta")
        };

        if (model == null)
        {
            return;
        }

        NameEntry.Text = model.Name;
        SurnameEntry.Text = model.Surname;
        BirthYearEntry.Text = model.BirthYear.ToString();
        var options = (IEnumerable<SexOption>)SexPicker.ItemsSource;
        SexPicker.SelectedItem = options.FirstOrDefault(o => o.Key == model.Sex);
    }

    private async Task<CancellationTokenSource> ShowToast(string text, bool semantic = true)
    {
        if (semantic) SemanticScreenReader.Announce(text);

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(text, ToastDuration.Short, 16);
        await toast.Show(cancellationTokenSource.Token);
        return cancellationTokenSource;
    }

    private async void SaveBtn_OnClicked(object? sender, EventArgs e)
    {
        if (!int.TryParse(BirthYearEntry.Text, out var year))
        {
            await ShowToast("Uzupełnij drugie pole");
            return;
        }

        var sex = (SexType)(SexPicker.SelectedItem?.ToString() == "Female" ? 1 : 0);

        var result = new Person(
            name: NameEntry.Text?.Trim() ?? string.Empty,
            surname: SurnameEntry.Text?.Trim() ?? string.Empty,
            birthYear: year,
            sex: sex
        );

        await CloseAsync(result);
    }

    private async void CancelBtn_OnClicked(object? sender, EventArgs e)
    {
        await CloseAsync();
    }
}