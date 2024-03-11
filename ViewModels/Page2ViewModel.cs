using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PlumbingCompany.Models;
using System.Text;
using System.Threading.Tasks;
using PlumbingCompany.Services;
using PlumbingCompany.Data;
using PlumbingCompany.Views;
using PlumbingCompany.Constants;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Buffers.Text;
using System.Security.Cryptography;
using Microsoft.Maui.Media;
using System.Globalization;

namespace PlumbingCompany.Data;


public partial class Page2ViewModel : ObservableObject
{

    private readonly IJobService _jobService;

    public Page2ViewModel(IJobService jobService)
    {
        _jobService = jobService;

        if (GlobalVariables.CurrentID != 0)
        {
            ID = GlobalVariables.CurrentID;
            GetPhotoList(ID);
            Progress = _jobService.DeserealizeRecordDescription(GlobalVariables.CurrentID);
            Sections = Progress.sections;
        }

        else if (GlobalVariables.CurrentCreatingID != 0)
        {
            ID = GlobalVariables.CurrentCreatingID;
            GetPhotoList(ID);
            Progress = _jobService.DeserealizeRecordDescription(GlobalVariables.CurrentCreatingID);
            Sections = Progress.sections;
        }

        else if (GlobalVariables.CurrentCreatingID == 0)
        {
            Sections.photoSec = true;
        }
        ColorsCompare();
    }

    [ObservableProperty]
    string _ConsoleLog;

    [ObservableProperty]
    string _HTMLSource;

    [ObservableProperty] 
    StringBuilder _HTML_String_Builder;

    [ObservableProperty]
    private Page2Data _Page2Data = new Page2Data();

    [ObservableProperty]
    HtmlWebViewSource _MyHtml = new HtmlWebViewSource();

    [ObservableProperty]
    private ImageSource imageSource;

    [ObservableProperty]
    private string _Name;

    [ObservableProperty]
    private int _Id;

    [ObservableProperty]
    private string _ImageData;

    [ObservableProperty]
    private byte[] _Bytes;

    [ObservableProperty]
    int _count = 0;

    [ObservableProperty]
    int _ImagesSaved;

    [ObservableProperty]
    int _ID;

    [ObservableProperty]
    public Progress _Progress = new Progress();

    [ObservableProperty]
    public Sections _Sections = new Sections();

    public string base64ImageString;

    public ObservableCollection<Page2Data> ImagesCollection { get; private set; } = new ObservableCollection<Page2Data>();

    public List<Page2Data> ImagesList { get; set; } = new List<Page2Data>();

