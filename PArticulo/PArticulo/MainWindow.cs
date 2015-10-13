using System;
using System.Data;
using Gtk;
//añadimos este using
using System.Collections.Generic;//en este using se encuentra el List
using MySql.Data.MySqlClient;
using System.Collections;

//pongo este using, porque dentro de este namespace esta el App
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		Console.WriteLine ("Mainwindow ctor.");
		IDbConnection dbConnection = App.Instance.DbConnection;
		IDbCommand dbCommand = dbConnection.CreateCommand ();

		dbCommand.CommandText = "select * from articulo";
		IDataReader dataReader = dbCommand.ExecuteReader ();
		//con el executereader se cuantas columnas hay
		//añado columnas
		//treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		//treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		string[] columnNames = getColumnNames (dataReader);
		CellRendererText cellRendererText = new CellRendererText ();
		for (int i = 0; i < columnNames.Length; i++) {
			//treeView.AppendColumn (columnNames [i], new CellRendererText (), "text", i);
			int column = i;
			treeView.AppendColumn (columnNames [i], cellRendererText, 
				delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				//string value = tree_model.GetValue(iter, column).ToString();  
				//cellRendererText.Text ="·" + value + "·";
				IList row = (IList)tree_model.GetValue(iter, 0);
				cellRendererText.Text = row [column].ToString();
			});
		}

		//Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (typeof(IList));

		while (dataReader.Read()) {
			//listStore.AppendValues (dataReader [0].ToString(), dataReader [1].ToString());
			//Console.WriteLine ("id={0} nombre={1}", dataReader [0], dataReader [1]);
			//string[] values = getValues (dataReader);
			IList values = getValues (dataReader);
			listStore.AppendValues (values);
		}

		dataReader.Close ();

		treeView.Model = listStore;

		dbConnection.Close ();

	}

	private string[] getColumnNames(IDataReader dataReader){
		List<string> columnNames = new List<string> ();
		int count = dataReader.FieldCount;
		for (int i = 0; i < count; i++) 
			columnNames.Add (dataReader.GetName (i));

		return columnNames.ToArray ();

	}

	private Type[] getTypes(int count) {
		List<Type> types = new List<Type> ();
		for (int i = 0; i < count; i++)
			types.Add (typeof(String));
		return types.ToArray();
		}

//	private string[] getValues(IDataReader dataReader){
//		List<string> values = new List<string> ();
//		int count = dataReader.FieldCount;
//		for (int i = 0; i < count; i++)
//			values.Add (dataReader [i].ToString ());
//		return values.ToArray ();
//	}
	private IList getValues(IDataReader dataReader){
		List<object> values = new List<object> ();
		int count = dataReader.FieldCount;
		for (int i = 0; i < count; i++)
			values.Add (dataReader [i].ToString ());
		return values;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
