using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBasics
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("DescriptionText " + this.DescriptionText.Text);
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
      this.cbWeld.IsChecked = false;
      this.cbAssembly.IsChecked = false;
      this.cbPlasma.IsChecked = false;
      this.cbLaser.IsChecked = false;
      this.cbPurchase.IsChecked = false;
      this.cbLathe.IsChecked = false;
      this.cbDrill.IsChecked = false;
      this.cbFold.IsChecked = false;
      this.cbRoll.IsChecked = false;
      this.cbSaw.IsChecked = false;
    }

    private void Checkbox_Checked(object sender, RoutedEventArgs e)
    {
      CheckBox cb = sender as CheckBox;
      if (cb.IsChecked == true)
      {
        TextBoxName.Text += cb.Content.ToString();
      }
      else
      {
        TextBoxName.Text = TextBoxName.Text.Replace(cb.Content.ToString(), "");
      }
    }

    private void ComboxFinish_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (TextBoxNote == null)
        return;

      ComboBox cb = ((ComboBox)sender);
      ComboBoxItem item = (ComboBoxItem)cb.SelectedValue;
      TextBoxNote.Text = item.Content.ToString();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      ComboxFinish_SelectionChanged(ComboxFinish, null);
    }
  }
}
