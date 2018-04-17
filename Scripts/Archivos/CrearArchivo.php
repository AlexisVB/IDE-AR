<?php
if(isset($_GET['fichero']))
	{
		$fichero=$_GET['fichero'];//fichero que vamos a crear
		$dirBase=dirname(__FILE__)."/"."Soluciones";
		$fichero=$dirBase."/".$fichero;	
		if(is_writable(dirname($fichero)))
		{		
			$resource=fopen($fichero, 'w');
			if(file_exists($fichero))
				echo "1\n";
		}
		else
			echo "0\n";	
	}
?>