    [RelayCommand]
    void Create_HTML_Image_List()
    {
        HTML_String_Builder = new StringBuilder();
        HTML_String_Builder.Append("<!DOCTYPE html>");
        HTML_String_Builder.Append("<HTML>");
        HTML_String_Builder.Append("<style> img { height: 350px; } </style>");
        HTML_String_Builder.Append("<BODY>");
        if (ImagesList.Count() > 0)
        {
            for (int i = 0; i < ImagesList.Count(); i++)
            {
                base64ImageString = Convert.ToBase64String(ImagesList[i].Bytes);
                HTML_String_Builder.Append("<H4>Image " + (i + 1).ToString() + ".</H4>");
                HTML_String_Builder.Append(@"<img src =""data:image/jpeg; base64," + base64ImageString + @"""/>");
            }

        }
        else
        {
            HTML_String_Builder.Append("<H3>No images found</H3>");
        }
        HTML_String_Builder.Append("</BODY>");
        HTML_String_Builder.Append("</HTML>");
        HTMLSource = HTML_String_Builder.ToString();
    }

    [RelayCommand]
    public async Task PickImages_Clicked2Async()
    {

        var results = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Pick Image"
        });

        if (GlobalVariables.CurrentID != 0) ID = GlobalVariables.CurrentID;
        else if (GlobalVariables.CurrentCreatingID != 0) ID = GlobalVariables.CurrentCreatingID;

        if (results != null)
        { 
            var stream = await results.OpenReadAsync();
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                var _bytes = memoryStream.ToArray();

                var response = await _jobService.AddImage(new Models.Page2Data
                {
                    Name = results.FileName,
                    ImageSource = results.FullPath,
                    Id = ID,
                    Bytes = _bytes
                });

                GetPhotoList(ID);

            }
        }
        ImagesSaved = ImagesCollection.Count;
    }


    [RelayCommand]
    public async Task CapturePhoto()
    {
        var status = PermissionStatus.Unknown;

        status = await Permissions.CheckStatusAsync<Permissions.Camera>();

        if (status == PermissionStatus.Granted)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (GlobalVariables.CurrentID != 0) ID = GlobalVariables.CurrentID;
            else if (GlobalVariables.CurrentCreatingID != 0) ID = GlobalVariables.CurrentCreatingID;

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    var _bytes = memoryStream.ToArray();

                    var response = await _jobService.AddImage(new Models.Page2Data
                    {
                        Name = photo.FileName,
                        ImageSource = photo.FullPath,
                        Id = ID,
                        Bytes = _bytes
                    });

                    GetPhotoList(ID);

                }
                ImagesSaved = ImagesCollection.Count;
            }
            return;
        }

        else if (Permissions.ShouldShowRationale<Permissions.Camera>())
        {
            await Shell.Current.DisplayAlert("Needs permissions", "Allow Camera Permission", "Ok");
        }
        status = await Permissions.RequestAsync<Permissions.Camera>();
    }


    [RelayCommand]
    public async void GetPhotoList(int ID)
    {
        try
        {
            ImagesCollection.Clear();
            var imageList = await _jobService.GetImagesList(ID).ConfigureAwait(true);
            if (imageList?.Count > 0)
            {
                int i = 0;
                imageList = imageList.ToList();
                foreach (var image in imageList)
                {
                    i++;
                    image.Name = i.ToString() + ". " + image.Name;
                    Page2Data = image;
                    ImagesCollection.Add(Page2Data);
                }
                ImagesList.Clear();
                ImagesList.AddRange(imageList);
                ImagesSaved = ImagesList.Count();
            }
            else 
            { 
                ImagesSaved = 0; 
                ImagesList.Clear(); 
            }
            Create_HTML_Image_List();
        }
        catch(Exception ex)
        {
            ConsoleLog = ex.ToString();
        }
        finally { }
        
    }

    [RelayCommand]
    private void DeleteImageButtonTyped(Page2Data page2Data)
    {
        try
        {
            _jobService.DeleteImage(page2Data);
        }
        catch(Exception ex)
        {
            ConsoleLog = ex.ToString();
        }
        GetPhotoList(ID);
    }



    [RelayCommand]
    private async void PreviousPageButtonClickedAsync()
    {
        Progress.sections = Sections;
        Progress.nextSection = "firstSec";
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page1_JobOrder");
    }


    [RelayCommand]
    public async Task BackToList()
    {
        await Shell.Current.GoToAsync($"//JobListFlyOut");
    }

    [RelayCommand]
    public async void NextPageButtonClickedAsync()
    {
        Sections.photoSec = true;
        Sections.secondSec = true;
        Progress.nextSection = nameof(Sections.secondSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);

        if (GlobalVariables.CurrentCreatingID != 0)
        {
            var updateing = await _jobService.UpdateRecordDescription(new Models.RecordMetaData
            {
                Id = GlobalVariables.CurrentCreatingID,
                Progress = json
            });
            await Shell.Current.GoToAsync($"//JobListFlyOut/Page3_GeneralInfo");
        }
        else if (GlobalVariables.CurrentID != 0)
        {
            var updateing = await _jobService.UpdateRecordDescription(new Models.RecordMetaData
            {
                Id = GlobalVariables.CurrentID,
                Progress = json
            });
            await Shell.Current.GoToAsync($"//JobListFlyOut/Page3_GeneralInfo");
        }
    }


    [RelayCommand]
    public async Task Page4Navigation()
    {
        Progress.nextSection = nameof(Sections.thirdSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page4_PreviousTagInfo");
    }

    [RelayCommand]
    public async Task Page5Navigation()
    {
        Progress.nextSection = nameof(Sections.fourthSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page5_ServiceManufacturersData");
    }

    [RelayCommand]
    public async Task Page6Navigation()
    {
        Progress.nextSection = nameof(Sections.fifthSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page6_TestResults");
    }

    [RelayCommand]
    public async Task Page7Navigation()
    {
        Progress.nextSection = nameof(Sections.sixthSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page7_RepairReport_1_");
    }

    [RelayCommand]
    public async Task Page8Navigation()
    {
        Progress.nextSection = nameof(Sections.seventhSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page8_RepairReport_2_");
    }


    [RelayCommand]
    public async Task Page9Navigation()
    {
        Progress.nextSection = nameof(Sections.textareaSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page9_Comments");
    }

    [RelayCommand]
    public async Task Page10Navigation()
    {
        Progress.nextSection = nameof(Sections.eighthSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page10_FinalSettings");
    }

    [RelayCommand]
    public async Task Page11Navigation()
    {
        Progress.nextSection = nameof(Sections.ninthSec);
        Progress.sections = Sections;
        string json = JsonConvert.SerializeObject(Progress);
        await Shell.Current.GoToAsync($"//JobListFlyOut/Page11_FinalAssembly");
    }


    [ObservableProperty]
    public ButtonsColors _ButtonColor = new ButtonsColors();

    [RelayCommand]
    public void ColorsCompare()
    {
        if (Sections.photoSec == true) ButtonColor.photoSecColor = Colors.Black;
        if (Sections.secondSec == true) ButtonColor.secondSecColor = Colors.Black;
        if (Sections.thirdSec == true) ButtonColor.thirdSecColor = Colors.Black;
        if (Sections.fourthSec == true) ButtonColor.fourthSecColor = Colors.Black;
        if (Sections.fifthSec == true) ButtonColor.fifthSecColor = Colors.Black;
        if (Sections.sixthSec == true) ButtonColor.sixthSecColor = Colors.Black;
        if (Sections.seventhSec == true) ButtonColor.seventhSecColor = Colors.Black;
        if (Sections.textareaSec == true) ButtonColor.textareaSecColor = Colors.Black;
        if (Sections.eighthSec == true) ButtonColor.eightSecColor = Colors.Black;
        if (Sections.ninthSec == true) ButtonColor.ninthSecColor = Colors.Black;
    }

    
}
