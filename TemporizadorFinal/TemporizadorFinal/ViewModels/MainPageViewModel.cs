using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TemporizadorFinal.Models;

namespace TemporizadorFinal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private Configuration configuration;
        private TimeSpan _currentCounter;
        private bool isRunning, isExam;
        private DelegateCommand _startStopCommand, _resetCommand;

        public DelegateCommand StartStopCommand { get => _startStopCommand ?? (_startStopCommand = new DelegateCommand(StartStopCommand_Execute)); }

       
        public DelegateCommand ResetCommand { get => _resetCommand ?? (_resetCommand = new DelegateCommand(ResetCommand_Execute)); }

       

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Temporizador";
        }
        private void StartStopCommand_Execute()
        {
            throw new NotImplementedException();
        }
        private void ResetCommand_Execute()
        {
            throw new NotImplementedException();
        }
    }
}
