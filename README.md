# TROUBLE TWIST

## Project Team Members:

Jake Tracy,
James Bohn,
Alexey Yermakov,
James Ryan,
Conor Simmons,

## What is this?

This project is a multiplayer online game that see people connecting to a host executable by means of their own browsers and competing in a "quote game," with each player making their best guess as to who said the prompt quote! In a year like 2020, having accessible outlets to play games with friends, no matter the distance, is vital and this game is another way to do this!

## How do I play?

### Client:

Go to http://ttgames.us/ and fill in the forms: choose a nickname, and type in the game code displayed on the host's screen. This should bring you to a waiting room with the rest of the players until the host starts up the game! Once the game starts, a quote will be displayed with 4 possible answers as to who said it. Make your vote by selecting on your browser. If you got it right, you get a point! Once the host ends the game, the player with the most points wins!

### Host:

#### Building the game

TBD

#### Running the game

After acquiring the game executeable either from building the game itself or by downloading and extracting the .zip file in this repo, simply run the .exe file named "Quote Game". When the game is running, you simply have to create a lobby, wait for players to join (you can play too in a browser or on a phone!), and start the game! At any point, the host can choose to end the game, displaying the top 3 players and their scores.

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

We used manual testing for this project primarily due to the fact that the main issues would occur in integration. We performed QA testing by having users try to break the applications. We also performed load testing by having up to 40 unique players join a single lobby at one time.
