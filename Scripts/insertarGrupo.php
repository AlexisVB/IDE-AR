<?php
	//Script para insertar grupo 03/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$Nombre=$_GET['nombre'];	
	$IdMateria=$_GET['idMateria'];
	$Color=$_GET['color'];
	$Nick=$_GET['nick'];
	//realiza la inserción del grupo
	$sql="INSERT INTO Grupos_IDE(Nombre,IdMateria,Color,Nick) VALUES('$Nombre',$IdMateria,'$Color','$Nick')";
	mysqli_query($conexion,$sql);
	//Revisa que la inserción se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM Grupos_IDE 
			WHERE Nombre='$Nombre' 			
			AND IdMateria=$IdMateria 
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