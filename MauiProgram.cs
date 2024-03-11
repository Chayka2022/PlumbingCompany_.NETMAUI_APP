using PlumbingCompany.Views;
using PlumbingCompany.Data;
using PlumbingCompany.Services;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;

namespace PlumbingCompany;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        

        //Services
        builder.Services.AddSingleton<IJobService, JobService>();

        //Views Registration
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<JobList>();
        builder.Services.AddTransient<Page1_JobOrder>();
        builder.Services.AddTransient<Page2_PhotoUpload>();
        builder.Services.AddTransient<Page3_GeneralInfo>();
        builder.Services.AddTransient<Page4_PreviousTagInfo>();
		builder.Services.AddTransient<Page5_ServiceManufacturersData>();
        builder.Services.AddTransient<Page6_TestResults>();
        builder.Services.AddTransient<Page7_RepairReport_1_>();
        builder.Services.AddTransient<Page8_RepairReport_2_>();
        builder.Services.AddTransient<Page9_Comments>();
        builder.Services.AddTransient<Page10_FinalSettings>();
        builder.Services.AddTransient<Page11_FinalAssembly>();
        builder.Services.AddTransient<PlumbingCompany.Views.WebView>();

        //ViewModels
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<JobListViewModel>();
        builder.Services.AddTransient<Page1ViewModel>();
        builder.Services.AddTransient<Page2ViewModel>();
		builder.Services.AddTransient<Page3ViewModel>();
		builder.Services.AddTransient<Page4ViewModel>();
		builder.Services.AddTransient<Page5ViewModel>();
        builder.Services.AddTransient<Page6ViewModel>();
        builder.Services.AddTransient<Page7ViewModel>();
        builder.Services.AddTransient<Page8ViewModel>();
        builder.Services.AddTransient<Page9ViewModel>();
        builder.Services.AddTransient<Page10ViewModel>();
        builder.Services.AddTransient<Page11ViewModel>();
        builder.Services.AddTransient<WebViewViewModel>();

        return builder.Build();  

        
	}
}
