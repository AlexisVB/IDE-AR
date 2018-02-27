<?php
	require("conectabd.php");
	$idusuario1=$_GET['idusuario1'];	
	$idusuario2=$_GET['idusuario2'];
	$sql="SELECT * FROM Chat_IDE WHERE (IdRemitente=$idusuario1 OR IdRemitente=$idusuario2)AND(IdDestinatario=$idusuario1 OR IdDestinatario=$idusuario2) ORDER by FechaEnvio";	
	$resultado=mysqli_query($conexion,$sql);	
	if(!$resultado)
	{
		echo "null";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"mensajes":[';
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
			$error="null";
			echo $error;		
		}		
	}
	mysqli_close($conexion);
?>