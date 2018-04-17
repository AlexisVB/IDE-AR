<?php
	//Script para modificar materia 03/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$IdUsuario=$_GET['idUsuario'];
	$NombreUsuario=$_GET['nombreUsuario'];
	$Tipo=$_GET['tipo'];
	$Nombre=$_GET['nombre'];
	$Registro=$_GET['registro'];
	$Grupo=$_GET['grupo'];
	$Correo=$_GET['correo'];
	//realiza la modificación de la materia
	$sql="UPDATE  Usuarios_IDE SET
	NombreUsuario='$NombreUsuario',  
	 Tipo=$Tipo ,
	 Nombre='$Nombre',
	 Registro='$Registro',
	 Grupo='$Grupo',
	 Correo='$Correo'
	 WHERE IdUsuario=$IdUsuario";			 
	mysqli_query($conexion,$sql);
	//Revisa que la modificación se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM Usuarios_IDE 
			WHERE IdUsuario=$IdUsuario";			
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