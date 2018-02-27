<?php
	require("conectabd.php");
	
	$idgrupo=$_GET['idGrupo'];	

	$sql="SELECT Usuarios_IDE.IdUsuario AS IdUsuario,
				 Usuarios_IDE.NombreUsuario AS NombreUsuario,
				 Usuarios_IDE.Nombre As Nombre,
				 Usuarios_IDE.Registro AS Registro,
				 Usuarios_IDE.Grupo AS Grupo,
				 Usuarios_IDE.Correo As Correo 
				  FROM  Usuarios_IDE
				  INNER JOIN IntegrantesGrupo_IDE
				  WHERE Usuarios_IDE.IdUsuario=IntegrantesGrupo_IDE.IdUsuario
				  AND IntegrantesGrupo_IDE.IdGrupo=$idgrupo";
	mysqli_query($conexion,$sql);
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"usuarios":[';
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
			$error="No hay alumnos";		
		}
		
	}
	mysqli_close($conexion);
?>