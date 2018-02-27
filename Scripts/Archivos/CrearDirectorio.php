<?php 
	if(isset($_GET['dir']))
	{	
		$carpeta=$_GET['carpeta'];//fichero que vamos a copiar			
		$dirBase=dirname(__FILE__)."\\"."Soluciones";
		if(!file_exists($dirBase))
			mkdir($dirBase);
		$Ruta=$dirBase."\\".$carpeta;	
		if(mkdir($Ruta))
		{		
			echo "Carpeta creada con éxito\n";
		}
		else
			echo "No se ha podido crear la carpeta\n";	
	}
	
	//agregar usuario agrega una carpeta para sus archivos 
	//agregar materia crea un nuevo directorio para la materia
	//agregar un grupo crea un nuevo directorio para el grupo
	//agregar una actividad crea un nuevo directorio para la actividad
	//el alumno al entregar una actividad crea un nuevo directorio en la actividad correspondiente unica del 
	//alumno
	
?>