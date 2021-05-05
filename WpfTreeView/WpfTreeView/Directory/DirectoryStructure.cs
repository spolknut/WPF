using System.Collections.Generic;
using System.Linq;

namespace WpfTreeView
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
            #region Get folders

            var directoryItems = new List<DirectoryItem>();

            // Try to get directories from the folder
            // Ignoring all issues; bad practice in most cases 
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directoryItems.AddRange(dirs.Select(dir => new DirectoryItem() { Type = DirectoryItemType.Folder, FullPath = dir }));
            }
            catch { }


            #endregion

            #region Get Files

            // Try to get files from the folder
            // Ignoring all issues; bad practice in most cases 
            try
            {
                var files = System.IO.Directory.GetFiles(fullPath);

                if (files.Length > 0)
                    directoryItems.AddRange(files.Select(file => new DirectoryItem() { Type = DirectoryItemType.File, FullPath = file }));
            }
            catch { }
            #endregion

            return directoryItems;
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
