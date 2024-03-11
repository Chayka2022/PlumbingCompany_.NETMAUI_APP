using PlumbingCompany.Views;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PlumbingCompany.Models;
using PlumbingCompany.Services;
using PlumbingCompany.Constants;
using CommunityToolkit.Maui.Core.Extensions;
using System.Runtime;
using System.Globalization;

namespace PlumbingCompany.Data
{
    public partial class JobListViewModel : ObservableObject
    {

        [ObservableProperty]
        bool _IsBusy;

        [ObservableProperty]
        bool _IsntBusy;

        [ObservableProperty]
        bool _Sending = true;

        [ObservableProperty]
        int _WebPageButtonPlace = 2;

        [ObservableProperty]
        int _SyncronizrionLabelPlace = 1;

        [ObservableProperty]
        bool _IsSeen;

        [ObservableProperty]
        bool _ButtonIsSeen = true;

        [ObservableProperty]
        UserCheck _UserCheck = new UserCheck();

        [ObservableProperty]
        int x = 1;

        [ObservableProperty]
        int y = 1;

        [ObservableProperty]
        string _SendingProgress;

        int result;

        [ObservableProperty]
        Page1Data page1Data = new Page1Data();

        public static List<Page1Data> JobsListForSearch { get; private set; } = new List<Page1Data>();
        public ObservableCollection<Page1Data> Jobs { get; set; } = new ObservableCollection<Page1Data>();

        

        public Page1Data SelectedJob { get; set; }

        private readonly IJobService _jobService;

        public JobListViewModel(IJobService jobService)
        {
            Sending = false;
            _jobService = jobService;
            Sending = true;
        }


        Page1ViewModel page1ViewModel { get; set; }

        [RelayCommand]
        public async Task GetJobList()
        {
            IsBusy = true;
            Sending = !IsBusy;
            GlobalVariables.CurrentCreatingID = 0;

            //await Task.Delay(800); 
            Jobs.Clear();
            var jobList = await _jobService.GetJobList();
            if (jobList?.Count > 0)
            {
                jobList = jobList.OrderByDescending(f => f.Id).ToList();
                foreach (var job in jobList)
                {
                    Jobs.Add(job);
                }
                JobsListForSearch.Clear();
                JobsListForSearch.AddRange(jobList);
            }
            IsBusy = false;
            Sending = !IsBusy;
        }

