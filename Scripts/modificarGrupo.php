<?php
	//Script para modificar un grupo 03/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$IdGrupo=$_GET['idGrupo'];
	$Nombre=$_GET['nombre'];	
	$Color=$_GET['color'];
	$Nick=$_GET['nick'];
	//realiza la modificación del grupo
	$sql="UPDATE Grupos_IDE 
		SET Nombre='$Nombre',		
		Color='$Color',
		Nick='$Nick'  
		WHERE IdGrupo=$IdGrupo";
	mysqli_query($conexion,$sql);
	//Revisa que la modificación se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM Grupos_IDE 
			WHERE IdGrupo=$IdGrupo";			
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