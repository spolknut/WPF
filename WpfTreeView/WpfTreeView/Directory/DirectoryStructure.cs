using System.Collections.Generic;
using System.Linq;
using WpfTreeView.Directory.Data;

namespace WpfTreeView.Directory
{

  /// <summary>
  /// A helper class to query information about directories
  /// </summary>
  public static class DirectoryStructure
  {
    #region Helpers

    /// <summary>
    /// Gets all logical drives on the computer
    /// </summary>
    /// <returns></returns>
    public static List<DirectoryItem> GetLogicalDrives()
    {
      // Get every logical drive on the machine
      return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive }).ToList();
    }

    /// <summary>
    /// Gets the directory top-level content
    /// </summary>
    /// <param name="fullPath">Full path to the directory</param>
    /// <returns></returns>
    public static List<DirectoryItem> GetDirectoryContent(string fullPath)
    {
      //#region Get folders

      var directoryItems = new List<DirectoryItem>();

      // Try to get directories from the folder
      // Ignoring all issues; bad practice in most cases 
      //try
      //{
        var dirs = System.IO.Directory.GetDirectories(fullPath);

        if (dirs.Length > 0)
        directoryItems.AddRange(dirs.Select(dir => new DirectoryItem() { Type = DirectoryItemType.Folder, FullPath = dir }));
      //  foreach (string directoryPath in dirs)
      //  {
      //    var subItem = new DirectoryItem()
      //    {
      //      // Set type to folder
      //      Type = DirectoryItemType.Folder,

      //      // Set full path 
      //      FullPath = directoryPath
      //    };

      //    // Add dummy path to allow expand folder
      //    subItem.Items.Add(null);

      //    // Handle expanding
      //    subItem.Expanded += Item_Expanded;

      //    // Add to parent node
      //    item.Items.Add(subItem);
      //  }
      //}
      //catch { }

      //#endregion

      //#region Get Files
      //var files = new List<string>();

      //// Try to get files from the folder
      //// Ignoring all issues; bad practice in most cases 
      //try
      //{
      //  var dirs = System.IO.Directory.GetFiles(fullPath);

      //  foreach (string filePath in dirs)
      //  {
      //    var subItem = new TreeViewItem()
      //    {
      //      // Set header to file name 
      //      Header = DirectoryStructure.GetFolderFileName(filePath),

      //      // Set full path 
      //      Tag = filePath
      //    };

      //    // Add to parent node
      //    item.Items.Add(subItem);
      //  }
      //}
      //catch { }
      //#endregion

      return null;
    }

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
