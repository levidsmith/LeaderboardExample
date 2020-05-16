<?php

        $name="Test";
        $score="9999";
        $game="6263";
        $secretKey="*****";

        //We md5 hash our results.
        $expected_hash = md5($name . $score . $game . $secretKey);
        echo "Hash: " . $expected_hash
?>

