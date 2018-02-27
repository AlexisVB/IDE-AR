<?php
	require("conectabd.php");
	
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
				  WHERE SolucionProyecto_IDE.IdActividad=$IdActividad";
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"soluciones":[';
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
			$error="No hay Soluciones\n";		
		}
		
	}
	mysqli_close($conexion);
?>