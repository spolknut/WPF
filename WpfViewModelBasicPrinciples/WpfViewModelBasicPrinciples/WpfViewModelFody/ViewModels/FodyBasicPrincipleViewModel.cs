using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewModelFody.ViewModels
{
    /// <summary>
    /// This is an example to demonstrate the basic concept of a ViewModel using PropertyChanged.Fody.
    /// In this example this is done by inheriting a base class which implements ViewModel with PropertyChanged.Fody.
    /// 
    /// A ViewModel is nothing more than a class which implements INotifyPropertyChanged, and which fires PropertyChanged events whenever one of its properties is changed.
    /// A ViewModel can therby be completely unaware of the UI.    
    /// 
    /// PropertyChanged.Fody makes it a lot easier to create the desired PropertyChanged pattern with significantly less lines of code.
    /// Instead of manually writing the PropertyChanged event for each Property, Fody injects it for us during compile time.
    /// Fody will automatically make sure the event PropertyChanged is called for each property in the class.
    /// 
    /// The class requires [AddINotifyPropertyChangedInterface] for Fody to be able to do its work.
    /// By implementing a base class inheriting INotifyPropertyChanged with [AddINotifyPropertyChangedInterface] specified, it gets really simple to create new ViewModel classes.
    /// 
    /// The properties in a ViewModel is completely unaware of the UI, jet, will work in a UI.
    /// The UI must have its DataContext set to the ViewModel class, this is typically done in the MainWindow constructor.
    /// e.g.  this.DataContext = new FodyBasicPrincipleViewModel();
    /// 
    /// UI objects can then be bound to the ViewModel via '{binding}', either directly to the ViewModel class or public properties in it.
    /// e.g.  <Button Content={Binding ExampleProperty></Button>
    /// </summary>
    public class FodyBasicPrincipleViewModel : BaseViewModel
    {
        #region Example Property
        /// <summary>
        /// Example property for the ViewModel, PropertyChanged.Fody will inject the basic PropertyChanged event behaviour for us 
        /// </summary>
        public string ExampleProperty { get; set; } = "0";
        #endregion

        #region Class Constructor
        /// <summary>
        ///  For the sake of the example a Task is started which changes a class property value every 500 ms 
        /// </summary>
        public FodyBasicPrincipleViewModel()
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
    }
}
