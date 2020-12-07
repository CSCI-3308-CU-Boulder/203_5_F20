# TROUBLE TWIST

## Project Team Members:

Jake Tracy,
James Bohn,
Alexey Yermakov,
James Ryan,
Conor Simmons,
Blake Peery

## What is this?

This project is a multiplayer online game that see people connecting to a host executable by means of their own browsers and competing in a "quote game," with each player making their best guess as to who said the prompt quote! In a year like 2020, having accessible outlets to play games with friends, no matter the distance, is vital and this game is another way to do this!

## How do I play?

### Client:

Go to http://ttgames.us/ in any web browser and fill in the forms: choose a nickname, and type in the game code displayed on the host's screen. This should bring you to a waiting room with the rest of the players until the host starts up the game! Once the game starts, a quote will be displayed with 4 possible answers as to who said it. Make your vote by selecting on your browser. If you got it right, you get a point! Once the host ends the game, the player with the most points wins!

### Host:

#### Building and Running the Game

First, Unity must be installed on the host machine. Create a new project and point to "Unity/QuoteGame" in the repository. Once this folder is pointed to, open that project. Under the "Project" tab in Unity, select "Assets/Scenes." Click on the "Game Start" scene and then open the scene. Now, make a directory on your system entitled "build" inside the Unity project directory. In Unity, click File -> Build and Run and select the "build" folder as the destination. This will compile the game and launch it on your system.

#### Hosting the game

Once the game is running, you simply have to create a lobby, wait for players to join, and start the game! At any point, the host can choose to end the game, displaying the top 3 players and their scores.

## Git Repository Structure

* `LABS`

  * Contains the PDF for Lab 2 for the class

* `MILESTONES`

  * Contains the project milestones for the class

* `Server`

  * Contains server-side code that runs on an Amazon EC2 cloud server. The code is primarily writted in NodeJS.

* `Unity`

  * Contains all files necessary to build the Unity executable (game host application).

* `Website`

  * Contains the full website source code.


## Testing Methodology

Integration Testing: We tested our Unity/Browser integration by hosting and joining games. We ensured that the correct signals were being sent at the correct times, that players could not send signals after a question timer runs out, and that games ended correctly.

Load Testing: We tested the load capacity of the amount of players able to join a game. Between our team members, we had 30-40 players join a single game instance and input their answers to questions. This was plenty of capacity for the intended use of the game.

User Acceptance Testing: We hosted a game for classmates to join and they were able to successfully submit their answers to questions and enjoyed playing the game from all around the country and the world.


