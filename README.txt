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

Update URLs (TopScoresURL and AddScoreURL) in Assets/Scripts/LeaderboardManager.cs to match your web host.

Possible improvements
- SQL: Add foreign key relation between game.id and score.game
- Unity: Add a timeout if the game has checked the leaderboard X number of times and no data has been returned
- Unity: Fix probable issue with leaderboard display (ScoreDisplay.cs) when there are no matching rows in the score table.  The game should keep checking every second until score values are returned.
- Unity: Remove the UI specific code in the addScore method from LeaderboardManager.cs
- PHP: Add ability to display latest scores
