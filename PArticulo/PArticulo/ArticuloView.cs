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

		private void load() {

		}


		private void updateModel() {
			articulo.Nombre = entryNombre.Text;
			articulo.Categoria = ComboBoxHelper.GetId (comboboxCategoria);
			articulo.Precio = Convert.ToDecimal (spinbuttonPrecio.Value);
		}

		private void insert() {

			updateModel ();
			ArticuloPersister.Insert (articulo);

			Destroy ();
		}

		private void update() {
			updateModel ();
			Console.WriteLine("update");
			ArticuloPersister.Update (articulo);
			Destroy ();
		}
	}
}