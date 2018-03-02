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
		{*/
			//si no hay ninguna fila quiere decir que no se encuentra
			$keysafe="";
			//generar keysafe
			$codigo=sha1($usuario.$password.$correo.$keysafe);
			for($cont=0;$cont<8;$cont++)
			{
				$pos=rand(0, strlen($codigo)-1);
				$keysafe=$keysafe.$codigo[$pos];
			}
			//dezplazar cadena
			$pos=0;
			while($pos<strlen($codigo)-1)
			{
				for($cont=0;$cont<8;$cont++)
				{
					$codigo[$pos]=$keysafe[$cont]+$codigo[$pos];
					$pos++;
				}
			}
			//obtener el codigo de longitud x			
			$x=8;
			$key="";
			for($cont=0;$cont<8;$cont++)
			{
				$pos=rand(0, strlen($codigo)-1);
				$key=$key.$codigo[$pos];
			}
			echo $key;			
		}		
	}
	?>
