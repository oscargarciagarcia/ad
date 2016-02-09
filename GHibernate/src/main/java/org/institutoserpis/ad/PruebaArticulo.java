package org.institutoserpis.ad;

//import javax.persistence.Entity;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

public class PruebaArticulo {
	
	private static EntityManagerFactory entityManagerFactory;

	public static void main(String[] args) {
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		
		System.out.println("inicio");
		entityManagerFactory = Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		//find articulo con id2
//		Long articuloId = persist();
//		update(articuloId);
//		find(articuloId);
//		remove(articuloId);
		query();
		
		entityManagerFactory.close();
	}
	
	private static void show(Articulo articulo){
		//los porcentajes marcan el espacio. El nombre tiene un -30 porque asi se pega al campo 
		//de antes, la id
		System.out.printf("%5s %-40s %-25s %10s\n",
			articulo.getId(),
			articulo.getNombre(),
			format(articulo.getCategoria()),
			articulo.getPrecio()
			);
	}
	
	private static String format(Categoria categoria) {
		if (categoria == null)
			return null;
		return String.format("%4s %-20s", categoria.getId(), categoria.getNombre());
		//return String.format("%4s", categoria.getId());
	}
	
	private static void query() {
		//El entity manager nos va a permitir trabajar con la entidad que sea.
				EntityManager entityManager = entityManagerFactory.createEntityManager();
				entityManager.getTransaction().begin();
				
				List<Articulo> articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
				for (Articulo articulo : articulos)
					show(articulo);
					 
				entityManager.getTransaction().commit();
				entityManager.close();
	}
	
	private static Long persist(){
		System.out.println("persist: ");
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		//creamos un articulo, le establezco un nombre. El persist es un insert
		Articulo articulo = new Articulo();
		articulo.setNombre("nuevo " + new Date());
		entityManager.persist(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
		return articulo.getId();
	}
	
	private static void find(Long id){
		System.out.println("Find: " + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = entityManager.find(Articulo.class, id);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}
	
	private static void remove(Long id){
		System.out.println("remove: " + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = entityManager.find(Articulo.class, id);
		entityManager.remove(articulo);
		entityManager.getTransaction().commit();
		entityManager.close();
		
	}
	
	private static void update(Long id){
		System.out.println("update :" + id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articulo = entityManager.find(Articulo.class, id);
		articulo.setNombre("modificado: " + new Date());
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articulo);
	}

}
