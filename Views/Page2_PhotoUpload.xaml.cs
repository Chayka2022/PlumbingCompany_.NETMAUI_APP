using System.Collections.ObjectModel;
using PlumbingCompany.Data;
using PlumbingCompany.Models;

namespace PlumbingCompany.Views;

public partial class Page2_PhotoUpload : ContentPage
{

    public Page2_PhotoUpload(Page2ViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();


        var timer = new Timer((object obj) =>
        {
            MainThread.BeginInvokeOnMainThread(() => scrollView2.ScrollToAsync(page2button, ScrollToPosition.MakeVisible, true));
        }, null, 1300, Timeout.Infinite);
    }
}
