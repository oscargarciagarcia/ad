using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		private object id = null;
		private string nombre = "";
		private object categoria = null;
		private decimal precio = 0;

//		public static ArticuloView New() {
//			return new ArticuloView ();
//		}
//
//		public static ArticuloView Edit(object id) {
//			return new ArticuloView (id);
//		}

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			init ();
		}

		public ArticuloView(object id) :base(WindowType.Toplevel) {
			this.id = id;
			load ();
			init ();
			saveAction.Activated += delegate {	update();	};
		}

		private void init() {
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboboxCategoria, queryResult, categoria);
			spinbuttonPrecio.Value = Convert.ToDouble(precio);
			//saveAction.Activated += delegate {	save();	};
		}

		private void load() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.addParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ())
				//TODO throw exception
				return;
			nombre = (string)dataReader ["nombre"];
			categoria = dataReader ["categoria"];
			if (categoria is DBNull)
				categoria = null;
			precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
		}

//		private void save() {
//			if (id == null)
//				insert ();
//			else
//				update ();
//		}

		private void insert() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId (comboboxCategoria);
			precio = Convert.ToDecimal(spinbuttonPrecio.Value);

			DbCommandHelper.addParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.addParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.addParameter (dbCommand, "precio", precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void update() {
			Console.WriteLine("update");
		}
	}
}