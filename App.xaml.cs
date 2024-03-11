using PlumbingCompany.Views;

namespace PlumbingCompany;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
    }
}
