<?php
        include 'mysql_connect.php';

    $name = mysqli_real_escape_string($conn, $_GET['name']); 
    
    //This is the polite version of our name
    $politestring = sanitize($name);
    
      //Here's our query to grab a rank.
      $query = "SELECT  uo.*, 
          (
          SELECT  COUNT(*)
          FROM    Score ui
          WHERE   (ui.score, -ui.ts) >= (uo.score, -uo.ts)
          ) AS rank
      FROM    Score uo
      WHERE   name = '$politestring';";
      $result = mysqli_query($conn, $query) or die('Query failed: ' . mysqli_error());
      
      //This is more elaborate than we need, considering we're only grabbing one rank, but you can modify it if needs be.
      $num_results = mysqli_num_rows($result);  
      
      for($i = 0; $i < $num_results; $i++)
      {
           $row = mysqli_fetch_array($result);
           echo $row['rank'] . "\n";
      }

/////////////////////////////////////////////////
// string sanitize functionality to avoid
// sql or html injection abuse and bad words
/////////////////////////////////////////////////
function no_naughty($string) {
    $badwords = file("badwords.txt", FILE_IGNORE_NEW_LINES);
    foreach ($badwords as $badword) {
      $string = preg_replace('/' . $badword . '/i', str_pad("", strlen($badword), '*', STR_PAD_LEFT), $string);
    }


    // ie does not understand "&apos;" &#39; &rsquo;
    $string = preg_replace("/'/i", '&rsquo;', $string);
    $string = preg_replace('/%39/i', '&rsquo;', $string);
    $string = preg_replace('/&#039;/i', '&rsquo;', $string);
    $string = preg_replace('/&039;/i', '&rsquo;', $string);

    $string = preg_replace('/"/i', '&quot;', $string);
    $string = preg_replace('/%34/i', '&quot;', $string);
    $string = preg_replace('/&034;/i', '&quot;', $string);
    $string = preg_replace('/&#034;/i', '&quot;', $string);

    return $string;
}

function my_utf8($string)
{
    return strtr($string,
      "/<>������������ ��Ց������������������������������ԕ���ٞ��������",
      "![]YuAAAAAAACEEEEIIIIDNOOOOOOUUUUYsaaaaaaaceeeeiiiionoooooouuuuyy");
}

function safe_typing($string)
{
    return preg_replace("/[^a-zA-Z0-9 \!\@\%\^\&\*\.\*\?\+\[\]\(\)\{\}\^\$\:\;\,\-\_\=]/", "", $string);
}

function sanitize($string)
{
    // make sure it isn't waaaaaaaay too long
    $MAX_LENGTH = 250; // bytes per chat or text message - fixme?
    $string = substr($string, 0, $MAX_LENGTH);
    $string = no_naughty($string);
    // breaks apos and quot: // $string = htmlentities($string,ENT_QUOTES);
    // useless since the above gets rid of quotes...
    //$string = str_replace("'","&rsquo;",$string);
    //$string = str_replace("\"","&rdquo;",$string);
    //$string = str_replace('#','&pound;',$string); // special case
    $string = my_utf8($string);
    $string = safe_typing($string);
    return trim($string);
}


?>
