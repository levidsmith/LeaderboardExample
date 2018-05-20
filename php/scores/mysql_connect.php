<?php
        //You have to fill in this information to connect to your database!
        $conn = mysqli_connect('*********************************example.com', '****************', '**********!') or die('Failed to connect: ' . mysqli_error());
        mysqli_select_db($conn, '****************') or die('Failed to access database');
?>
