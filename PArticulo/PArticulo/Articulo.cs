using System;

namespace PArticulo {

	public class Articulo {

		public Articulo () {
		}

		private object id;
		private string nombre;
		private object categoria;
		private object precio;

		//property de id
		public object Id {
			get { return id; }
			set { id = value; }
		}

		//property de nombre
		public string Nombre {
			get { return nombre; }
			set { nombre = value; }
		}

		//property de categoria
		public object Categoria {
			get { return categoria; }
			set { categoria = value; }
		}

		//property de precio
		public object Precio {
			get { return precio; }
			set { precio = value; }
		}
	}
}

