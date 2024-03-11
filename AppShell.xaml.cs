using PlumbingCompany;
using PlumbingCompany.Data;
using PlumbingCompany.Views;

namespace PlumbingCompany;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(JobList), typeof(JobList));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(Page1_JobOrder), typeof(Page1_JobOrder));
        Routing.RegisterRoute(nameof(Page2_PhotoUpload), typeof(Page2_PhotoUpload));
        Routing.RegisterRoute(nameof(Page3_GeneralInfo), typeof(Page3_GeneralInfo));
        Routing.RegisterRoute(nameof(Page4_PreviousTagInfo), typeof(Page4_PreviousTagInfo));
        Routing.RegisterRoute(nameof(Page5_ServiceManufacturersData), typeof(Page5_ServiceManufacturersData));
        Routing.RegisterRoute(nameof(Page6_TestResults), typeof(Page6_TestResults));
        Routing.RegisterRoute(nameof(Page7_RepairReport_1_), typeof(Page7_RepairReport_1_));
        Routing.RegisterRoute(nameof(Page8_RepairReport_2_), typeof(Page8_RepairReport_2_));
        Routing.RegisterRoute(nameof(Page9_Comments), typeof(Page9_Comments));
        Routing.RegisterRoute(nameof(Page10_FinalSettings), typeof(Page10_FinalSettings));
        Routing.RegisterRoute(nameof(Page11_FinalAssembly), typeof(Page11_FinalAssembly));
        Routing.RegisterRoute(nameof(PlumbingCompany.Views.WebView), typeof(PlumbingCompany.Views.WebView));
    }
}
