using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TemporizadorFinal.Models;
using Xamarin.Essentials;

namespace TemporizadorFinal.ViewModels
{
    public class ConfigurationPageViewModel : ViewModelBase
    {


        #region PRIVATE_FIELDS
        private DelegateCommand _acceptCommand;
        private DelegateCommand _cancelCommand;
        private Configuration _configuration;
        private int _examMinutes;
        private int _examSeconds;
        private int _pauseMinutes;
        private int _pauseSeconds;
        #endregion

        #region PUBLIC_DECLARATIONS
        public int ExamMinutes { get => _examMinutes; set => SetProperty(ref _examMinutes, value); }
        public int ExamSeconds { get => _examSeconds; set {
                var temp = value;
                while (temp >= 60)
                {
                    ExamMinutes++;
                    temp -= 60;
                }
                SetProperty(ref _examSeconds, temp);
            } }
        public int PauseMinutes { get => _pauseMinutes; set => SetProperty(ref _pauseMinutes, value); }
        public int PauseSeconds { get => _pauseSeconds; set
            {
                var temp = value;
                while (temp >= 60)
                {
                    PauseMinutes++;
                    temp -= 60;
                }
                SetProperty(ref _pauseSeconds, temp);
            }
        }
        public Configuration Configuration{ get => _configuration; set => SetProperty(ref _configuration, value); }
        public DelegateCommand AcceptCommand { get => _acceptCommand ?? (_acceptCommand = new DelegateCommand(AcceptCommand_Execute)); }
        public DelegateCommand CancelCommand { get => _cancelCommand ?? (_cancelCommand = new DelegateCommand(async() => await NavigationService.NavigateAsync("MainPage"))); }


        #endregion
        public ConfigurationPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Configuration = new Configuration();
            ExamMinutes = Configuration.ExamDuration.Minutes;
            ExamSeconds = Configuration.ExamDuration.Seconds;
            PauseMinutes = Configuration.PauseDuration.Minutes;
            PauseSeconds = Configuration.PauseDuration.Seconds;
        }
        
        private async void AcceptCommand_Execute()
        {
            Configuration.ExamDuration = TimeSpan.FromSeconds(ExamMinutes * 60 + ExamSeconds);
            Configuration.PauseDuration = TimeSpan.FromSeconds(PauseMinutes * 60 + PauseSeconds);
            string json = JsonConvert.SerializeObject(Configuration);
            Preferences.Set("CONFIGURATION", json);
            await NavigationService.NavigateAsync("MainPage");


        }
    }
}
