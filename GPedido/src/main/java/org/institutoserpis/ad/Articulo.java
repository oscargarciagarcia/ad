package org.institutoserpis.ad;

import java.math.BigDecimal;

import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.JoinColumn;

import org.hibernate.annotations.GenericGenerator;

@Entity
public class Articulo {

	private Long id;
	private String nombre;
	private Categoria categoria;
	private BigDecimal precio;
	
	@Id
	@GeneratedValue(generator="increment")
	@GenericGenerator(name="increment", strategy="increment")
	
	//get - set de id
	public Long getId() {
		return id;
	}
	public void setId(Long id) {
		this.id = id;
	}
	
	//get - set de nombre
	public String getNombre() {
		return nombre;
	}
	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
	
	//Con el LAZY solo lee de la tabla de categoria si es necesario.
	@ManyToOne(fetch=FetchType.LAZY)
	@JoinColumn(name="categoria")
	//get - set de categoria
	public Categoria getCategoria() {
		return categoria;
	}
	public void setCategoria(Categoria categoria) {
		this.categoria = categoria;
	}
	
	//get - set de precio
	public BigDecimal getPrecio() {
		return precio;
	}
	public void setPrecio(BigDecimal precio) {
		this.precio = precio;
	}
	
	@Override
	public String toString() {
		return String.format("%s %-20s %s %s", id, nombre, categoria, precio);
				
	}
	
	
}
