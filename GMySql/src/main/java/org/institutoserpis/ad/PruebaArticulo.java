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
	public static void main(String[] args) throws SQLException {
		
		
		//conectar con la base de datos 
		Connection conexion = DriverManager.getConnection(
		"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
			
		Statement stat = conexion.createStatement();
		
		//Insert
		String sqlInsert = " INSERT INTO articulo VALUES (null,?,?,?)";
		PreparedStatement insertar = null;
		insertar = (PreparedStatement)conexion.prepareStatement(sqlInsert);
		//Damos valor a los atributos de la tabla
		Scanner tcl = new Scanner(System.in);
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
		insertar.executeUpdate();
		
		//String sqlUpdate = " UPDATE articulo SET "
		//stat.executeUpdate(sql);
		//Muestro lo que quiero ver de la tabla
		ResultSet result = stat.executeQuery("SELECT * FROM articulo");
		
	
			
		ResultSetMetaData rsMetaData = result.getMetaData();
		int numColum = rsMetaData.getColumnCount();
		System.out.println("Nombre de las columnas:");
		for (int i =1; i < numColum+1; i++) {
			String columnName = rsMetaData.getColumnName(i);
			System.out.print(columnName+"  ");
			    
		}
		System.out.println();
	
		System.out.println();
		while(result.next()){
			String id = result.getString(1);
			String nombre = result.getString(2);
			String categoria= result.getString(3);
			String precio = result.getString(4);
			
			//mostramos los datos de las columnas de la base de datos
			System.out.println(id+" | "+nombre+" | "+ categoria +" | "+ precio);
			
			
		}
			
			
		conexion.close();
		System.out.println();
		System.out.println("Fin");
		}
}
