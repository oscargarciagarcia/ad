using System;
using System.Collections;
using System.Data;
using Gtk;

using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Title = "Artículo";
		Console.WriteLine ("MainWindow ctor.");
		fillTreeView ();

		newAction.Activated += delegate {
			new ArticuloView();
		};

		refreshAction.Activated += delegate {
			fillTreeView();
		};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine("click en deleteAction id={0}", id);
			delete(id);

		};

		editAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);

			new ArticuloView(id);
		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine("ha ocurrido treeView.Selection.Changed");
			bool isSelected = TreeViewHelper.IsSelected(treeView);
			deleteAction.Sensitive = isSelected;
			editAction.Sensitive = isSelected;
		};



		deleteAction.Sensitive = false;
		editAction.Sensitive = false;

		//newAction.Activated += newActionActivated;
	}

	private void delete(object id) {
		if (!WindowHelper.ConfirmDelete (this))
			return;
		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "delete from articulo where id = @id";
		DbCommandHelper.addParameter (dbCommand, "id", id);
		dbCommand.ExecuteNonQuery ();
		fillTreeView ();
	}

	private void fillTreeView() {
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);
	}

	//	void newActionActivated (object sender, EventArgs e)
	//	{
	//		new ArticuloView ();
	//	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

}