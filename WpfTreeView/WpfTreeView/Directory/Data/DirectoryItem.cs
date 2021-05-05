using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTreeView
{
  /// <summary>
  /// Informaton about a directory item such a drive, a folder or a file
  /// </summary>
  public class DirectoryItem
  {
    /// <summary>
    /// The type of this item
    /// </summary>
    public DirectoryItemType Type { get; set; }

    /// The full path to this directory item
    /// </summary>
    public string FullPath { get; set; }

    /// <summary>
    /// The name o this directory item
    /// </summary>
    public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFolderFileName(this.FullPath); } }
  }
}
