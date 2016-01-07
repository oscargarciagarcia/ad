package org.institutoserpis.ad;

import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Scanner;

public class PruebaArticulo {
	//Scanner creado
	static Scanner tcl = new Scanner(System.in);
	
	public static void main(String[] args) throws Exception {
		
		//conectar con la base de datos
		Connection conexion = DriverManager.getConnection(
			"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		Statement stat = conexion.createStatement();
		
		//Muestro la tabla
		//ResultSet result1 = stat.executeQuery("SELECT * FROM artiuclo");
		//mostrarDatos(result1);
		
		int opcion;
		//Menu
	do {	
		System.out.println("1. Insertar");
		System.out.println("2. Borrar");
		System.out.println("3. Mostrar Tabla");
		System.out.println("4. Salir");
		System.out.println("");
		System.out.println("Que quieres hacer? ");
		opcion = tcl.nextInt();
		//con el nextLine() limpio el buffer
		tcl.nextLine();
		
		//Opcion 1: Insertar datos
		if (opcion == 1){
			String sqlInsert = "INSERT INTO articulo VALUES (null,?,?,?)";
			PreparedStatement insertar = (PreparedStatement)conexion.prepareStatement(sqlInsert);
			insertarDatos(insertar);
			
		//Opcion 2: Borrar datos		
		}if (opcion == 2){
			String sqlDelete = "DELETE FROM articulo WHERE id=?";
			PreparedStatement borrar = (PreparedStatement)conexion.prepareStatement(sqlDelete);
			borrarDatos(borrar);
		
		//Opción 3: Mostar los datos
		}if (opcion == 3){
			ResultSet result = stat.executeQuery("SELECT * FROM articulo");
			mostrarDatos(result);
		}else{	
			
		
		}
	}while (opcion != 4);
	conexion.close();
	System.out.println();
	System.out.println("Fin");
}
	//Metodo Mostrar
	private static void mostrarDatos(ResultSet resultSet) throws SQLException {
		//esto muestra el nombre de las columnas	
				ResultSetMetaData rsMetaData = resultSet.getMetaData();
				int numColum = rsMetaData.getColumnCount();
				System.out.println("Nombre de las columnas:");
				for (int i =1; i < numColum+1; i++) {
					String columnName = rsMetaData.getColumnName(i);
					System.out.print(columnName+"  ");
					    
				}
				System.out.println();
			
				System.out.println();
				while(resultSet.next()){
					//le indico que coja de la sentencia "result" las columnas
					String id = resultSet.getString(1);
					String nombre = resultSet.getString(2);
					String categoria= resultSet.getString(3);
					String precio = resultSet.getString(4);
				
					//mostramos los datos de las columnas de la base de datos
					System.out.println(id+" | "+nombre+" | "+ categoria +" | "+ precio);
				}
	}
	//Metodo Insert		
	private static int insertarDatos(PreparedStatement insertar) throws SQLException {
		//nombre
		System.out.print("Introduce el nombre: ");
		String nombreInsert = tcl.nextLine();
		insertar.setString(1, nombreInsert);
		//categoria
		System.out.print("Introduce la categoria (1, 2 o 3): ");
		int categoriaInsert = tcl.nextInt();
		insertar.setInt(2, categoriaInsert);
		//precio
		System.out.print("Introduce el precio: ");
		Double precioInsert = tcl.nextDouble();
		insertar.setDouble(3, precioInsert);
		return insertar.executeUpdate();	
	}
	//Metodo Borrar
	private static int borrarDatos(PreparedStatement borrar) throws SQLException {
		System.out.println("Que id quieres borrar? ");
		int idBorrar = tcl.nextInt();
		borrar.setInt(1, idBorrar);
		return borrar.executeUpdate();
	}
	//Metodo Upadte
	//private static int updateDatos (PreparedStatement updatear) throws SQLException {
	//	System.out.println("Que articulo quieres modificar");
		
	//}
	
}