        [RelayCommand]
        public async Task SendPage1Data(Page1Data page1Data)
        {
            Sending = false;

            SendingProgress = ("Synchronization... 0% " + X + "/" + Y);

            IsSeen = !Sending;
            ButtonIsSeen = !IsSeen;
            SyncronizrionLabelPlace = 2;
            WebPageButtonPlace = 1;

            await _jobService.SendPage1DataToServer(page1Data);
            var progressInfo = _jobService.GetRecordMetaDataForDeserealization(page1Data.Id);

            

            progressInfo.RowID = page1Data.RowID;
            await _jobService.SendProgressInfo(progressInfo);

            SendingProgress = ("Synchronization... 10% " + X + "/" + Y);

            var imageList = await _jobService.GetImagesList(page1Data.Id);
            if (imageList?.Count > 0)
            {
                
                imageList = imageList.ToList();
                foreach (var image in imageList)
                {
                    if (image.RowID == null || image.RowID == "" || image.RowID == "0")
                    {
                        image.RowID = page1Data.RowID;
                        await _jobService.SendImageDataToServer(image);
                    }
                }
            }

            SendingProgress = ("Synchronization... 20% " + X + "/" + Y);

            var page3Data = _jobService.GetPage3_Data(page1Data.Id);
            if (page3Data != null)
            {
                page3Data.RowID = page1Data.RowID;
                await _jobService.SendPage3DataToServer(page3Data);
            }

            SendingProgress = ("Synchronization... 30% " + X + "/" + Y);

            await _jobService.SendProgressInfo(progressInfo);

            var page4Data = _jobService.GetPage4_Data(page1Data.Id);
            if ( page4Data != null )
            {
                page4Data.RowID = page1Data.RowID;
                await _jobService.SendPage4DataToServer(page4Data);
            }

            SendingProgress = ("Synchronization... 40% " + X + "/" + Y);

            await _jobService.SendProgressInfo(progressInfo);

            var page5Data = _jobService.GetPage5_Data(page1Data.Id);
            if (page5Data != null)
            {
                page5Data.RowID = page1Data.RowID;
                await  _jobService.SendPage5DataToServer(page5Data);
            }

            SendingProgress = ("Synchronization... 50% " + X + "/" + Y);

            await _jobService.SendProgressInfo(progressInfo);

            var page6Data = _jobService.GetPage6_Data(page1Data.Id);
            if (page6Data != null)
            {
                page6Data.RowID = page1Data.RowID;
                await _jobService.SendPage6DataToServer(page6Data);
            }

            SendingProgress = ("Synchronization... 60% " + X + "/" + Y);

            await _jobService.SendProgressInfo(progressInfo);

            var page7Data = _jobService.GetPage7_Data(page1Data.Id);
            if (page7Data != null)
            {
                page7Data.RowID = page1Data.RowID;
               await _jobService.SendPage7DataToServer(page7Data);
            }

            SendingProgress = ("Synchronization... 70% " + X + "/" + Y);

            await _jobService.SendProgressInfo(progressInfo);

            var page8Data = _jobService.GetPage8_Data(page1Data.Id);
            if (page8Data != null)
            {
                page8Data.RowID = page1Data.RowID;
                await _jobService.SendPage8DataToServer(page8Data);
            }

            await _jobService.SendProgressInfo(progressInfo);

            SendingProgress = ("Synchronization... 80% " + X + "/" + Y);

            var page9Data = _jobService.GetPage9_Data(page1Data.Id);
            if (page9Data != null)
            {
                page9Data.RowID = page1Data.RowID;
                await _jobService.SendPage9DataToServer(page9Data);
            }

            await _jobService.SendProgressInfo(progressInfo);

            var page10Data = _jobService.GetPage10_Data(page1Data.Id);
            if (page10Data != null)
            {
                page10Data.RowID = page1Data.RowID;
                await _jobService.SendPage10DataToServer(page10Data);
            }

            SendingProgress = ("Synchronization... 90% " + X + "/" + Y);

            await _jobService.SendProgressInfo(progressInfo);

            var page11Data = _jobService.GetPage11_Data(page1Data.Id);
            if (page11Data != null)
            {
                page11Data.RowID = page1Data.RowID;
                await _jobService.SendPage11DataToServer(page11Data);
            }

            await _jobService.SendProgressInfo(progressInfo);

            if (GlobalVariables.Page1Sent == 1)
            {
                if(GlobalVariables.Page2Sent == 1)
                {
                    if (GlobalVariables.Page3Sent == 1)
                    {
                        if (GlobalVariables.Page4Sent == 1)
                        {
                            if (GlobalVariables.Page5Sent == 1)
                            {
                                if (GlobalVariables.Page6Sent == 1)
                                {
                                    if (GlobalVariables.Page7Sent == 1)
                                    {
                                        if (GlobalVariables.Page8Sent == 1)
                                        {
                                            if (GlobalVariables.Page9Sent == 1)
                                            {
                                                if (GlobalVariables.Page10Sent == 1)
                                                {
                                                    if (GlobalVariables.Page11Sent == 1)
                                                    {
                                                        if (GlobalVariables.ProgressSent == 1)
                                                        {
                                                            await _jobService.UpdateJob(new Models.Page1Data
                                                            {
                                                                Id = page1Data.Id,
                                                                JobOrder = page1Data.JobOrder,
                                                                Item = page1Data.Item,
                                                                Customer = page1Data.Customer,
                                                                Customer_ID = page1Data.Customer_ID,
                                                                Customer_Location_ID = page1Data.Customer_Location_ID,
                                                                RowID = page1Data.RowID,
                                                                Synchronized = 1

                                                            });
                                                            //await Shell.Current.DisplayAlert("Data was sent ☭", "Saved to server ✔️ Keep working harder, comrade!", "OK");
                                                        }
                                                        else { await Shell.Current.DisplayAlert("Something went wrong", "Eleventh page data wasn't saved on server ❌", "OK"); result = 0; }
                                                    }
                                                    else { await Shell.Current.DisplayAlert("Something went wrong", "Eleventh page data wasn't saved on server ❌", "OK"); result = 0; }
                                                }
                                                else { await Shell.Current.DisplayAlert("Something went wrong", "Tenth page data wasn't saved on server ❌", "OK"); result = 0; }
                                            }
                                            else { await Shell.Current.DisplayAlert("Something went wrong", "Ninth page data wasn't saved on server ❌", "OK"); result = 0; }
                                        }
                                        else { await Shell.Current.DisplayAlert("Something went wrong", "Eighth page data wasn't saved on server ❌", "OK"); result = 0; }
                                    }
                                    else { await Shell.Current.DisplayAlert("Something went wrong", "Seventh page data wasn't saved on server ❌", "OK"); result = 0; }
                                }
                                else { await Shell.Current.DisplayAlert("Something went wrong", "Sixth page data wasn't saved on server ❌", "OK"); result = 0; }
                            }
                            else { await Shell.Current.DisplayAlert("Something went wrong", "Fifth page data wasn't saved on server ❌", "OK"); result = 0; }
                        }
                        else { await Shell.Current.DisplayAlert("Something went wrong", "Fourth page data wasn't saved on server ❌", "OK"); result = 0; }
                    }
                    else { await Shell.Current.DisplayAlert("Something went wrong", "Third page data wasn't saved on server ❌", "OK"); result = 0; }
                }
                else { await Shell.Current.DisplayAlert("Something went wrong", "Second page data wasn't saved on server ❌", "OK"); result = 0; }
            }
            else { await Shell.Current.DisplayAlert("Something went wrong", "First page data wasn't saved on server ❌", "OK"); result = 0; }

            SendingProgress = ("Synchronization... 100% " + X + "/" + Y);

            await Task.Delay(1000);

            await GetJobList();

            Sending = true;
            IsSeen = !Sending;
            ButtonIsSeen = !IsSeen;
            SyncronizrionLabelPlace = 1;
            WebPageButtonPlace = 2;
        }

