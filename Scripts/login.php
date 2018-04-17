<?php

	require("conectabd.php");
	$usuario=$_GET['user'];
	$password=$_GET['password'];
	$sql="SELECT * FROM Usuarios_IDE WHERE nombreUsuario='$usuario' AND password=md5('$password')";	
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
				$json=$encode;
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