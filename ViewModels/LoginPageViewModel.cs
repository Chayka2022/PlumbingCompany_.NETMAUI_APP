using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlumbingCompany.Services;
using PlumbingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlumbingCompany.Constants;

namespace PlumbingCompany.Data
{
    public partial class LoginPageViewModel : ObservableObject
    {

        private readonly IJobService _jobService;

        public LoginPageViewModel(IJobService jobService)
        {
            _jobService = jobService;
        }

        [ObservableProperty]
        bool _IsEnabled = true;

        [ObservableProperty]
        public LoginData _loginData = new LoginData();

        [ObservableProperty]
        string _NickName;

        [ObservableProperty]
        string _Password;

        [ObservableProperty]
        UserCheck _UserCheck = new UserCheck();

        [RelayCommand]
        async void JobListGoTo()
        {
            IsEnabled = false;
            LoginData.username = NickName;
            LoginData.password = Password;
            await _jobService.SendLoginInformationToServer(LoginData);
            if (GlobalVariables.LoginResponse == null) await Shell.Current.DisplayAlert("Could not login!", "Check your internet connection and entered data", "OK");
            else if (GlobalVariables.LoginResponse.Contains("Login Successful.")) 
            { 
                /*await _jobService.CreateTables();*/ 
                await GetCustomersList(); 
                UserCheck.LastLoginDate = DateTime.Today; 
                UserCheck.Id = 1;
                await _jobService.UpdateLoginData(UserCheck); 
                await Shell.Current.GoToAsync($"///JobListFlyOut"); 
            }
            else { await Shell.Current.DisplayAlert("Could not login!", "Check your internet connection and entered data", "OK"); }
            IsEnabled = true;
        }

        [RelayCommand]
        async Task SetupDataBase()
        {
            await _jobService.SetUpDataBase();
        }

        [RelayCommand]
        public async void ResetData()
        {
            IsEnabled = false;
            NickName = null;
            Password = null;
            await SetupDataBase();
            await _jobService.CreateTables();
            UserCheck = _jobService.GetLoginData(1);
            if (UserCheck != null)
            {
                if (UserCheck.LastLoginDate < DateTime.Today.AddDays(-30))
                {
                    IsEnabled = true;
                    return;                    
                }
                else
                { 
                    await GoToJobLsitWithouLogin();
                }
            }
            else
            {
                UserCheck = new UserCheck();
                UserCheck.Id = 1;
                UserCheck.LastLoginDate = DateTime.Today;
                await _jobService.AddLoginData(UserCheck);   
            }
            IsEnabled = true;
        }

        [RelayCommand]
        async Task GetCustomersList()
        {
            var result = await _jobService.GetCustomers();
            if (result > 0) await _jobService.CreateCustomersAdressTable();
        }

        [RelayCommand]
        async Task GoToJobLsitWithouLogin()
        {
            await _jobService.CreateTables(); 
            await Shell.Current.GoToAsync($"///JobListFlyOut"); 
            await GetCustomersList(); 
            UserCheck.LastLoginDate = DateTime.Today; 
            UserCheck.Id = 1; 
            await _jobService.UpdateLoginData(UserCheck);
        }

        [RelayCommand]
        async void CrateTables()
        {
            await _jobService.CreateTables();
        }
    }
}
