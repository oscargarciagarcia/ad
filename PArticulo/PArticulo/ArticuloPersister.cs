using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;


namespace PArticulo {

	public class ArticuloPersister {

		public ArticuloPersister () {
		}

		public static Articulo Load (object id) {
			Articulo articulo = new Articulo ();
			articulo.Id = id;
			string nom = articulo.Nombre;
			object categ = articulo.Categoria;
			decimal prec = articulo.Precio;
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.addParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ())
				//TODO throw exception
				return null;
			nom = (string)dataReader ["nombre"];
			categ = dataReader ["categoria"];
			if (categ is DBNull)
				categ = null;
			prec = (decimal)dataReader ["precio"];
			dataReader.Close ();
			return articulo;
		}

		public static void Insert (Articulo articulo) {
		}

		public static void update (Articulo articulo) {
		}
	}
}

