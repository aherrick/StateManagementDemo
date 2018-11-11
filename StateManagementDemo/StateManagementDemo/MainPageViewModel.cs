using MvvmHelpers;

namespace StateManagementDemo
{
    public class MainPageViewModel : BaseViewModel
    {
        private eState _state;

        public eState State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        public enum eState
        {
            Loading,
            Loaded,
            Error
        }
    }
}