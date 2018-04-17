<?php
if(isset($_GET['origen'])&&isset($_GET['destino']))
	{
		$origen=$_GET['origen'];//fichero que vamos a renombrar
		$destino=$_GET['destino'];//fichero que vamos a renombrar
		$dirBase=dirname(__FILE__)."\\"."Soluciones";
		$origen=$dirBase."\\".$origen;
		$destino=$dirBase."\\".$destino;
		if(@rename($origen,$destino))
		{					
				echo "Archivo  renombrado\n";
		}
		else
			echo "El archivo no se ha podido renombrar\n";	
	}
?>