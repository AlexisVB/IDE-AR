<?php
	require("conectabd.php");
	$IdUsuario=$_GET['IdUsuario'];//Es un alumno
	$sql="SELECT Grupos_IDE.IdGrupo AS IdGrupo,
				 Grupos_IDE.Nombre AS Nombre,
				 Grupos_IDE.IdMateria AS IdMateria
				 FROM Grupos_IDE INNER JOIN IntegrantesGrupo_IDE 
				 ON Grupos_IDE.IdGrupo=IntegrantesGrupo_IDE.IdGrupo
				 WHERE IntegrantesGrupo_IDE.IdUsuario=$IdUsuario";
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"grupos":[';
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
			echo "{}";
		}
	}
	mysqli_close($conexion);

?>



