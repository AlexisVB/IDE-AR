<?php
	require("conectabd.php");
	$IdGrupo=$_GET['IdGrupo'];
	$sql="SELECT Actividades_IDE.IdActividad AS IdActividad,
				 Actividades_IDE.Nombre AS Nombre,
				 Actividades_IDE.IdGrupo AS IdGrupo,
				 Actividades_IDE.FechaInicial AS FechaInicial,
				 Actividades_IDE.FechaLimite AS FechaLimite,
				 Actividades_IDE.Color AS Color,
				 Actividades_IDE.Nick AS Nick 
				 FROM Actividades_IDE INNER JOIN Grupos_IDE 
				 ON Actividades_IDE.IdGrupo = Grupos_IDE.IdGrupo
				 WHERE Grupos_IDE.IdGrupo=$IdGrupo";
				 $resultado=mysqli_query($conexion,$sql);	
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"actividades":[';
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
