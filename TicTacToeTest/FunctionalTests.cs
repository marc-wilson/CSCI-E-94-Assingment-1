using Microsoft.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToeSdk;
using TicTacToeSdk.Models;

namespace TicTacToeTest.FunctionalTests
{
	[TestClass]
	public class FunctionalTests
	{
		private readonly TicTacToeSdkClient _client = new TicTacToeSdkClient(new Uri("https://localhost:44391"), new TokenCredentials("123"));

		/// <summary>
		/// Test to make sure an incomplete game is inconclusive
		/// </summary>
		[TestMethod]
		public void ShouldEvaluatBoardAsInconclusive()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" };
			var response = _client.ExecuteMove(board);
			Assert.AreEqual(response.Winner, "inconclusive");
		}

		/// <summary>
		/// Test to make sure the Winner is O if O wins the game
		/// </summary>
		[TestMethod]
		public void ShouldEvaluateGameOfWinner_O()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>(){
				"X", "X", "O",
				"O", "O", "X",
				"O", "X", "?"
			};
			var response = _client.ExecuteMove(board);
			Assert.AreEqual("O", response.Winner);
			Assert.AreEqual(2, response.WinPositions[0]);
			Assert.AreEqual(4, response.WinPositions[1]);
			Assert.AreEqual(6, response.WinPositions[2]);
		}

		/// <summary>
		/// Test to make sure the Winner is X if X wins the game
		/// </summary>
		[TestMethod]
		public void ShouldEvaluateGameOfWinner_X()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>(){
				"X", "X", "X",
				"O", "O", "X",
				"O", "O", "?"
			};
			var response = _client.ExecuteMove(board);
			Assert.AreEqual("X", response.Winner);
			Assert.AreEqual(0, response.WinPositions[0]);
			Assert.AreEqual(1, response.WinPositions[1]);
			Assert.AreEqual(2, response.WinPositions[2]);
		}

		/// <summary>
		/// Test to make sure a Tie game is detected if game is finished with no winner
		/// </summary>
		[TestMethod]
		public void ShouldEvaluateGameAsTie()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>(){
				"X", "O", "X",
				"O", "X", "X",
				"O", "X", "O"
			};
			var response = _client.ExecuteMove(board);
			Assert.AreEqual("tie", response.Winner);
			Assert.IsNull(response.WinPositions);
		}

		/// <summary>
		/// Test to make sure the AzurePlayerSymbol is 'X' if it executes the first move on an empty game board
		/// </summary>
		[TestMethod]
		public void VerifyAzurePlayerSymbolIsXIfFirstMove()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" };
			var response = _client.ExecuteMove(board);
			Assert.AreEqual("X", response.AzurePlayerSymbol);
		}

		/// <summary>
		/// Test to make sure Azure Player Symbol is O if a game board passed in has a single move of X
		/// </summary>
		[TestMethod]
		public void VerifyAzurePlayerSymbolIsXIfGameBoardHasOneMoveOfX()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "X", "?", "?", "?", "?", "?", "?", "?", "?" };
			var response = _client.ExecuteMove(board);
			Assert.AreEqual("O", response.AzurePlayerSymbol);
		}

		/// <summary>
		/// Test to make sure an exception is thrown when an game board with a bad length is provided
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_WrongLength()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "X", "?", "?", "?", "?", "?", "?", "?" };
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex )
			{
				Assert.IsNotNull(ex);
			}
		}

		/// <summary>
		/// Test to make sure an exception is thrown when an game board with a an invalid character is provided
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_WrongChar()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "$", "?", "?", "?", "?", "?", "?", "?", "?" };
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		/// <summary>
		/// Test to make sure an exception is thrown when an game board with a an lowercase character is provided
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_LowerChar()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "x", "?", "?", "?", "?", "?", "?", "?", "?" };
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		/// <summary>
		/// Test to make sure an exception is thrown when a game board with invalid plays is provided
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_TooManyXMoves()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "X", "X", "X", "?", "?", "?", "?", "?", "O" };
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		/// <summary>
		/// Test to make sure an exception is thrown when a game board with invalid plays is provided
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_TooManyOMoves()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "X", "O", "O", "O", "?", "?", "?", "?", "O" };
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		/// <summary>
		/// Test to make sure an exception is thrown when a game board with an empty string is provided
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_EmptyString()
		{
			var board = new GameBoardRequest();
			board.GameBoard = new List<string>() { "X", "X", "O", "?", "?", "?", "?", "?", "" };
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		/// <summary>
		/// Test to make sure an exception is thrown when a game board is null
		/// </summary>
		[TestMethod]
		public void ShouldThrowBadRequestForInvalidGameBoard_NullBoard()
		{
			var board = new GameBoardRequest();
			board.GameBoard = null;
			try
			{
				_client.ExecuteMove(board);
			}
			catch (Exception ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		#region Extra Credit

		/// <summary>
		/// Test to make sure the the resuling gameboard in calculatemove is correct
		/// </summary>
		[TestMethod]
		public void CalculateMoveShouldMakeAValidMove()
		{
			var request = new GameBoardRequestEC();
			request.PlayerSymbol = "X";
			request.GameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" };
			var response =_client.CalculateMove(request);
			Assert.IsNotNull(response.Move);
		}

		/// <summary>
		/// Unit test so that both ExecuteMove and CalculateMove play against each other
		/// </summary>
		[TestMethod]
		public void ShouldFinishAGameWhenAzurePlayerMovesFirst()
		{
			// Set game state to inconclusive
			var gameFinished = false;
			// Winning or last game move execution responses
			GameBoardResponse azurePlayerResponse = null;
			GameBoardResponseEC calculatePlayerResponse = null;
			// Variable to keep track of the last player to move
			var lastPlayerToMove = string.Empty;
			// Start with an empty game board
			var gameBoard = new List<string>() { "?", "?", "?", "?", "?", "?", "?", "?", "?" };
			// Create a variable that serves as the Azure Player
			var azurePlayer = new GameBoardRequest() { GameBoard = gameBoard };
			// Create a variable that serves as the Calculate Move Player
			var calculateMovePlayer = new GameBoardRequestEC() { GameBoard = gameBoard };

			//// Make the first move as the Azure Player
			//var azureResponse = await _client.ExecuteMoveAsync(azurePlayer);
			//calculateMovePlayer.PlayerSymbol = azureResponse.AzurePlayerSymbol == "X" ? "O" : "X";

			// Start playing the game and alternating between Azure Player and Calculate Move Player
			while (gameFinished != true)
			{
				// Count both game boards to determine who's turn it is.
				// Count the Azure Players game board moves
				var azurePlayerMoveCount = azurePlayer.GameBoard.Count(x => x == "X" || x == "O");
				// Count the Calculate Players game board moves
				var calculateMovePlayerMoveCount = calculateMovePlayer.GameBoard.Count(x => x == "X" || x == "O");

				// Nobody has made a move yet
				if (azurePlayerMoveCount == 0 && calculateMovePlayerMoveCount == 0)
				{
					// Execute the first move for Azure Player
					azurePlayerResponse = _client.ExecuteMove(azurePlayer);
					// Check to see if winPositions is not null. If it's not, that means theres a winner
					if (azurePlayerResponse.WinPositions != null)
					{
						//Set the calculateplayerresponse with the updated shared game board info
						calculatePlayerResponse.Winner = azurePlayerResponse.Winner;
						calculatePlayerResponse.WinPositions = azurePlayerResponse.WinPositions;
					}
					
					Assert.IsFalse(gameFinished);
					// Set the lastPlayerMoved to Azure Player
					lastPlayerToMove = "AzurePlayer";
					// The first move should have an inconclusive winner
					Assert.AreEqual("inconclusive", azurePlayerResponse.Winner);
					// Update the azure players original games board
					azurePlayer.GameBoard = azurePlayerResponse.GameBoard;
					// Make the calculate move player have the oposite player symbol of the azure player
					calculateMovePlayer.PlayerSymbol = azurePlayerResponse.AzurePlayerSymbol == "X" ? "O" : "X";
					// Update the calculate move players game board
					calculateMovePlayer.GameBoard = azurePlayerResponse.GameBoard;
					// Check if game should be finished
					gameFinished = azurePlayerResponse.Winner == "tie" || azurePlayerResponse.Winner == "X" || azurePlayerResponse.Winner == "O" ? true : false;

				}
				else if (lastPlayerToMove == "AzurePlayer")
				{
					// Last player to move was Azure Player. Call the calculatemove method instead of executemove
					calculatePlayerResponse = _client.CalculateMove(calculateMovePlayer);
					// Check to see if winPositions is not null. If it's not, that means theres a winner
					if (calculatePlayerResponse.WinPositions != null)
					{
						//Set the azurePlayerResponse with the updated shared game board info
						azurePlayerResponse.Winner = calculatePlayerResponse.Winner;
						azurePlayerResponse.WinPositions = calculatePlayerResponse.WinPositions;
					}
					
					// Set the lastPlayerMoved to Calculate Player
					lastPlayerToMove = "CalculatePlayer";
					// Update both of the gameboards to keep them in sync
					azurePlayerResponse.GameBoard = calculatePlayerResponse.GameBoard;
					calculateMovePlayer.GameBoard = calculatePlayerResponse.GameBoard;
					azurePlayer.GameBoard = calculatePlayerResponse.GameBoard;
					// Check if game should be finished
					gameFinished = calculatePlayerResponse.Winner == "tie" || calculatePlayerResponse.Winner == "X" || calculatePlayerResponse.Winner == "O" ? true : false;


				}
				else if (lastPlayerToMove == "CalculatePlayer")
				{
					// Last player to move was Calculate Player. Call the executemove method instead of calculatemove
					azurePlayerResponse = _client.ExecuteMove(azurePlayer);
					// Check to see if winPositions is not null. If it's not, that means theres a winner
					if (azurePlayerResponse.WinPositions != null)
					{
						//Set the calculateplayerresponse with the updated shared game board info
						calculatePlayerResponse.Winner = azurePlayerResponse.Winner;
						calculatePlayerResponse.WinPositions = azurePlayerResponse.WinPositions;
					}
					
					// Set the lastPlayerMoved to Calculate Player
					lastPlayerToMove = "AzurePlayer";
					// Update both of the gameboards to keep them in sync
					calculatePlayerResponse.GameBoard = azurePlayerResponse.GameBoard;
					calculateMovePlayer.GameBoard = azurePlayerResponse.GameBoard;
					azurePlayer.GameBoard = azurePlayerResponse.GameBoard;
					// Check if game should be finished
					gameFinished = azurePlayerResponse.Winner == "tie" || azurePlayerResponse.Winner == "X" || azurePlayerResponse.Winner == "O" ? true : false;
				}
			}

			// azure player response should be set to a response and not null
			Assert.IsNotNull(azurePlayerResponse);
			// calculate player response should be set to a response and not null
			Assert.IsNotNull(calculatePlayerResponse);
			// Both the Azure and Calculate player should have the same result as a winner
			Assert.AreEqual(calculatePlayerResponse.Winner, azurePlayerResponse.Winner);
			// Both Game Boards should match eachother
			var azureGameBoard = String.Join("", azurePlayerResponse.GameBoard.ToArray());
			var calculateGameBoard = String.Join("", calculatePlayerResponse.GameBoard.ToArray());
			Assert.AreEqual(azureGameBoard, calculateGameBoard);
		}

		#endregion
	}
}
