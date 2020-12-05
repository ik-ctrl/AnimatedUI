using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AnimatedUI.Annotations;
using AnimatedUI.Source.Infrastructure;

namespace AnimatedUI.Source.ViewModels
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Хранит ссылку на выбранную viewmodel
        /// </summary>
        private object _selectedViewModel;

        public NavigationViewModel(object selectedViewModel)
        {
            _selectedViewModel = selectedViewModel;
        }

        public NavigationViewModel() { }

        /// <summary>
        /// Для привязки выбранной модели
        /// </summary>
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        #region command
        public RelayCommand SetHomeViewModelCommand =>  new RelayCommand(SetHomeViewModel);

        public void SetHomeViewModel()
        {
            SelectedViewModel = null;
            SelectedViewModel = new HomeViewModel();
        }

        public RelayCommand SetRainViewModelCommand => new RelayCommand(SetRainViewModel);
        
        public void SetRainViewModel()
        {
            SelectedViewModel = null;
            SelectedViewModel = new RainViewModel();
        }

        public RelayCommand SetSettingsViewModelCommand => new RelayCommand(SetSettingsViewModel);
        public void SetSettingsViewModel()
        {
            SelectedViewModel = null;
            SelectedViewModel = new SettingsViewModel();
        }
        #endregion
        
        #region notify
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
