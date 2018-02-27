<?php
	//Script para insertar materia 18/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	//dirBase=\Soluciones\IdPRofesor\IdMateria\IdGrupo\IdActividad\IdAlumno\
	$dirBase=dirname(__FILE__)."\\"."Soluciones";
	$IdProfesor=$_GET['IdProfesor'];
	$IdMateria=$_GET['IdMateria'];
	$IdGrupo=$_GET['IdGrupo'];
	$IdActividad=$_GET['IdActividad'];
	$IdAlumno=$_GET['IdAlumno'];
	$Nombre=$_GET['Nombre'];
	//creación de la ruta	
	$Ruta=$dirBase."\\".$IdProfesor."\\".$IdMateria."\\".$IdGrupo."\\".$IdActividad."\\".$IdAlumno."\\".$Nombre;	
	$Fecha=$_GET['Fecha'];
	//realiza la inserción de la solución
	$sql="INSERT INTO SolucionProyecto_IDE(Nombre,Ruta,IdPropietario,IdActividad,Fecha) VALUES('$Nombre','$Ruta',$IdAlumno,
	$IdActividad,'$Fecha')";
	mysqli_query($conexion,$sql);
	//Revisa que la inserción se haya realizado correctamente haciendo una consulta con los datos anteriores
	$sql="SELECT * FROM SolucionProyecto_IDE 
			WHERE Nombre='$Nombre' 
			AND Ruta='$Ruta' 
			AND IdPropietario=$IdAlumno 
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
			$error="{}";
			echo $error;		
		}		
	}
	mysqli_close($conexion);
?>