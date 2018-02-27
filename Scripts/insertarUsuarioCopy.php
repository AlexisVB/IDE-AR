<?php
	//Script para insertar usuario 03/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
//http://proyectosinformaticatnl.ceti.mx/asistencia-automatizada/IDE-AR/insertarUsuario.php?nombreUsuario=A&password=1234&tipo=0&nombre=A&registro=1234&grupo=8C1&correo=anyk00
	require("conectabd.php");
	//Obtiene los parametros
	$NombreUsuario=$_GET['nombreUsuario'];
	$Password=$_GET['password'];
	$Tipo=$_GET['tipo'];
	$Nombre=$_GET['nombre'];
	$Registro=$_GET['registro'];
	$Grupo=$_GET['grupo'];
	$Correo=$_GET['correo']
	$password=md5($Password);
	
	//realiza la inserción del usuario
	$sql="INSERT INTO Usuarios_IDE(NombreUsuario,Password,Tipo,Nombre,Registro,Grupo,Correo) 
	VALUES('$NombreUsuario','$Password',$Tipo,'$Nombre','$Registro','$Grupo','$Correo')";
	mysqli_query($conexion,$sql);
	//Revisa que la inserción se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM Usuarios_IDE 
			WHERE NombreUsuario='$NombreUsuario' 
			AND Password='$Password' 
			AND Tipo=$Tipo 
			AND Nombre='$Nombre'
			AND Registro='$Registro';
			AND Correo='$Correo'";
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
