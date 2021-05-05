using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// Full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }


        /// <summary>
        /// Image name for the items type, closed or open as needed
        /// </summary>
        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        /// <summary>
        /// Name of the item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFolderFileName(this.FullPath); } }

        /// <summary>
        /// A list of all children contained in this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if this item is expanded
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                {
                    // Expand all children
                    Expand();
                }
                else
                {
                    this.ClearChildren();
                }
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of this item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            // Setup children as needed
            this.ClearChildren();
        }
        #endregion


        #region Public Commands

        /// <summary>
        /// Command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Removes all children from observable list, adding a dummy item to show expand if required
        /// </summary>
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand icon if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        /// <summary>
        /// Expands this item
        /// </summary>
        private void Expand()
        {
            // We cannot expand a file
            if (this.Type == DirectoryItemType.File)
                return;

            // Find all children 
            var children = DirectoryStructure.GetDirectoryContent(this.FullPath).Select(content => new DirectoryItemViewModel(content.FullPath, content.Type));
            this.Children = new ObservableCollection<DirectoryItemViewModel>(children);
        }

        #endregion
    }
}
