using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MySqlConnection mySqlConnection = new MySqlConnection (
				"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
			);

			mySqlConnection.Open ();


			//updateDataBase (mySqlConnection);


			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = 
				"select a.categoria as articulocategoria," +
				"c.nombre as categorianombre, " +
				"count(*) as numeroarticulos " +
				"from articulo a " +
				"left join categoria c " +
				"on a.categoria = c.id " +
				"group by articulocategoria, categorianombre";

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

			showColumNames (mySqlDataReader);
			show (mySqlDataReader);

			mySqlDataReader.Close ();

			mySqlConnection.Close ();
		}

		private static void updateDataBase(MySqlConnection mySqlConnection) {
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = "update articulo set categoria=2 where id=3";
			mySqlCommand.ExecuteNonQuery ();
		}

		private static void showColumNames(MySqlDataReader mySqlDataReader){
			for (int i = 0; i < mySqlDataReader.FieldCount; i++) {
				Console.WriteLine ("Columna: " + mySqlDataReader.GetName (i));
			}
			Console.WriteLine (" ");
		}

		//string estatico
//		public static string[] getColumnNames(MySqlDataReader mySqlDataReader) {
//			int count = mySqlDataReader.FieldCount;
//			string[] columnNames = new string[count];
//			for (int i = 0; i < count; i++)
//				columnNames [i] = mySqlDataReader.GetName (i);
//			return columnNames;
//		}


		//lista dinamica
		public static string[] getColumnNames(MySqlDataReader mySqlDataReader) {
			int count = mySqlDataReader.FieldCount;
			List<string> columnNames = new List<string> ();
			for (int i = 0; i < count; i++)
				columnNames.Add (mySqlDataReader.GetName (i));

			return columnNames.ToArray ();
		}

		private static void show(MySqlDataReader mySqlDataReader) {
			while (mySqlDataReader.Read()) {
				Console.WriteLine ("ID: " + mySqlDataReader ["id"].ToString ());
				Console.WriteLine ("nombre: " + mySqlDataReader ["nombre"].ToString ());
				Console.WriteLine (" ");
				                                 

			}

		}
	}
}
