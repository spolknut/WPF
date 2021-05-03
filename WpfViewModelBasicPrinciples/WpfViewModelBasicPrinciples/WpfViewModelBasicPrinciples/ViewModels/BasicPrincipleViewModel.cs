using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewModelBasicPrinciples.ViewModels
{
    /// <summary>
    /// This is an example to demonstrate the basic concept of a ViewModel.
    /// There are tools which can automatically generate this pattern, e.g. PropertyChanged.Fody, but this example demonstrates what goes on behind the scene.
    /// 
    /// A ViewModel is nothing more than a class which implements INotifyPropertyChanged, and which fires PropertyChanged events whenever one of its properties is changed.
    /// A ViewModel can therby be completely unaware of the UI.    
    /// 
    /// The properties in a ViewModel is completely unaware of the UI, jet, will work in a UI.
    /// 
    /// The UI must have its DataContext set to the ViewModel class, this is typically done in the MainWindow constructor.
    /// e.g.  this.DataContext = new BasicPrincipleViewModel();
    /// 
    /// UI objects can then be bound to the ViewModel via '{binding}', either directly to the ViewModel class or public properties in it.
    /// e.g.  <Button Content={Binding ExampleProperty></Button>
    /// 
    /// Note that this pattern must be done for every property which becomes a lot of filler code.
    /// </summary>
    public class BasicPrincipleViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///  Propery changed event handler, tells external part that a propery in this class context has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Private Properties
        private string exampleProperty;
        #endregion

        #region Class Constructor
        /// <summary>
        ///  For the sake of the example a Task is started which changes a class property value every 500 ms 
        /// </summary>
        public BasicPrincipleViewModel()
        {
            int i = 0;
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(500);
                    this.ExampleProperty = (i++).ToString();
                }
            });
        }
        #endregion

        #region Example Property
        /// <summary>
        /// Example property for the ViewModel, 
        /// There is no magic going on. 
        /// When the value is set, the setter calls the PropertyChanged event handler, which notifies the UI that the property value has changed and that it should reflect the new value.
        /// This pattern allows an UI object which is bound to 'ExampleProperty' to be updated each time the property value changes.  
        /// </summary>
        public string ExampleProperty
        {
            get
            {
                return exampleProperty;
            }
            set
            {
                // Avoid setting same value and below calling ProperyChanged
                if (exampleProperty == value)
                    return;

                exampleProperty = value;

                // Call PropertyChanged for this specific property, notifies bound UI that it has been changed and a new value displayed
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ExampleProperty)));
            }
        }
        #endregion
    }
}
