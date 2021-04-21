using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.Generic;

namespace TreeView
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

    #region On Loaded
    /// <summary>
    /// When the application first opens
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      foreach (string drive in Directory.GetLogicalDrives())
      {
        var item = new TreeViewItem()
        {
          // Set header 
          Header = drive,

          // Set full path 
          Tag = drive
        };

        // Add dummy item to get expand
        item.Items.Add(null);

        // Listen for item being expanded
        item.Expanded += Item_Expanded;


        // Add item to tree view
        this.FolderView.Items.Add(item);
      }
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
      #region Initial Checks
      var item = (TreeViewItem)sender;

      if (item.Items.Count > 1 || item.Items[0] != null)
        return;

      // Clear dummy data
      item.Items.Clear();

      // Get full path 
      string fullPath = (string)item.Tag;

      #endregion

      #region Get folders

      var directories = new List<string>();

      // Try to get directories from the folder
      // Ignoring all issues; bad practice in most cases 
      try
      {
        var dirs = Directory.GetDirectories(fullPath);

        foreach (string directoryPath in dirs)
        {
          var subItem = new TreeViewItem()
          {
            // Set header to folder name 
            Header = GetFolderFileName(directoryPath),

            // Set full path 
            Tag = directoryPath
          };

          // Add dummy path to allow expand folder
          subItem.Items.Add(null);

          // Handle expanding
          subItem.Expanded += Item_Expanded;

          // Add to parent node
          item.Items.Add(subItem);
        }
      }
      catch { }

      #endregion

      #region Get Files
      var files = new List<string>();

      // Try to get files from the folder
      // Ignoring all issues; bad practice in most cases 
      try
      {
        var dirs = Directory.GetFiles(fullPath);

        foreach (string filePath in dirs)
        {
          var subItem = new TreeViewItem()
          {
            // Set header to file name 
            Header = GetFolderFileName(filePath),

            // Set full path 
            Tag = filePath
          };

          // Add to parent node
          item.Items.Add(subItem);
        }
      }
      catch { }
      #endregion
    }

    #endregion

    #region Helpers
    /// <summary>
    /// Find the folder or file name from a full path
    /// </summary>
    /// <param name="fullPath"></param>
    /// <returns></returns>
    public static string GetFolderFileName(string fullPath)
    {
      // If we have no path, return empty
      if (string.IsNullOrEmpty(fullPath))
        return string.Empty;

      // Make all slashes back slashes
      string normalizePath = fullPath.Replace('/', '\\');

      // Find last backslash
      int lastIndex = normalizePath.LastIndexOf('\\');

      // If no backslash, return empty
      if (lastIndex == -1)
        return string.Empty;

      // Extract name after last backslash
      var fileFolderName = normalizePath[(lastIndex + 1)..];
      return fileFolderName;
    }
    #endregion
  }
}
