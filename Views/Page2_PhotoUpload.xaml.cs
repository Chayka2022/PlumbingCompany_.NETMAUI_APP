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



    //public Page2_PhotoUpload()
    //{
    //}

    //private async void NextPageButton_Click(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new Page3_GeneralInfo());
    //}

    /*private async void PreviousPageButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Page1_JobOrder());
    }

    private async void JobOrderButton_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Page1_JobOrder());
    }*/

    //private async void PhotoUpload_Click(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new Page3_GeneralInfo());
    //}

    //private async void FinalAssembly_Click(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new Page11_FinalAssembly());
    //}

    //private void Page_Loaded(object sender, EventArgs e)
    //{
    //    var button = new Button { Text = "Job order" };
    //    var span = new Span { Text = button.Text, Underline = true };
    //    button.FormattedText = new FormattedString { Spans = { span } };
    //    Content = button;
    //}

    //private async void PickImages_Clicked(object sender, EventArgs e)
    //{
    //    var results = await FilePicker.PickMultipleAsync(new PickOptions
    //    {
    //        PickerTitle = "Pick Images",
    //        FileTypes = FilePickerFileType.Images
    //    });
    //}

    //List<ImageSource> selectedImages = new List<ImageSource>();



    //public ImageSource Image_source;
    //public string FilePath;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    //public ObservableCollection<Item> ItemCollection { get; set; } = new ObservableCollection<Item>();

    //private async void PickImages_Clicked1(object sender, EventArgs e)
    //{
    //    var results = await FilePicker.PickMultipleAsync(new PickOptions
    //    {
    //        PickerTitle = "Pick Images",
    //        FileTypes = FilePickerFileType.Images
    //    });


    //    if (results != null)
    //    {
    //        foreach (var file in results)
    //        {
    //           ItemCollection.Add(
    //           new Item
    //           {
    //               Name = file.FileName,
    //               ImageSource = file.FullPath
    //           });

    //        }
    //    }
    //}
    /////////////////////////////////////////////////////////////////////////////
}
