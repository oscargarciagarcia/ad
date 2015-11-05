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
			ComboBoxHelper.Fill (ComboBoxCategoria, queryResult);

			saveAction.Activated += delegate { save(); };

		}
		private void save(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " + "values (@nombre, @categoria, @precio)";



			//Esto es un poco pesado (4 líeas para establcer un paráetro)
			String nombre = entryNombre.Text;
			object categoria = ComboBoxHelper.GetId; //TODO cogerlo del ComboBox
			decimal precio = Convert.ToDecimal (spinButtonPrecio.Value);


			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

			dbCommand.ExecuteNonQuery ();
		}




}

}
