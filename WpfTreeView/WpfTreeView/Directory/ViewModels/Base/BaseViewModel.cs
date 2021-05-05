using PropertyChanged;
using System.ComponentModel;

namespace WpfTreeView
{
    /// <summary>
    /// A base ViewModel that fires PropertyChanged as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any inherited class property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}

