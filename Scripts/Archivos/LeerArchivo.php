<?php
if(isset($_GET['fichero']))
	{
		$fichero=$_GET['fichero'];//fichero que vamos a escribir		
		$dirBase=dirname(__FILE__)."\\"."Soluciones";
		$fichero=$dirBase."\\".$fichero;	
		if(is_writable(dirname($fichero)))
		{	
				$texto=file_get_contents($fichero);				
				echo $texto;
		}
		else
			echo "El archivo no se ha leer \n";	
	}
?>