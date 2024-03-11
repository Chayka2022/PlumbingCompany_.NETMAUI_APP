using PlumbingCompany.Data;

namespace PlumbingCompany.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        LoginPageViewModel = viewModel;
        BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoginPageViewModel.ResetDataCommand.Execute(null);
    }

    LoginPageViewModel LoginPageViewModel;
}