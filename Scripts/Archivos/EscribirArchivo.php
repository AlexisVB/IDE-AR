<?php
if(isset($_GET['fichero'])&&isset($_GET['texto']))
	{
		$fichero=$_GET['fichero'];//fichero que vamos a escribir
		$texto=$_GET['texto'];//contenido a escribir en el fichero
		$dirBase=dirname(__FILE__)."/"."Soluciones";
		$fichero=$dirBase."/".$fichero;	
		if(is_writable(dirname($fichero)))
		{		
			$resource=fopen($fichero, 'w');
			if(file_exists($fichero))
				{
					fwrite($resource, $texto);
					echo "1\n";
				}
				fclose($resource);
		}
		else
			echo "0\n";	
	}
?>