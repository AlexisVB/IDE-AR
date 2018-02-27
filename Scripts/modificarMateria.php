<?php
	//Script para modificar materia 03/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$IdMateria=$_GET['idMateria'];
	$Nombre=$_GET['nombre'];
	$Matricula=$_GET['matricula'];	
	$Color=$_GET['color'];
	$Nick=$_GET['nick'];
	//realiza la modificación de la materia
	$sql="UPDATE Materias_IDE 
		SET Nombre='$Nombre',
		Matricula='$Matricula',
		Color='$Color',
		Nick='$Nick'  
		WHERE IdMateria=$IdMateria";
	mysqli_query($conexion,$sql);
	//Revisa que la modificación se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM Materias_IDE 
			WHERE IdMateria=$IdMateria";			
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