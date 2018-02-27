<?php
	require("conectabd.php");
	$TipoRaiz=$_GET['TipoRaiz'];
	$IdRaiz=$_GET['IdRaiz'];	

	$sql="SELECT * FROM  Ficheros_IDE WHERE TipoRaiz=$TipoRaiz AND IdRaiz=$IdRaiz";	
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"Ficheros":[';
			for($cont=1;$cont<=$nr;$cont++)
			{
				$row=mysqli_fetch_object($resultado);
				$encode=json_encode($row);
				if(is_string($encode))
				{
					if($cont<$nr)
						$json=$json.$encode.",";
					else
						$json=$json.$encode;
				}
				
			}
			$json=$json."]}";						
			echo "$json";
		}
		else
		{
			$error="No hay ficheros.\n";		
		}
		
	}
	mysqli_close($conexion);
?>