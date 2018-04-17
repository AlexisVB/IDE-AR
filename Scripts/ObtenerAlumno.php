<?php
	require("conectabd.php");
	$registro=$_GET['registro'];	
	$sql="SELECT * FROM alumnos WHERE Registro_al='$registro'";	
	$resultado=mysqli_query($conexion,$sql);	
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	
			$row=mysqli_fetch_object($resultado);			
			$encode=json_encode($row);
			if(is_string($encode))
			{		
				$json=$encode."\n";
			}									
			echo "$json";
		}
		else
		{
			$error="{}";
			echo $error;		
		}		
	}
	mysqli_close($conexion);
?>