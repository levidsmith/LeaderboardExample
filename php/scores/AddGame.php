<?php 

        include 'mysql_connect.php';
        include 'update_key.php';
        //These are our variables.

        $name = mysqli_real_escape_string($conn, $_GET['name']); 
        $id = mysqli_real_escape_string($conn, $_GET['id']); 
        $download_url = mysqli_real_escape_string($conn, $_GET['download_url']); 
        $hash = $_GET['hash']; 
        
        //This is your key. You have to fill this in! Go and generate a strong one.
        //Included from update_key.php

		
        //We md5 hash our results.
        $str_date = date("Ymd");
        $expected_hash = md5(urlencode($name) . $id  . $str_date . $secretKey); 

//       echo "hash: ";
//        echo urlencode($name) . $id  . $secretKey;
//        echo "<br/> ";
      
        
        //If what we expect is what we have:
        if($expected_hash == $hash) { 
            // Here's our query to insert/update scores!
            $query = 'INSERT INTO game (id, name, order_method, score_format, metric, download_url) ' .
                     'VALUES (' . $id . ", '" . $name . "'," . 
                     "0, NULL, 'Points Scored', " .
                     "'" . $download_url . "'" . ')' ; 

            //And finally we send our query.
            $result = mysqli_query($conn, $query) or die('Query failed: ' . mysqli_error()); 
            echo "<html><head>";
//            echo '<meta http-equiv="refresh" content="0; url=' . $redirect_url . '" />';
            echo "</head><body>";
            echo "Game Added: " . $name . ", " . $id; 
            echo "</body></html>";
        }  else {
            echo "Invalid hash value<br/>";
//            echo "Expected: ". $expected_hash . " got: " . $hash;
        }



?>
