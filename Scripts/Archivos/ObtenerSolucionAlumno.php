<?php
	require("conectabd.php");
	$IdPropietario=$_GET['IdPropietario']
	$IdActividad=$_GET['IdActividad']	

	
	$sql="SELECT SolucionProyecto_IDE.IdProyecto AS IdProyecto,
				 SolucionProyecto_IDE.Nombre AS Nombre,
				 SolucionProyecto_IDE.Ruta As Ruta,
				 SolucionProyecto_IDE.IdPropietario AS IdPropietario,
				 SolucionProyecto_IDE.IdActividad AS IdActividad,
				 SolucionProyecto_IDE.Fecha As Fecha 
				 Usuarios_IDE.Nombre As  NombrePropietario
				  FROM  SolucionProyecto_IDE
				  INNER JOIN Usuarios_IDE 
				  ON SolucionProyecto_IDE.IdPropietario=Usuarios_IDE.IdUsuario
				  WHERE SolucionProyecto_IDE.IdActividad=$IdActividad
				  AND SolucionProyecto_IDE.IdPropietario=$IdPropietario";
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		//Cuenta cuantas filas se obtuvieron	
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	
			//si hay filas en el resultado quiere decir que si se inserto y se obtiene el primer registro
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
			//si no hay ninguna fila quiere decir que no se inserto
			$error="{}";
			echo $error;		
		}		
	}
	mysqli_close($conexion);
?>