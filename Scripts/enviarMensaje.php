<?php
	require("conectabd.php");
	
	$idRemitente=$_GET['idRemitente'];	
	$idDestinatario=$_GET['idDestinatario'];	
	$fechaEnvio=$_GET['fechaEnvio'];
	$mensaje=$_GET['mensaje'];
	$sql="INSERT INTO Chat_IDE (IdRemitente,idDestinatario,FechaEnvio,Mensaje) Values ($idRemitente,$idDestinatario,'$fechaEnvio','$mensaje')";
	mysqli_query($conexion,$sql);	
	$sql="SELECT * FROM  Chat_IDE WHERE
	IdRemitente=$idRemitente AND
	idDestinatario=$idDestinatario AND
	FechaEnvio='$fechaEnvio' AND
	Mensaje= '$mensaje'";
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	//si hay filas en el resultado quiere decir que si se inserto y se obtiene el primer registro
			$row=mysqli_fetch_object($resultado);
			//convierte el registro obtenido a cadena en formato Json
			$encode=json_encode($row);
			if(is_string($encode))
			{		
				$json=$encode;
			}
			//retorna el registro en Json obtenido									
			echo "$json";
		}
		else
		{
			$error="No hay mensajes";		
		}
		
	}
	mysqli_close($conexion);
?>