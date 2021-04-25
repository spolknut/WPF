using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WpfTreeView.Directory;

namespace WpfTreeView
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region Constructor
    public MainWindow()
    {
      InitializeComponent();
    }
    #endregion

    #region Folder Expanded
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Item_Expanded(object sender, RoutedEventArgs e)
    {
     
    }

    #endregion
  }
}
