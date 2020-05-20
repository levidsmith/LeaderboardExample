<?php
    include 'mysql_connect.php';

    if (isset($_GET['game'])) {
      $game = mysqli_real_escape_string($conn, $_GET['game']);
    } else {
      $game = "";
    }

    if (isset($_GET['unique'])) {
      $unique = mysqli_real_escape_string($conn, $_GET['unique']);
      if ($unique != "0") {
        $unique = "1";
      }
    } else {
      $unique = "1";
    }


    $latest = "0";
    if (isset($_GET['latest'])) {
      $latest = mysqli_real_escape_string($conn, $_GET['latest']);
      if ($latest != "0") {
        $latest = "1";
        #If we want to look at latest scores, we want to see duplicates 
        $unique = "0";  
      }
    }

    $score_limit_value = 20;

 
     //This query grabs the top $score_limit_value scores, sorting by score and timestamp.
    $query_game = "SELECT id, name, order_method, score_format, metric, download_url FROM game";
    if ($game != "") {
      $query_game .= " WHERE id = " . $game;
    }
    $query_game .= " ORDER BY name ASC ";
  
    $result = mysqli_query($conn, $query_game) or die('Query failed: ' . mysqli_error());
 
    //We find our number of rows
    $result_length = mysqli_num_rows($result); 

    echo "{\n";
    echo "\t" . '"games": [' . "\n";
    
    //And now iterate through our results
    for($i = 0; $i < $result_length; $i++) {
         $row = mysqli_fetch_array($result);
         echo "\t\t" . '{' . "\n"; 
         echo "\t\t\t" . '"name": "' . $row['name'] .  '",' . "\n"; 
         echo "\t\t\t" . '"download_url": "' . $row['download_url'] .  '",' . "\n"; 
         echo "\t\t\t" . '"metric": "' . $row['metric'] .  '"'; 

         ### BEGIN SCORE QUERY
         if ($unique == "1") {
             if ($row['order_method'] == 0) {
               $query_scores = "SELECT name, MAX(score) as score, ts FROM score";
               $query_scores .= " WHERE game =  " . $row['id'];
               $query_scores .= " GROUP BY name";
               $query_order_by = " ORDER BY score DESC";

             } elseif ($row['order_method'] == 1) {
               $query_scores = "SELECT name, MIN(score) as score, ts FROM score";
               $query_scores .= " WHERE game =  " . $row['id'];
               $query_scores .= " GROUP BY name";
               $query_order_by = " ORDER BY score ASC";
             }
 
         } else {
             $query_scores = "SELECT name, score, ts FROM score";
             $query_scores .= " WHERE game =  " . $row['id'];
             if ($row['order_method'] == 0) {
               $query_order_by = " ORDER BY score DESC";
             } elseif ($row['order_method'] == 1) {
               $query_order_by = " ORDER BY score ASC";
             }
         }

         if ($latest == 1) {
           $query_order_by = " ORDER BY ts DESC"; 
         }

         $query_scores .= $query_order_by;

         $query_scores .= " LIMIT " . $score_limit_value;

         $result_scores = mysqli_query($conn, $query_scores) or die('');
         $result_scores_length = mysqli_num_rows($result_scores); 
         if ($result_scores_length > 0) {
           echo ',' . "\n"; 
           echo "\t\t\t" . '"scores": [' . "\n"; 
         } else { 
           echo "\n"; 
         }

         for($j = 0; $j < $result_scores_length; $j++) {
           $row_scores = mysqli_fetch_array($result_scores);
           echo "\t\t\t\t" . '{' . "\n";
           echo "\t\t\t\t\t" . '"name": "' . $row_scores['name'] . '",' . "\n";

           $score_display = $row_scores['score'];
           if ($row['score_format'] == 'stopwatch') {
#             $score_display = (float)$score_display / 100.0;  
             $mins = floor($score_display / 6000);
             $secs = floor($score_display / 100) % 60; 
             $hundredths = $score_display % 100; 
             $score_display = $mins . ":" . str_pad($secs, 2, "0", STR_PAD_LEFT) . "." . str_pad($hundredths, 2, "0", STR_PAD_LEFT);
           }

           echo "\t\t\t\t\t" . '"score": "' . $score_display . '"' . "\n";
           echo "\t\t\t\t" . '}';
           if ($j < $result_scores_length - 1) {
             echo ',';
           }
           echo "\n"; 
         }

         if ($result_scores_length > 0) {
           echo "\t\t\t" . ']' . "\n"; 
         }

         ### END SCORE QUERY

         echo "\t\t" . '}'; 
         if ($i < $result_length - 1) {
           echo ',';
         }
         echo "\n"; 
    }
    echo "\t" . ']' . "\n";
    echo '}';
?>
