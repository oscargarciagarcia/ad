using Gtk;
using System;
using System.Collections;
using System.Data;


namespace SerpisAd
{
	public class ComboBoxHelper
	{
		public static void Fill (ComboBox combobox, QueryResult queryresult) {
			CellRendererText cellRendererText = new CellRendererText ();
			comboBoxCategoria.PackStart (cellRendererText, false);
			comboBoxCategoria.SetCellDataFunc (cellRendererText, 
			                                   delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row = (IList)tree_model.GetValue(iter, 0);
				cellRendererText.Text = row[1].ToString();
			});
			ListStore listStore = new ListStore (typeof(IList));
			IList first = new object[] { null, "<sin asignar>" };
			TreeIter treeIterFirst = listStore.AppendValues (first);
			listStore.AppendValues (first);
			foreach (IList row in queryResult.Rows)
				listStore.AppendValues (row);
			comboBox.Model = listStore;
			//combobox.Active = 0;
			ComboBox.SetActiveIter (treeIterFirst);
	}

		public static object GetId (ComboBox comboBox) {
			TreeIter treeIter;
			comBoxCategoria.GetActiveIter (out treeIter);
			//esto devuelve una fila del combobox
			IList row = (IList)comboBoxCategoria.Model.GetValue (treeIter, 0);
			return row [0];
		}
}
}

