using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;

namespace lab2;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
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
    }
}