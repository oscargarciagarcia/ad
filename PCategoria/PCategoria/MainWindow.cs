using System;
using Gtk;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Console.WriteLine ("MainWindow ctor.");
		fillTreeView ();

		newAction.Activated += delegate {
			new ArticuloView();
		};

		refreshAction.Activated += delegate {
			fillTreeView();
		};

		deleteAction.Activated += delegate {
			object id = GetId (treeView);
			Console.WriteLine ("click en deleteAction id={0}", id);
			delete (id);

		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine ("ha ocurrido un treeVIew.Selection.Change");
			deleteAction.Sensitive = GetId (treeView) != null;
		};
		//newAction.Activated += newActionActivated;
	}
//	private void fillTreeView() {
//		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
//		TreeViewHelper.Fill (treeView, queryResult);
//	}
	private void  delete(object id) {
		MessageDialog messageDialog = MessageDialog (
			this, 
			DialogFlags.DestroyWithParent,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el elemento seleccionado?"
		);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType)messageDialog.Run ();
		messageDialog.Destroy ();
		if (response == ResponseType.Yes)
			Console.WriteLine ("Eliminar, SI!");
	}

	public static object GetId(TreeView treeView) {
		TreeIter treeIter;
		if (!treeView.Selection.GetSelected (out treeIter))
			return null;
		IList row = (IList)treeView.Model.GetValue(treeIter, 0);
		return row[0];
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}