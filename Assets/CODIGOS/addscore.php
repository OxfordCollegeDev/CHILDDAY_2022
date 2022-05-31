<?php
$hostname = 'localhost';
$username = 'root';
$password = '';
$database = 'oxcolleg_camp_users';
$secretKey = "mySecretKey";
 
try 
{
	$dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, 
           $username, $password);
} 
catch(PDOException $e) 
{
	echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() 
            ,'</pre>';
}

$hash = $_GET['hash'];
$realHash = hash('sha256', $_GET['dni'] . $_GET['oxfordcredits'] . $secretKey);
	
if($realHash == $hash) 
{ 
	$sth = $dbh->prepare('INSERT INTO usuarios VALUES (null, :dni, :id)');
	try 
	{
        $sth->bindParam(':dni', $_GET['dni'], 
                  PDO::PARAM_STR);
		$sth->bindParam(':oxfordcredits', $_GET['oxfordcredits'], 
                  PDO::PARAM_STR);
		$sth->execute();
	}
	catch(Exception $e) 
	{
		echo '<h1>An error has ocurred.</h1><pre>', 
                 $e->getMessage() ,'</pre>';
	}
}
?>