<?php

        include 'mysql_connect.php';
        //These are our variables.
        //We use real escape string to stop people from injecting. We handle this in Unity too, but it's important we do it here as well in case people extract our url.
        $name = mysql_real_escape_string($_GET['name'], $db);
        $score = mysql_real_escape_string($_GET['score'], $db);
        $game = mysql_real_escape_string($_GET['game'], $db);
        $hash = $_GET['hash'];

        //This is the polite version of our name
        $politestring = sanitize($name);

        $secretKey="randomlygeneratedkey";

        //We md5 hash our results.
        $expected_hash = md5($name . $score . $game . $secretKey);

        //If what we expect is what we have:
        if($expected_hash == $hash) {
            // Here's our query to insert/update scores!
            $query = "INSERT INTO score
SET name = '$politestring'
   , score = '$score'
   , game = '$game'
   , ts = CURRENT_TIMESTAMP
ON DUPLICATE KEY UPDATE
   ts = if('$score'>score,CURRENT_TIMESTAMP,ts), score = if ('$score'>score, '$score', score);";
            //And finally we send our query.
            $result = mysql_query($query) or die('Query failed: ' . mysql_error());
        }

/////////////////////////////////////////////////
// string sanitize functionality to avoid
// sql or html injection abuse and bad words
/////////////////////////////////////////////////
function no_naughty($string)
{

//LDS - Put bad word replacement here.  I took these out to keep search engines from flagging this code
    $string = preg_replace('/badword/i', '*******', $string);

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
      "/<>???????????? ??????????????????????????????????????????????",
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
//    $string = my_utf8($string);
    $string = safe_typing($string);
    return trim($string);
}

?>
