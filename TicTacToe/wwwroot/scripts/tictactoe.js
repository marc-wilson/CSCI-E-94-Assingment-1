/**
 * @summary GameBoardRequest Model for sending a gameBoard Payload to the server
 * @class GameBoardRequest
 */
class GameBoardRequest {
	constructor() {
		/**
		 * @param {string[]} gameBoard An array of string characters (X, O or ?)
		 */
		this.gameBoard = new Array(9).fill('?');
	}
	/**
	 * @summary move function to execute a move on the gameBoard
	 * @param {number} position Position of space to put the move (0 - 8)
	 * @param {string} playerSymbol Symbol of player making move (X or O)
	 * @returns GameBoardRequest with the updated move
	 */
	move(position, playerSymbol) {
		// Set the game board position to given symbol
		this.gameBoard[position] = playerSymbol;
		// return the game board
		return this;
	}
}

/**
 * @summary GameBoardResponse model for the response from the server
 * @class GameBoardResponse
 * @param {string} azurePlayerSymbol Symbol of the Azure Player (X or O)
 * @param {string[]} gameBoard An array of string characters (X, O or ?)
 * @param {number} move The index on the game board in which the move was made
 * @param {number[]} winPositions The winning combination of the resulting game win. null if no winner or tie
 * @param {string} winner The winner of the game (X or O). Or the result of the game being inconclusive or tie
 * */
class GameBoardResponse {
	constructor(azurePlayerSymbol, gameBoard, move, winPositions, winner) {
		this.azurePlayerSymbol = azurePlayerSymbol;
		this.gameBoard = gameBoard;
		this.move = move;
		this.winPositions = winPositions;
		this.winner = winner;
	}
}

/**
 * @summary Main TicTacToe Model for the page view
 * @class TicTacToe
 * @property {string} playerSymbol Symbol of the Player (X or O)
 * @property {GameBoardRequest} gameBoardRequest The current game board request
 * @property {Element} gameBoardElement The HTML Element of the game board
 * @property {boolean} gameComplete Whether or not the game is completed
 * @property {boolean} gameInProgress Whether or not the game has started
 * */
