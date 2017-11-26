<?php
        //You have to fill in this information to connect to your database!
        $db = mysql_connect('***HOSTNAME***', '***DBNAME***', '***PASSWORD***') or die('Failed to connect: ' . mysql_error());
        mysql_select_db('***DBNAME***') or die('Failed to access database');
?>
