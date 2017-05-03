ScoreTest - 2017 Levi D. Smith support@levidsmith.com @GaTechGrad

This is provided AS IS with no expectation of WARRANTY.

I started with code from "Self Hosted PHP/SQL Leaderboard" by Michael James Williams
https://github.com/tutsplus/self-hosted-php-sql-leaderboard
Modifications
- Fixed SQL table name error
- Put database connection information into its own PHP file
- Added additional column and table to track leaderboards for multiple games
- Unity5 example written from scratch.
- Removed rank tracking

This is an example of how to make a leaderboard for a Unity game using PHP and MySQL.  This example requires Unity5 for the UI components.

The Unity project is in the ScoreTest folder.

The MySQL script to create the database is in "MySQL".  Run these once on your database.

The PHP files are in the "php" folder.  Host these files on your web server.

Generate an alphanumeric key, and place it in the php/AddScore.php file and ScoreTest/Assets/Scripts/LeaderboardManager.cs files.