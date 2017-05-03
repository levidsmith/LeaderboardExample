<?php
        //You have to fill in this information to connect to your database!
        $db = mysql_connect('yourdatabase.com', 'db_username', 'db_password') or die('Failed to connect: ' . mysql_error());
        mysql_select_db('db_name') or die('Failed to access database');
?>
