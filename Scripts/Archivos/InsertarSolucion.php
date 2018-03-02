<?php
	//Script para insertar materia 18/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("../conectabd.php");
	//Obtiene los parametros
	//dirBase=\Soluciones\IdPRofesor\IdMateria\IdGrupo\IdActividad\IdAlumno\
	$dirBase=dirname(__FILE__)."/"."Soluciones";
	$Nombre=$_GET['Nombre'];
	$ruta=$_GET['Ruta'];
	$IdPropietario=$_GET['IdPropietario'];
	$IdActividad=$_GET['IdActividad'];
	$Fecha=$_GET['Fecha'];
	//creación de la ruta	
	$Ruta=$dirBase."/".$ruta;
	//realiza la busqueda 	
	$sql="SELECT * FROM SolucionesProyecto_IDE 
			WHERE IdPropietario=$IdPropietario 
			AND IdActividad=$IdActividad";
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
			//ya existe hay que actualizar
			$sql="UPDATE SolucionesProyecto_IDE
			SET Nombre='$Nombre',
				Ruta='$Ruta',
				IdPropietario=$IdPropietario,
				IdActividad=$IdActividad,
				Fecha='$Fecha'
				WHERE IdPropietario=$IdPropietario
				AND IdActividad=$IdActividad";
			mysqli_query($conexion,$sql);
		}
		else
		{
			//No existe hay que intsertar
			//realiza la inserción de la solución
			$sql="INSERT INTO SolucionesProyecto_IDE(Nombre,Ruta,IdPropietario,IdActividad,Fecha) VALUES('$Nombre','$Ruta',$IdPropietario,$IdActividad,'$Fecha')";
			mysqli_query($conexion,$sql);
			//Revisa que la inserción se haya realizado correctamente haciendo una consulta con los datos anteriores
				
		}
		$sql="SELECT * FROM SolucionesProyecto_IDE 
					WHERE Nombre='$Nombre' 
					AND Ruta='$Ruta' 
					AND IdPropietario=$IdPropietario 
					AND IdActividad=$IdActividad
					AND Fecha='$Fecha'";
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
					$error="{}\n";
					echo $error;		
				}		
			}		
	}
	
	mysqli_close($conexion);
?>