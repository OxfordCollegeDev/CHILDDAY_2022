<?php
$hostname = 'localhost';
$username = 'root';
$password = '';
//$username = 'ox_camp';
//$password = 'Entropia_1977';
$database = 'oxcolleg_camp_users';

$secretKey = "mySecretKey";

try 
{
	$dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, 
         $username, $password);
} 
catch(PDOException $e) 
{
	echo '<h1>An error has occurred.</h1><pre>', $e->getMessage()
            ,'</pre>';
}

$hash = $_GET['hash'];
$realHash = hash('sha256', $_GET['dni'] . $secretKey);

if($realHash == $hash) {
	$sth = $dbh->query('SELECT * FROM usuarios WHERE dni = '.$_GET['dni'].' AND estado = 1');
	$sth->setFetchMode(PDO::FETCH_ASSOC);
	 
	$result = $sth->fetchAll();
	 
	if (count($result) > 0) 
	{
		foreach($result as $r) 
		{
			$nombre_completo = $r['nombre']." ".$r['apellido'];
			echo "_". $r['id'];
			echo "_". $r['oxfordcredits'];
			echo "_". $nombre_completo;
		}
	} else {
		echo "_0";
		echo "_0";
	}
}
?>