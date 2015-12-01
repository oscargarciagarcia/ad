using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		//private object id = null;
//		private string nombre = "";
//		private object categoria = null;
//		private decimal precio = 0;
		System.Action save;

		private Articulo articulo;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{
			articulo = new Articulo ();
			articulo.Nombre = "";
			//esto de las "" es para que el valor por defecto del nombre sea vacío, y no null
			init ();
			saveAction.Activated += delegate {
				insert ();
			};
		}

		public ArticuloView(object id) :base(WindowType.Toplevel) {
			//this.id = id;
			articulo = ArticuloPersister.Load(id);
			init ();
			saveAction.Activated += delegate {	update();	};
		}

		private void init() {
			this.Build ();
			entryNombre.Text = articulo.Nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboboxCategoria, queryResult, articulo.Categoria);
			spinbuttonPrecio.Value = Convert.ToDouble(articulo.Precio);
			//saveAction.Activated += delegate {	save();	};
		}

//		private void load() {
//			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
//			dbCommand.CommandText = "select * from articulo where id = @id";
//			DbCommandHelper.addParameter (dbCommand, "id", id);
//			IDataReader dataReader = dbCommand.ExecuteReader ();
//			if (!dataReader.Read ())
//				//TODO throw exception
//				return;
//			nombre = (string)dataReader ["nombre"];
//			categoria = dataReader ["categoria"];
//			if (categoria is DBNull)
//				categoria = null;
//			precio = (decimal)dataReader ["precio"];
//			dataReader.Close ();
//		}

//		private void save() {
//			if (id == null)
//				insert ();
//			else
//				update ();
//		}

		private void insert() {

//			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
//			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
//				"values (@nombre, @categoria, @precio)";

			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboboxCategoria);
			articulo.Precio = Convert.ToDecimal(spinbuttonPrecio.Value);
			ArticuloPersister.Insert (articulo);
			//si quiero que el precio no me pete cuando sea null, tengo que cambiarlo a tipo object. 
			//aqui tambien tendria que añadir un Convert.ToDouble

//			DbCommandHelper.addParameter (dbCommand, "nombre", nombre);
//			DbCommandHelper.addParameter (dbCommand, "categoria", categoria);
//			DbCommandHelper.addParameter (dbCommand, "precio", precio);
//			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void update() {
			Console.WriteLine("update");
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboboxCategoria);
			articulo.Precio = Convert.ToDecimal(spinbuttonPrecio.Value);
			ArticuloPersister.Update (articulo);
			Destroy ();
		}
	}
}