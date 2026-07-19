using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TreeViewUtilities
{
	/// <summary>
	/// Provides a generic mechanism for navigating the nodes in a TreeView control.  Call the ProcessTree method to 
	/// start the navigation process for an entire TreeView.  Call ProcessBranch to navigate only a subset of a TreeView's nodes.
	/// The ProcessNode event will fire for every node in the tree or branch, unless the processing is aborted before reaching the last node.
    /// For further info, see http://www.codeproject.com/Articles/12952/TreeViewWalker-Simplifying-Recursion
	/// </summary>
	public class TreeViewWalker
	{
		#region Data

		private TreeView treeView;
		private bool stopProcessing = false;

		#endregion // Data

		#region Constructors

		/// <summary>
		/// Creates an empty instance.  Set the TreeView property to a TreeView instance before calling ProcessTree.
		/// </summary>
		public TreeViewWalker()
		{
		}

		/// <summary>
		/// Creates an instance which references the specified TreeView.
		/// </summary>
		/// <param name="treeView">The TreeView to navigate.</param>
		public TreeViewWalker( TreeView treeView )
		{
			this.treeView = treeView;
		}

		#endregion // Constructors

		#region Public Interface

			#region ProcessNode [event]

		/// <summary>
		/// This event is raised when the TreeViewWalker navigates to a TreeNode in a TreeView.
		/// </summary>
		public event ProcessNodeEventHandler ProcessNode;

			#endregion // ProcessNode [event]

			#region ProcessBranch

		/// <summary>
		/// Navigates the node branch which starts with the specified node and fires the ProcessNode event for every TreeNode it encounters.
		/// The TreeNode passed to this method does not have to belong to the TreeView assigned to the TreeView property.
		/// </summary>
		/// <param name="rootNode"></param>
		public void ProcessBranch( TreeNode rootNode )
		{
			if( rootNode == null )
				throw new ArgumentNullException( "rootNode" );

			// Reset the abort flag in case it was previously set.
			this.stopProcessing = false;

			this.WalkNodes( rootNode );
		}

			#endregion // ProcessBranch

			#region ProcessTree

		/// <summary>
		/// Navigates the TreeView and fires the ProcessNode event for every TreeNode it encounters.
		/// </summary>
		public void ProcessTree()
		{
			if( this.TreeView == null )
				throw new InvalidOperationException( "The TreeViewWalker must reference a TreeView when ProcessTree is called." );

			foreach( TreeNode node in this.TreeView.Nodes )
			{
				this.ProcessBranch( node );
				if( this.stopProcessing )
					break;
			}
		}

			#endregion // ProcessTree

			#region TreeView

		/// <summary>
		/// Gets/sets the TreeView control to navigate.
		/// </summary>
		public TreeView TreeView
		{
			get { return this.treeView; }
			set { this.treeView = value; }
		}

			#endregion // TreeView

		#endregion // Public Interface

		#region Protected Interface

			#region OnProcessNode

		/// <summary>
		/// Raises the ProcessNode event.
		/// </summary>
		/// <param name="e">The event argument.</param>
		protected virtual void OnProcessNode( ProcessNodeEventArgs e )
		{
			ProcessNodeEventHandler handler = this.ProcessNode;
			if( handler != null )
				handler( this, e );
		}

			#endregion // OnProcessNode

		#endregion // Protected Interface

		#region Private Helpers
			
			#region WalkNodes

		private bool WalkNodes( TreeNode node )
		{
			// Fire the ProcessNode event.
			ProcessNodeEventArgs args = ProcessNodeEventArgs.CreateInstance( node );
			this.OnProcessNode( args );

			// Cache the value of ProcessSiblings since ProcessNodeEventArgs is a singleton.
			bool processSiblings = args.ProcessSiblings;

			if( args.StopProcessing )
			{
				this.stopProcessing = true;
			}
			else if( args.ProcessDescendants )
			{
				for( int i = 0; i < node.Nodes.Count; ++i )
					if( ! this.WalkNodes( node.Nodes[i] ) || this.stopProcessing )
						break;
			}

			return processSiblings;
		}

			#endregion // WalkNodes

		#endregion // Private Helpers
	}
}