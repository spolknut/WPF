using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WpfTreeView.Directory;

namespace WpfTreeView
{
  [ValueConversion(typeof(string), typeof(BitmapImage))]
  public class HeaderToImageConverter : IValueConverter
  {
    public static HeaderToImageConverter Instance = new HeaderToImageConverter();
    /// <summary>
    /// Convert a full path to a specific image path of a drive, folder or file
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      // Get full path
      var fullPath = (string)value;

      // If the path is null, ignore
      if (fullPath == null)
        return null;

      // Get name of file/folder
      var name = DirectoryStructure.GetFolderFileName(fullPath);

      // By default, presume an image
      string image = "file.png";

      // If the name is blank, assume drive as we cannot have blank file or folder names
      if (String.IsNullOrEmpty(name))
      {
        image = "drive.png";
      }
      else if (new FileInfo(fullPath).Attributes.HasFlag(FileAttributes.Directory))
      {
        image = "folder-closed.png";
      }

      return new BitmapImage(new Uri($"pack://application:,,,/Images/{image}"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
