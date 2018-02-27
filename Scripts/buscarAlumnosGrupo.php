<?php
	require("conectabd.php");
	
	$grupo=$_GET['grupo'];//Busca el parametro grupo en la url que viene del visual	

	$sql="SELECT * FROM  Usuarios_IDE WHERE Grupo LIKE '%$grupo%'";	
	$resultado=mysqli_query($conexion,$sql);//realiza una consulta a la base de datos
	if(!$resultado)
	{
		echo "Error";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"usuarios":[';
			for($cont=1;$cont<=$nr;$cont++)
			{
				$row=mysqli_fetch_object($resultado);
				$encode=json_encode($row);
				if(is_string($encode))
				{
					if($cont<$nr)
						$json=$json.$encode.",";
					else
						$json=$json.$encode;
				}
				
			}
			$json=$json."]}";						
			echo "$json";
		}
		else
		{
			$error="No hay alumnos";		
		}
		
	}
	mysqli_close($conexion);
?>