        [RelayCommand]
        private async Task DeleteButtonTaped(Page1Data page1Data) 
        {
            var result = await Shell.Current.DisplayAlert("❌ DELETE this record from the local device?", "This will not delete the record from the server", "YES", "Cancel");
            if (result)
            {
                _jobService.DeleteJob(page1Data);
                await GetJobList();
            }
            else
            {
                return;
            }

        }

        [RelayCommand]
        public async Task CreateNewJob() 
        {
            GlobalVariables.CurrentCreatingID = 0;
            GlobalVariables.CurrentID = 0;
            await Shell.Current.GoToAsync($"//JobListFlyOut/Page1_JobOrder");
        }

        [RelayCommand]
        async void GetCustomersList()
        {
            Sending = false;
            var result = await _jobService.GetCustomers();
            if (result > 0) await _jobService.CreateCustomersAdressTable();
            Sending = true;
        }

        [RelayCommand]
        async void OpenWebPage()
        {
            await Shell.Current.GoToAsync($"//JobListFlyOut/WebView");
        }

        [RelayCommand]
        async Task Logout()
        {
            var result = await Shell.Current.DisplayAlert("Logout", "Are you shure you want to logout?", "Yes", "Cancel");
            if (result == true)
            {
                UserCheck.Id = 1;
                UserCheck.LastLoginDate = DateTime.MinValue;
                await _jobService.UpdateLoginData(UserCheck);
                GlobalVariables.LoginResponse = null;
                await Shell.Current.GoToAsync($"///LoginPage");
            }
            else return; 
        }

        [RelayCommand]
        async void SendAllRecordsToServer()
        {
            Sending = false;
            result = 1;
            var jobList = await _jobService.GetJobList();
            Y = jobList.Count;
            X = 0;
            foreach (Page1Data i in jobList)
            {
                X++;
                try
                {
                    if (i.isSynchronized == false)
                    {
                        Sending = false;
                        await SendPage1Data(i);
                    }
                }
                catch (Exception e)
                {
                    result = 0;
                }
            }
            if(result != 0) 
            {
#if ANDROID || IOS
                await Toast.Make("All records successfully synchronized ✔", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
#endif
#if WINDOWS
                await Shell.Current.DisplayAlert("Synchronized ✔", "All records successfully synchronized", "Ok");
#endif
            }
            else
            {
#if ANDROID || IOS
                await Toast.Make("Not all records were synchronized ❌", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
#endif
#if WINDOWS
                await Shell.Current.DisplayAlert("Synchronization ❌", "Not all records were synchronized", "Ok");
#endif
            }
            X = 1;
            Y = 1;
            Sending = true;
        }

        [RelayCommand]
        async void DeleteAllRecords()
        {
            var result = await Shell.Current.DisplayAlert("❌ DELETE all records from the local device?", "This will not delete them from the server", "YES", "Cancel");
            if (result)
            {
                var jobList = await _jobService.GetJobList();
                foreach (Page1Data i in jobList)
                {
                    _jobService.DeleteJob(i);
                }
                await GetJobList();
#if ANDROID || IOS
                await Toast.Make("All records successfully deleted", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
#endif
#if WINDOWS
                await Shell.Current.DisplayAlert("Deleted", "All records successfully deleted", "Ok");
#endif
            }
            else
            {
                return;
            }
        }
    }
}
