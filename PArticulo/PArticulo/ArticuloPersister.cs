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
				return null;
			articulo.Nombre = (string)dataReader ["nombre"];
			articulo.Categoria =  get (dataReader ["categoria"], null);
			if (articulo.Categoria is DBNull)
				articulo.Categoria = null;
			articulo.Precio = (decimal) get(dataReader ["precio"], decimal.Zero);
			dataReader.Close ();

			return articulo;
		}

		private static object get(object value, object defaultValue) {
			return value is DBNull ? defaultValue : value;
			//el simbolo "?" quiere decir que, teniendo en cuenta lo de la izquierda del mismo devuelve lo de la
			//izquierda de los puntos (true), si no devuelve lo de la derecha (false)
		}

		public static void Insert (Articulo articulo) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			DbCommandHelper.addParameter (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.addParameter (dbCommand, "categoria", articulo.Categoria);
			DbCommandHelper.addParameter (dbCommand, "precio", articulo.Precio);
			dbCommand.ExecuteNonQuery ();
			//el ExecuteNonQuery devuelve cuantas filas son aceptadas
		}

		public static void Update (Articulo articulo) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre = @nombre, categoria = @categoria, " +
				"precio = @precio where id = @id";

			DbCommandHelper.addParameter (dbCommand, "nombre", articulo.Nombre);
			DbCommandHelper.addParameter (dbCommand, "categoria", articulo.Categoria);
			DbCommandHelper.addParameter (dbCommand, "precio", articulo.Precio);
			DbCommandHelper.addParameter (dbCommand, "id", articulo.Id);
			dbCommand.ExecuteNonQuery ();
		}
	}
}