class TicTacToe {
	constructor() {
		this.playerSymbol = 'X';
		this.gameBoardRequest = new GameBoardRequest();
		this.gameBoardElement = document.getElementById('gameBoard');
		this.gameComplete = false;
		this.gameInProgress = false;
		this.init();
	}
	/**
	 * @summary Setter for keeping the HTML game boad up to date with the model
	 * @property {string[]} gameBoard Setter for keeping the HTML game boad up to date with the model
	 */
	set gameBoard(val) {
		if (val) {
			// set the game board property in the request object
			this.gameBoardRequest.gameBoard = val;
			// get all the cells in the game board and update them with the appropriate player move/symbol
			this.gameBoardElement.querySelectorAll('.cell').forEach((cell, idx) => {
				// get the symbol
				const symbol = val[idx];
				// set the inner text of the correct cell with the symbol
				// Instead of using a '?' to identify an empty space, use an empty string to make it more intuitive
				cell.innerText = symbol === 'X' || symbol === 'O' ? symbol : '';
			});
		}
	}
	/**
	 * @summary Getter for converting HTML Game Board into a game board array
	 * @property {string[]} gameBoard Getter for converting HTML Game Board into a game board array
	 * @returns {string[]} Game board (array of strings (X/O/?))
	 */
	get gameBoard() {
		// Make sure the gameboardElement exists
		if (this.gameBoardElement) {
			// Intialize an empty array
			const gameBoard = [];
			// Get all cells in the game board and iterate through the board and get the value of the cell
			this.gameBoardElement.querySelectorAll('.cell').forEach((cell, idx) => {
				// Get the inner text of the cell
				const val = cell.innerText;
				// Push the appropriate space symbol to the gameboar array (X, O, or ? if empty)
				gameBoard.push(val === 'X' || val === 'O' ? val : '?');
			});
			// Update the gameBoard on the request object
			this.gameBoardRequest.gameBoard = gameBoard;
			// return the GameBoard
			return gameBoard;
		}
		// Fall back to return an empty gameboard if something goes wrong. Shouldn't ever happen
		return new GameBoardRequest().gameBoard;
	}
	/**
	 * @summary Initializes the page view and binds events
	 * @returns {void}
	 * */
	init() {
		// Get the radio button ref for playerX selection
		const playerSymbolX = document.getElementById('playerX');
		// Get the radio button ref for playerO selection
		const playerSymbolO = document.getElementById('playerO');
		// Bind an event listener to watch for changes in player preference
		playerSymbolX.addEventListener('change', this.onPlayerSymbolChange.bind(this));
		// Bind an event listener to watch for changes in player preference
		playerSymbolO.addEventListener('change', this.onPlayerSymbolChange.bind(this));
		// Bind an event listener to all of the cells so that when a user makes a move/selects a cell on the gameboard
		this.gameBoardElement.querySelectorAll('.cell').forEach(c => {
			c.addEventListener('click', this.onCellClick.bind(this));
		});
	}
	/**
	 * @summary Event that is triggered anytime the user switches their preferred player symbol. Disabled after the first move
	 * @param {Event} evt Change event of the radio button
	 * @returns {void}
	 */
	onPlayerSymbolChange(evt) {
		const symbol = evt.target.value;
		if (!this.gameState) {
			this.playerSymbol = symbol;
		}
	}
	/**
	 @summary function used to disable the player selection radio buttons after the first move
	 @returns {void}
	 */
	disablePlayerSelection() {
		// Set the game as in progress
		this.gameInProgress = true;
		// Get the references to the radio buttons for player selection only
		const inputs = document.querySelectorAll('#playerSelection input[type="radio"]');
		// Update the disabled attribute to true
		inputs.forEach(i => i.setAttribute('disabled', true));
	}
	/**
	 * @summary Event fires on a game board move (a cell has been clicked)
	 * @param {Event} evt
	 * @returns {void}
	 */
	async onCellClick(evt) {
		// If the game is not in progress, it's the first move
		if (!this.gameInProgress) {
			// call the disablePlayerSelection which disables radio buttons and sets the game as in progress
			this.disablePlayerSelection();
		}
		// obtain the elementId in which the user clicked on
		const id = evt.target.id;
		// Check to make sure the id is truthy and the game is not completed yet
		if (id && !this.gameComplete) {
			// Check to make sure the cell clicked doens't container a move
			const requestedMove = document.getElementById(id);
			if (requestedMove && (requestedMove.innerText === 'X' || requestedMove.innerText === 'O')) {
				return;
			}
			// Remove the helper text of the id attribute (we just need the position index)
			const position = id.replace(/position/, '');
			// execute a move on the GameBoardRequest
			this.gameBoardRequest.move(+position, this.playerSymbol);
			// Send the request to the server and wait for a response
			const result = await this.postGameBoard(this.gameBoardRequest);
			// Set the current (local) gameboard with the response from the server
			this.gameBoard = result.gameBoard;
			// Check to see if there was a winner
			if (result.winner) {
				// A winner is populated, determine what type of winner it is
				// Initialize the winText as an empty string
				let winText = '';
				switch (result.winner) {
					// If result.winner is either an X or O, it's an actual winner
					case 'X':
					case 'O':
						// Update the winText with the winner and it's winning combo
						winText = `Player ${result.winner} is the winner! Winning combo is ${result.winPositions.join(', ')}`;
						// Mark the game as domplete
						this.gameComplete = true;
						break;
					// If result.winner is a 'tie', the game is finished but nobody won
					case 'tie':
						// Set the winText to display a tie occurred
						winText = `The Game is over and the result is a tie`;
						break;
					// The game is still in progress and moves are still available
					case 'inconclusive':
						// Set winText to show the game is currently in progress along with the last move and who did it
						winText = `The Game is in progress. Last move was at position ${result.move} by ${result.azurePlayerSymbol}`;
						break;
				}
				// Set the HTML winner element's inner text to the winText
				document.getElementById('winner').innerText = winText;
			}
		}
	}
	/**
	 * @summary Function that posts the gameboard to the server
	 * @param {string[]} gameBoard the gameboard array of valid characters
	 * @returns {GameBoardResponse} GameBoardResponse model that shows game information
	 */
	async postGameBoard(gameBoard) {
		// API relative path
		const url = '/api/tictactoe/executemove';
		// result of the POST operation to the server (uses fetch API)
		const result = await fetch(url, {
			headers: { 'Content-Type': 'application/json' }, // Set headers to work with json
			method: 'POST', // Set the Http Verb as POST
			body: JSON.stringify(gameBoard) // stringify (serialize) the game board before sending it off to the server
		});
		// wait for the response
		const response = await result.json();
		// Return an instantiated GameBoardResponse
		return new GameBoardResponse(response.azurePlayerSymbol, response.gameBoard, response.move, response.winPositions, response.winner);
	}
	
}

// Once the window has loaded, instantiate a new TicTacToe class
window.onload = () => new TicTacToe();