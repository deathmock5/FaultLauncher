<?php   
    #Arthor Steven venham
    #Product key verification
    #http://tempestgamers.com/fault-ftp/validateKey.php?fname=Steven&lname=Venham&pid=1&key=2147483647
    #http://tempestgamers.com/fault-ftp/validateKey.php?fname=Steven&lname=Venham&pid=1&key=2147483647
    $host = "localhost";
    $username = "DMClient";
    $pass = "1234";
    $databace = "deathmock";
    $connect = mysql_connect($host,$username,$pass);
    $db = mysql_select_db($databace,$connect);
    $sql = "SELECT * FROM ProductKeys";
    $query = mysql_query($sql,$connect);
    $row = mysql_fetch_array($query);
    #conn made, lets get the values in the url
    $myuserf = $_GET["fname"];
    $myuserl = $_GET["lname"];
    #$myuserc = $_GET["comp"];
    $myuserp = $_GET["pid"];
    $myuserk = $_GET["key"];
    #conn made everythign ready, lets see if theres a match.
    $matchfound = false;
    while($row != NULL && !$matchfound)
    {
        if($myuserk == $row[4])
        {
            if($myuserp == $row[3] && $myuserf == $row[0] && $myuserl == $row[1])
            {
                $matchfound = true;
            }
            else
            {
                #echo "Er key is invalid";
                $row = NULL;
            }
        }
        else
        {
            $row = mysql_fetch_array($query);
        }
    }
    if($matchfound)
    {
        echo 1;
    }
    else
    {
        echo 0;
    }
    mysql_close($connect);
?>