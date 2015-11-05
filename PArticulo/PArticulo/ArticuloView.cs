using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
			base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			//entryNombre.Text = "nuevo";
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboboxCategoria, queryResult);
			//spinButtonPrecio.Value = 1.5;

			saveAction.Activated += delegate {	save();	};
		}

		private void save() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			string nombre = entryNombre.Text;
			object categoria = ComboBoxHelper.GetId (comboboxCategoria);
			decimal precio = Convert.ToDecimal(spinbuttonPrecio.Value);

			DbCommandHelper.addParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.addParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.addParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
		}
	}
}