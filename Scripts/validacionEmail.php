<?php
	//Script para generar un codigo para la validación del email  10/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$usuario=$_GET['usuario'];
	$password=$_GET['password'];
	$correo=$_GET['correo'];	
	$sql="SELECT * FROM Usuarios_IDE WHERE Correo='$correo'";	
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
			echo "ocupado";
		}
		else
		{
			//si no hay ninguna fila quiere decir que no se encuentra
			$keysafe="NGNVGDF5894XCNJ7";
			$codigo=sha1($usuario.$password.$correo.$keysafe);		
			$key="";
				for($cont=0;$cont<strlen($codigo);$cont+=5)
				{
					$key=$key.$codigo[$cont];
				}
			echo $key;			
		}		
	}
	?>
