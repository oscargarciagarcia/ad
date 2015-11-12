using System;
using Gtk;
using System.Collections;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Articulo";
		Console.WriteLine ("MainWindow ctor.");
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);


		newAction.Activated += delegate {
			new ArticuloView();
		};

		refreshAction.Activated += delegate {
			fillTreeView();
		};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine ("click en deleteAction id={0}", id);
			delete (id);

		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine ("ha ocurrido un treeVIew.Selection.Change");
			deleteAction.Sensitive = GetId (treeView) != null;
		};
		//newAction.Activated += newActionActivated;
	}
		private void fillTreeView() {
			QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
			TreeViewHelper.Fill (treeView, queryResult);
		}
	private void  delete(object id) {
		if (ConfirmDelete (this))
			Console.WriteLine ("Dice que eliminar si");
	}

	public bool ConfirmDelete(Window window) {
		MessageDialog messageDialog = new MessageDialog (
			window, 
			DialogFlags.DestroyWithParent,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el elemento seleccionado?"
			);
		messageDialog.Title = window.Title;
		ResponseType response = (ResponseType)messageDialog.Run ();
		messageDialog.Destroy ();
		return response == ResponseType.Yes;
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