<?php
	require("conectabd.php");
	$idusuario1=$_GET['idusuario1'];	
	$idusuario2=$_GET['idusuario2'];
	$sql="SELECT * FROM mensajes WHERE (id_remitente=$idusuario1 OR id_remitente=$idusuario2)AND(id_destinatario=$idusuario1 OR id_destinatario=$idusuario2) ORDER by id_mensaje";	
	$resultado=mysqli_query($conexion,$sql);	
	if(!$resultado)
	{
		echo "null";
	}
	else
	{		
		$nr=mysqli_num_rows($resultado);
		if($nr>=1)
		{	$json='{"chat":[';
			for($cont=1;$cont<=$nr;$cont++)
			{
				$row=mysqli_fetch_object($resultado);
				$encode=json_encode($row);
				if(is_string($encode))
				{
					if($cont<$nr)
						$json=$json.$encode.",\n";
					else
						$json=$json.$encode."\n";
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