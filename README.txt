ScoreTest - 2017-2020 Levi D. Smith support@levidsmith.com

DISCLAIMERS

This is provided AS IS with no expectation of WARRANTY.

I started with code from "Self Hosted PHP/SQL Leaderboard" by Michael James Williams
https://github.com/tutsplus/self-hosted-php-sql-leaderboard
Modifications
- May 2020 - Now can display top scores by name and latest scores.  TopScores.php now defaults to top scores grouped by name
- May 2020 - Keys are now read from the database and unique for each game.  So if a game key is compromised, then only that game needs to be re-keyed

- Fixed SQL table name error
- Put database connection information into its own PHP file so that the connection information only has to be entered once and not for every file
- Added additional column and table to track leaderboards for multiple games
- Unity5 example written from scratch.
- Removed rank tracking

DESCRIPTION

This is an example of how to make a leaderboard for a Unity game using PHP and MySQL.  This requires the user to manually enter their name in a text box.  If the same name is used more than once, then only the highest score for that name is displayed.  This example requires Unity5 for the UI components.

INSTALLATION

The Unity project is in the ScoreTest folder.

The MySQL script to create the database is in "MySQL".  Run this once on your database.  You may have to modify settings as needed for your database.

The PHP files are in the "php" folder.  Host these files on your web server.
	
	You will need to create your own scores/mysql_connect.php and scores/update_key.php

Generate an alphanumeric key, and place it at the appropriate locations in php/AddScore.php file and ScoreTest/Assets/Scripts/LeaderboardManager.cs files.

Update URLs (TopScoresURL and AddScoreURL) in Assets/Scripts/LeaderboardManager.cs to match your web host.

For each game, set the iGameID variable in LeaderboardManager.cs to a unique integer.  I suggest starting with 0 and incrementing from there.  Currently, the game SQL table is not used, but it may be used in the future to track which ID cooresponds to which game.  Therefore, I would suggest adding the game ID and name relation in that table.


POSSIBLE IMPROVEMENTS

- SQL: Add foreign key relation between game.id and score.game
- Unity: Add a timeout if the game has checked the leaderboard X number of times and no data has been returned
- Unity: Fix probable issue with leaderboard display (ScoreDisplay.cs) when there are no matching rows in the score table.  The game should keep checking every second until score values are returned.
- Unity: Remove the UI specific code in the addScore method from LeaderboardManager.cs
- Unity: Modify LeaderboardManager.cs to use the iGameID for addScore instead of the input box value
- Unity: Add constants class to hold all of the values that need to be changed.
- PHP: Add option to display all scores for a given name instead of just the highest score
- Unity/PHP: Add capability to pull player name from an OAuth social media account (Twitter, Google, Facebook, etc)
