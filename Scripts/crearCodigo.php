<?php
	//Script para generar un codigo para la validación del email  05/02/2018
	//Alexis daniel Villicaña Barrera-IDE_AR
	//Conexion a la base de datos
	require("conectabd.php");
	//Obtiene los parametros
	$usuario=$_GET['usuario'];
	$password=$_GET['password'];
	$correo=$_GET['correo'];
	$keysafe="NGNVGDF58947"
	$codigo=sha1($usuario.$password.$correo.$keysafe);

	echo($codigo);
	echo strlen($codigo);

	?>
