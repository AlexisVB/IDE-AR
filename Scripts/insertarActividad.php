<?php
	//Script para insertar actividad 03/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$Nombre=$_GET['nombre'];
	$IdGrupo=$_GET['idGrupo'];
	$FechaIni=$_GET['fechaIni'];	
	$FechaLim=$_GET['fechaLim'];
	$Color=$_GET['color'];
	$Nick=$_GET['nick'];
	//realiza la inserción de la actividad
	$sql="INSERT INTO Actividades_IDE(Nombre,IdGrupo,FechaInicial,FechaLimite,Color,Nick) 
			VALUES('$Nombre',$IdGrupo,'$FechaIni','$FechaLim','$Color','$Nick')";
	mysqli_query($conexion,$sql);
	//Revisa que la inserción se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM Actividades_IDE 
			WHERE Nombre='$Nombre' 
			AND IdGrupo=$IdGrupo 
			AND FechaInicial='$FechaIni'
			AND FechaLimite='$FechaLim'
			AND Color='$Color'
			AND Nick='$Nick'";
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