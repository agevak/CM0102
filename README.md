# CM0102

# TrueCMScout
24 years passed, CM scout is finally (intended to be) done right.
Features:
1) Correctly calculated in-match attributes. What the game shows are cosmetic values, calculated via completely different
	formulas and not actually used in matches.
	To see in-match attributes in the game itself, use InMatchAttributes patch.
2) Rating model based on bechmark results, not on suggestions.
3) Game save tracking. Allows to instantly see changes after you save the game.
	Create search criteria to find awesome regens and TrueCMScout will notify you about new regen as soon you save
	the game (auto save option comes handy).
4) Handle multiple search criterias as tabs. No need to run multiple instances of the Scout application to track defenders
	and attackers at the same time, etc.

# Benchmarker
Benchmarks whatever .sav file you like.

Simple usage.
1) Download distribution from .
2) Unzip distribution.
3) Put .sav files you want to BM into "Saves" folder.
4) Configure Benchmarker (see below).
5) Run Benchmarker.exe.
6) Wait until finish.
7) View BM results in "Results" folder.

Distribution cointains example configuration, .sav file and BM results for reference.

Idea.
Each .sav file is a Test. For each Test, specified amount of seasons is played (more precisely, 1 seasons several times).
Result is stored in "Results\{TestName}.txt" file lists results for each club in human club's division in these format:
ClubName;AmountOfSeasons;AvgScoredGoalsPerSeasons;AvgConcededGoalsPerSeason;AvgPointsPerSeason;AvgPlace
Also, a line is added to "Results\repository.csv" file in this format:
{TestName};AmountOfSeasons;AvgScoreGoalsPerMatch;AvgConcededGoalsPerMatch;AvgPointsPerMatch;AvgPlace
Completed test will not run again as long as present in "Results\repository.csv".

Configuration.
Configuration parameters are stored in Benchmarker.exe.config file:
  <appSettings>
    <add key="CMFolder" value="C:\Program Files (x86)\Championship Manager 01-02" />
    <add key="SeasonsPerTest" value="1" />
    <add key="MaxThreads" value="1" />
    <add key="TestTimeoutS" value="300" />
    <add key="HideWindow" value="false" />
    <add key="UpdateAttributesForConsistency" value="false" />
  </appSettings>
CMFolder - Path for CM installation.
SeasonsPerTest - Amount of seasons to run per test.
MaxThreads - Max amount of CM processes to run at the same time. Amount of CPU threads is a reasonable value.
TestTimeoutS - Max time to wait for CM to finish season, in seconds.
HideWindow - If to hide running CM windows ("true" of "false").
UpdateAttributesForConsistency - If to update attributes of every player for consistency ("true" of "false"). Specifically:
	InjuryProneness = 1;
	Dirtiness = 1;
	NaturalFitness = 20;
	Consistency = 20;
	ImportantMatches = 20;

Preparing .sav files.
Any .sav file should work, as long as:
1) There is exactly 1 human manager.
2) Human manager controls 1 club. Benchmarker monitors the division where that club plays.
The more players and leagues are enabled in .sav file, the more time BM will take.
It's recommended to BM game saves done on truncated DBs (see DBTruncator tool) or minimal DBs, otherwise BM will be very slow.

How to abort testing / fix stuck testing.
1) Try to abort Benchmarker gracefully. Press [Enter].
2) If not finished gracefully, terminate process via Ctrl-C / close window / kill process.
3) If not finished gracefully, to cleanup running CM instances, run this command:
	taskkill.exe /F /IM cm0102_bm.exe

Using custom cm0102.exe.
CM executable used for benchmarking is not the one from CM installation, but "Template\cm0102_bm.exe".
To use custom CM executable, patch it via BenchmarkMode patch: https://github.com/agevak/CM0102/tree/main/Patches/BenchmarkMode

# TacticsBenchmarker
Benchmarks tactics.

Input:
	- Human tactics to test.
	- .sav file for testing.
	- AI tactics to play against.

Output:

Benchmarking works via CM .exe patched with BenchmarkMode patch using same approach as Benchmark tool.
For each specified human tactic the tool does the following:
	1) Injects human tactic into .sav file. So, all the matches will be played with this tactic ('go on holiday' should be setup
	manually as required beforehand).
	2) If AI tactics were specified, separate test is performed for each specified AI tactic.
	3) If AI tactic is specified, injects it into .sav file. Same tactic replaces the whole AI tactics pack. So, regardless
	of whatever tactic AI manager will choose, it will end up with the same (specified, injected) tactic.
	If AI tactic was not specified, AI tactic pack originally stored in .sav file is used (i.e. AI tactics in the .sav file
	remain untouched).
	4) Plays 1 season the configured amount of times.
	5) Prints the results.
Then you can save the results and export them into HTML.
The results table shows how each human tactics performed vs each AI tactics:
	- Average goals scored per match.
	- Average goals conceded per match.
	- Average points earned per match.
	- Average league placement per season.

# SaveGameTacticsEditor
Updates AI tactics in your game save. For example, if you started the game with AI tactics from ODB, but now want the game to become
more challenging, you can replace AI tactics with stronger packs via this tool.
Please note that updating AI tactics in CM's 'Data' folder makes effect only on new games, but for game saves AI tactics are stored
inside .sav file.

# DBTruncator
Truncates CM DB.
It is intended to be used for bechmarking or to speed up play.
It removes staff (both players and non-players), keeping only the staff from the league you are playing. It also keeps a few more staff
from your league's nation (otherwise CM will crash with 'Database.cpp' error).
The fewer staff will be in DB, the fewer will get into your save game and thus the game will run faster (critical for benchmarking).

# Usage:
1) Run CM and start new game in whatever country you like (choose only 1 country). Save game.
2) Copy save game from step 1 to DBTruncator's 'Input\Save' folder. Only 1 .sav file must be there, so delete all the old ones if any.
3) Optional: backup your CM's 'Data' folder.
4) Copy CM DB from CM's 'Data' folder to 'Input\Data' folder of DBTruncator. Required files are:
	club.dat
	index.dat
	nat_club.dat
	nation.dat
	staff.dat
5) Run DBTruncator.
6) Copy (overwrite) generated files from DBTruncator's 'Output\Data' folder to CM's 'Data' folder.
7) Run CM and start new game with the same country selected as at step 1. Please note that DB size must be set to 'Minimal' in CM settings.

# SaveUnpacker
Unpacks save files. Takes every *.sav file in current directory and extracts content of each 'xxx.sav' file to 'xxx' folder.

# SavePacker.
Repacks save files. Takes every *.sav file in current directory and rebuilds it from file from namesake folder.
I.e. takes 'xxx.sav' and 'xxx' folder and repacks files from 'xxx' folder into 'xxx.sav'. Please note that original .sav file is required.
Expected usage is:
1) Unpack .sav via SaveUnpacker.
2) Edit extracted .dat files as you wish.
3) Pack .sav back.
