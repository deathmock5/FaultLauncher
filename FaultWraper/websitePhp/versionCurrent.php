<?php   
    #Arthor Steven venham
    #Checks version information
    #http://tempestgamers.com/fault-ftp/versionCurrent.php?pid=1&myVers=1.1.1.1;
    #http://tempestgamers.com/fault-ftp/versionCurrent.php?pid=1&myVers=1.1.1.1;
    $host = "localhost";
    $username = "DMClient";
    $pass = "1234";
    $databace = "deathmock";
    $connect = mysql_connect($host,$username,$pass);
    $db = mysql_select_db($databace,$connect);
    $sql = "SELECT * FROM ProductInformation";
    $query = mysql_query($sql,$connect);
    $row = mysql_fetch_array($query);
    #conn made, lets get the values in the url
    $myuserp = $_GET["pid"];
    $myuserv = $_GET["myVers"];
    #conn made everythign ready, lets see if theres a match.
    $matchfound = false;
    while($row != NULL && !$matchfound)
    {
        if($myuserp == $row[0])
        {
            $matchfound = true;
        }
        else
        {
            $row = mysql_fetch_array($query);
        }
    }
    if($matchfound)
    {
        if($row[1] == $myuserv)
        {
            #versions current
            echo 1;
        }
        else
        {
            #out of date
            echo $row[2];
        }
    }
    else
    {
        echo 0;
    }
    mysql_close($connect);
?>