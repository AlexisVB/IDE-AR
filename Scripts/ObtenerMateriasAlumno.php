<?php
	require("conectabd.php");
	$IdGrupo=$_GET['IdGrupo'];
	$sql="SELECT Materias_IDE.IdMateria AS IdMateria,
				 Materias_IDE.Nombre AS Nombre,
				 Materias_IDE.Matricula AS Matricula,
				 Materias_IDE.IdProfesor AS IdProfesor,
				 Materias_IDE.Color AS Color,
				 Materias_IDE.Nick AS Nick
				 FROM Materias_IDE INNER JOIN Grupos_IDE 
				 ON Materias_IDE.IdMateria = Grupos_IDE.IdMateria
				 WHERE Grupos_IDE.IdGrupo=$IdGrupo";
				 $resultado=mysqli_query($conexion,$sql);	
	if(!$resultado)
	{
		//Si la consulta esta mal escrita llega a este punto
		echo "Error";
	}
	else
	{	//Cuenta cuantas filas se obtuvieron	
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