using Newtonsoft.Json;
using Plugin.SimpleAudioPlayer;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using TemporizadorFinal.Models;
using TemporizadorFinal.Services;
using Xamarin.Essentials;

namespace TemporizadorFinal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private Configuration configuration;
        private TimeSpan _currentTimer;
        private static int REFRESH_PERIOD = 137;
        private string _iterationsDisplay;
        public string IterationsDisplay { get => _iterationsDisplay; set => SetProperty(ref _iterationsDisplay, value); }
        public TimeSpan CurrentTimer { get => _currentTimer; set => SetProperty(ref _currentTimer, value); }
        private bool  isExam;
        private int _currentIteration;
        private int CurrentIteration { get => _currentIteration;
            set
            {
                SetProperty(ref _currentIteration, value);
                IterationsDisplay = $"Posta {_currentIteration} de {configuration.NumberOfIntervals}";
            }
        }
        private string _buttonText;
        public string ButtonText { get => _buttonText; set => SetProperty(ref _buttonText, value); }
        private DelegateCommand _startStopCommand, _resetCommand;
        private static System.Timers.Timer aTimer;


        ISimpleAudioPlayer AudioPlayer;
        private IHC05Driver _hC05Driver;

        public DelegateCommand StartStopCommand { get => _startStopCommand ?? (_startStopCommand = new DelegateCommand(StartStopCommand_Execute)); }

       
        public DelegateCommand ResetCommand { get => _resetCommand ?? (_resetCommand = new DelegateCommand(ResetCommand_Execute)); }

       

        public MainPageViewModel(INavigationService navigationService, IHC05Driver hC05Driver)
            : base(navigationService)
        {
            Title = "Temporizador";
            aTimer = new System.Timers.Timer(REFRESH_PERIOD);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            AudioPlayer =CrossSimpleAudioPlayer.Current;
            _hC05Driver = hC05Driver;
            _hC05Driver.Initialize("HC-05");


        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            ResetCommand_Execute();
        }
        private void StartStopCommand_Execute()
        {
            aTimer.Enabled = !aTimer.Enabled;
            if (aTimer.Enabled)
            {
                ButtonText = "Pausar";
                
            }
            else
            {
                ButtonText = "Iniciar";
            }
        }
        private void ResetCommand_Execute()
        {
            aTimer.Stop();
            var configJson = Preferences.Get("CONFIGURATION", "");
            if (string.IsNullOrEmpty(configJson))
            {
                configuration = new Configuration();
            }
            else
            {
                configuration = JsonConvert.DeserializeObject<Configuration>(configJson);
            }
            isExam = false;
            CurrentIteration = 0;
            _currentTimer = configuration.PauseDuration;
            ButtonText = "Iniciar";
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            CurrentTimer -= TimeSpan.FromMilliseconds(REFRESH_PERIOD);
            if (CurrentTimer.TotalMilliseconds <= 0)
            {
                FireSound();
                _hC05Driver.Tx.WriteLine("StreamReader defaults to UTF-8 encoding unless specified otherwise, instead of defaulting to the ANSI code page for the current system. UTF-8 handles Unicode characters correctly and provides consistent results on localized versions of the operating system. If you get the current character encoding using the CurrentEncoding property, the value is not reliable until after the first Read method, since encoding auto detection is not done until the first call to a Read method.");
                var rx = _hC05Driver.Rx.ReadLine();
                isExam = !isExam;

                if (isExam)
                {
                    if (CurrentIteration++ == configuration.NumberOfIntervals)
                    {
                        normalFinish();
                    }
                    CurrentTimer = configuration.ExamDuration;
                }
                else
                {
                    CurrentTimer = configuration.PauseDuration;
                }
            }
        }

        private async void FireSound()
        {
            if (!AudioPlayer.IsPlaying)
            {
                AudioPlayer.Load("chime.mp3");
                AudioPlayer.Play();
            }
        }

        private void normalFinish()
        {
            ResetCommand_Execute();
        }
    }
}
