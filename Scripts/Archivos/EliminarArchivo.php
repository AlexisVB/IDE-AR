<?php
if(isset($_GET['fichero']))
	{
		$fichero=$_GET['fichero'];//fichero que vamos a eliminar
		$dirBase=dirname(__FILE__)."\\"."Soluciones";
		$fichero=$dirBase."\\".$fichero;	
		if(is_writable(dirname($fichero)) && file_exists($fichero) &&unlink($fichero))
		{					
				echo "Archivo eliminado\n";
		}
		else
			echo "El archivo no se ha podido eliminar\n";	
	}
?>