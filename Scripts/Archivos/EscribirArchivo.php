<?php
if(isset($_GET['fichero']))
	{
		$fichero=$_GET['fichero'];//fichero que vamos a escribir
		$texto=$_GET['texto'];//cotneido aescribir en el fichero
		$dirBase=dirname(__FILE__)."\\"."Soluciones";
		$fichero=$dirBase."\\".$fichero;	
		if(is_writable(dirname($fichero)))
		{		
			$resource=fopen($fichero, 'w');
			if(file_exists($fichero))
				{
					fwrite($resource, $texto);
				}
				fclose($resource);
		}
		else
			echo "El archivo no se ha podido crear\n";	
	}
?>