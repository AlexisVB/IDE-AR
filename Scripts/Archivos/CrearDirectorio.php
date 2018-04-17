<?php 
	if(isset($_GET['carpeta']))
	{
		$carpeta=$_GET['carpeta'];//fichero que vamos a copiar			
		$dirBase=dirname(__FILE__)."/"."Soluciones";
		if(!file_exists($dirBase))
			mkdir($dirBase);
		$Ruta=$dirBase."/".$carpeta;			
		if(file_exists($Ruta))
		{
				echo "1\n";//Ya existe
		}
		else
		{
			if(mkdir($Ruta))
			{		
				echo "1\n";//Se creo correctamente
			}
			else
				echo "0\n";//Error al crear
		}	
		
	}
	else
		echo "No hay parametros";
	//agregar usuario agrega una carpeta para sus archivos 
	//agregar materia crea un nuevo directorio para la materia
	//agregar un grupo crea un nuevo directorio para el grupo
	//agregar una actividad crea un nuevo directorio para la actividad
	//el alumno al entregar una actividad crea un nuevo directorio en la actividad correspondiente unica del 
	//alumno
	
?>