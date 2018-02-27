<?php
if(isset($_GET['fichero']))
	{
		$fichero=$_GET['fichero'];//fichero que vamos a crear
		$dirBase=dirname(__FILE__)."\\"."Soluciones";
		$fichero=$dirBase."\\".$fichero;	
		if(is_writable(dirname($fichero)))
		{		
			$resource=fopen($fichero, 'w');
			if(file_exists($fichero))
				echo "Archivo creado\n";
		}
		else
			echo "El archivo no se ha podido crear\n";	
	}
?>