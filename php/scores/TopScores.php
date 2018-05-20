<?php
    include 'mysql_connect.php';

        $game = mysqli_real_escape_string($conn, $_GET['game']);
        $order_by = mysqli_real_escape_string($conn, $_GET['order_by']);
 
     //This query grabs the top 10 scores, sorting by score and timestamp.
    $query = "SELECT * FROM score WHERE game=$game ";
    if ($order_by == 'ASC') {
      $query .= " ORDER BY score ASC, ";
    } elseif ($order_by == 'DESC') {
      $query .= " ORDER BY score DESC, ";
    } else {
      $query .= " ORDER BY score DESC, ";

    }
    $query .= "ts ASC LIMIT 10";
   
    $result = mysqli_query($conn, $query) or die('Query failed: ' . mysqli_error());
 
    //We find our number of rows
    $result_length = mysqli_num_rows($result); 
    
    //And now iterate through our results
    for($i = 0; $i < $result_length; $i++)
    {
         $row = mysqli_fetch_array($result);
         echo $row['name'] . "\t" . $row['score'] . "\n"; // And output them
    }
?>
