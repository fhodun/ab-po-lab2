using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;

namespace lab2;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        ReloadPeopleAsync();
    }

    private async Task ReloadPeopleAsync()
    {
        var persons = await App.Database.GetPersonsAsync();
        PersonList.ItemsSource = persons;
    }

    private async Task<CancellationTokenSource> ShowToast(string text, bool sementic = true)
    {
        if (sementic) SemanticScreenReader.Announce(text);

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(text, ToastDuration.Short, 16);
        await toast.Show(cancellationTokenSource.Token);
        return cancellationTokenSource;
    }

    private async void AddPersonBtn_OnClicked(object? sender, EventArgs e)
    {
        var popup = new PersonPopup();
        IPopupResult<Person> result = await this.ShowPopupAsync<Person>(popup);

        if (result.WasDismissedByTappingOutsideOfPopup)
        {
            return;
        }

        if (result.Result == null)
        {
            return;
        }

        await App.Database.SavePersonAsync(result.Result);
        await ReloadPeopleAsync();
    }

    private async void PersonMenuBtn_OnClicked(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.BindingContext is not Person person)
            return;

        var choice = await DisplayActionSheet(
            $"{person.Name} {person.Surname}",
            "Anuluj", null,
            "Edytuj", "Usuń");

        if (choice == "Edytuj")
        {
            // edycja w popupie (zakładam, że PersonPopup ma ctor z Person)
            var popup = new PersonPopup(person);
            var result = await this.ShowPopupAsync<Person>(popup);
            if(result.WasDismissedByTappingOutsideOfPopup)
                return;
            if (result.Result == null)
                return;
            ShowToast(result.Result.Id.ToString());
            await App.Database.SavePersonAsync(result.Result);
        }
        else if (choice == "Usuń")
        {
            bool confirm = await DisplayAlert(
                "Usuń",
                $"Usunąć {person.Name} {person.Surname}?",
                "Tak", "Nie");

            if (confirm)
            {
                await App.Database.DeletePersonAsync(person);
                await ShowToast("Usunięto");
            }
        }

        await ReloadPeopleAsync();
    }
}