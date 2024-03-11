using PlumbingCompany.Constants;
using PlumbingCompany.Data;
using PlumbingCompany.Models;

namespace PlumbingCompany.Views;

public partial class JobList : ContentPage
{

	private JobListViewModel _viewModel; 

    public JobList(JobListViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_viewModel.GetJobListCommand.Execute(null);
	}

	async void EditRecord(object sender, SelectionChangedEventArgs e)
    {
		if (GlobalVariables.CurrentID != 0)
		{
			GlobalVariables.CurrentID = 0;
			GlobalVariables.CurrentCreatingID = 0;
        }
		try
		{
			GlobalVariables.CurrentID = (e.CurrentSelection.FirstOrDefault() as Page1Data).Id;
			GlobalVariables.CurrentCreatingID = 0;
			await Shell.Current.GoToAsync($"//JobListFlyOut/Page1_JobOrder");
		}
		catch
		{
			return;
		}
        //var navParam = new Dictionary<string, object>();
        //navParam.Add("PassingID", page1Data);
        //GlobalVariables.CurrentID = Jobs.Select(id => id.Id).FirstOrDefault();				
     }

} 