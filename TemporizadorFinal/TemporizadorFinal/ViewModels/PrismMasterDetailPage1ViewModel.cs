using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TemporizadorFinal.ViewModels
{
    public class PrismMasterDetailPage1ViewModel : BindableBase
    {
#region PRIVATEFIELDS
        private INavigationService _navigationService;
        private DelegateCommand<string> _navigateCommand;
#endregion
        public DelegateCommand<string> NavigateCommand {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(async (string page) => {
                await _navigationService.NavigateAsync("/PrismMasterDetailPage1/NavigationPage/"+page);
            })); }
        public PrismMasterDetailPage1ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        
    }
}
