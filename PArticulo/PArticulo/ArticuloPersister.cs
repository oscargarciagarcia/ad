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
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.addParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (!dataReader.Read ())
				//TODO throw exception
				return null;
			articulo.Nombre = (string)dataReader ["nombre"];
			articulo.Categoria = dataReader ["categoria"];
			if (articulo.Categoria is DBNull)
				articulo.Categoria = null;
			articulo.Precio = (object)dataReader ["precio"];
			dataReader.Close ();
			return articulo;
		}

		public static void Insert (Articulo articulo) {
		}

		public static void update (Articulo articulo) {
		}
	}
}

