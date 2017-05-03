<?php
    include 'mysql_connect.php';

        $game = mysql_real_escape_string($_GET['game'], $db);

     //This query grabs the top 10 scores, sorting by score and timestamp.
    $query = "SELECT * FROM score WHERE game=$game ORDER by score DESC, ts ASC LIMIT 10";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());

    //We find our number of rows
    $result_length = mysql_num_rows($result);

    //And now iterate through our results
    for($i = 0; $i < $result_length; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['name'] . "\t" . $row['score'] . "\n"; // And output them
    }
?>
