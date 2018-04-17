<?php
	require("conectabd.php");
	
	$idGrupo=$_GET['idGrupo'];	
	$idUsuario=$_GET['idUsuario'];		

	$sql="INSERT INTO IntegrantesGrupo_IDE Values ($idGrupo,$idUsuario)";	
	mysqli_query($conexion,$sql);
	$sql="SELECT * FROM IntegrantesGrupo_IDE WHERE IdGrupo=$idGrupo AND IdUsuario=$idUsuario";	
	$resultado=mysqli_query($conexion,$sql);
	if(!$resultado)
	{
		echo "Error\n";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	
			echo "Insertado\n";
		}
		else
		{
			$error="Error\n";		
		}
		
	}
	mysqli_close($conexion);
?>