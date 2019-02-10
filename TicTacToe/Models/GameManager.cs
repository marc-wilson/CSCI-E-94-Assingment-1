using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Enums;

namespace TicTacToe.Models
{
	/// <summary>
	/// Main class for making moves and evaluating a game board to determine a winner
	/// </summary>
	public class GameManager
	{
		/// <summary>
		/// Evaluates the current game board to determine a game winner state and return the response
		/// </summary>
		/// <returns>GameEvaluationResult</returns>
		public GameEvaluationResult EvaluateGame(List<string> gameBoard)
		{
			// Const string for winning combo for player X
			const string X_WIN_COMBO = "XXX";
			// Const string for winning combo for player O
			const string O_WIN_COMBO = "OOO";

			// Check for any row wins
			var r1 = String.Join(gameBoard[0], gameBoard[1], gameBoard[2]);
			if (r1 == X_WIN_COMBO || r1 == O_WIN_COMBO)
				return new GameEvaluationResult(r1.Substring(0, 1), new List<int>() { 0, 1, 2 }, gameBoard);
			var r2 = String.Join(gameBoard[3], gameBoard[4], gameBoard[5]);
			if (r2 == X_WIN_COMBO || r2 == O_WIN_COMBO)
				return new GameEvaluationResult(r2.Substring(0, 1), new List<int>() { 3, 4, 5 }, gameBoard);
			var r3 = String.Join(gameBoard[6], gameBoard[7], gameBoard[8]);
			if (r3 == X_WIN_COMBO || r3 == O_WIN_COMBO)
				return new GameEvaluationResult(r3.Substring(0, 1), new List<int>() { 6, 7, 8 }, gameBoard);

			// Check for any column wins
			var c1 = String.Join(gameBoard[0], gameBoard[3], gameBoard[6]);
			if (c1 == X_WIN_COMBO || c1 == O_WIN_COMBO)
				return new GameEvaluationResult(c1.Substring(0, 1), new List<int>() { 0, 3, 6 }, gameBoard);
			var c2 = String.Join(gameBoard[1], gameBoard[4], gameBoard[7]);
			if (c2 == X_WIN_COMBO || c2 == O_WIN_COMBO)
				return new GameEvaluationResult(c2.Substring(0, 1), new List<int>() { 1, 4, 7 }, gameBoard);
			var c3 = String.Join(gameBoard[2], gameBoard[5], gameBoard[8]);
			if (c3 == X_WIN_COMBO || c3 == O_WIN_COMBO)
				return new GameEvaluationResult(c3.Substring(0, 1), new List<int>() { 2, 5, 8 }, gameBoard);

			// Check for any diagnol wins
			var d1 = String.Join(gameBoard[0], gameBoard[4], gameBoard[8]);
			if (d1 == X_WIN_COMBO || d1 == O_WIN_COMBO)
				return new GameEvaluationResult(d1.Substring(0, 1), new List<int>() { 0, 4, 8 }, gameBoard);
			var d2 = String.Join(gameBoard[2], gameBoard[4], gameBoard[6]);
			if (d2 == X_WIN_COMBO || d2 == O_WIN_COMBO)
				return new GameEvaluationResult(d2.Substring(0, 1), new List<int>() { 2, 4, 6 }, gameBoard);

			// Check to see if the game is still going (there's an open space and no winner)
			if (gameBoard.Contains("?") == true)
				return new GameEvaluationResult(GameWinnerStates.Inconclusive, null, gameBoard);

			// Game is tied as no winner was determined and no empty spaces available
			return new GameEvaluationResult(GameWinnerStates.Tie, null, gameBoard);
		}

		/// <summary>
		/// Executes the next move on the board and sets the AzurePlayerSymbol appropriately
		/// </summary>
		public GameBoardResponse ExecuteMoveAsAzurePlayer(GameBoardResponse gameBoard)
		{
			// Count the current moves for X and O Players
			var xCount = gameBoard.GameBoard.Count(x => x == "X");
			var oCount = gameBoard.GameBoard.Count(o => o == "O");
			if (oCount == 0 && xCount == 0)
			{
				// If both counts are 0, no moves have been made and the Azure Player is X
				gameBoard.AzurePlayerSymbol = "X";
			}
			else
			{
				// A Play has happened, default the Azure Player to the lesser count between X and O moves
				gameBoard.AzurePlayerSymbol = xCount > oCount ? "O" : "X";
			}
			// Temporary
			var moveIndex = GuessBestMoveIndex(gameBoard.AzurePlayerSymbol, gameBoard.GameBoard);
			gameBoard.GameBoard[moveIndex] = gameBoard.AzurePlayerSymbol;
			gameBoard.Move = moveIndex;
			var evaluationResult = EvaluateGame(gameBoard.GameBoard);
			gameBoard.Winner = evaluationResult.Winner;
			gameBoard.WinPositions = evaluationResult.WinPositions;
			gameBoard.GameBoard = evaluationResult.GameBoard;
			return gameBoard;
		}

		/// <summary>
		/// Executes the next move on the board on behalf of the provided player symbol
		/// </summary>
		public GameBoardResponseEC ExecuteMoveAsGivenPlayer(GameBoardResponseEC gameBoard)
		{
			var moveIndex = GuessBestMoveIndex(gameBoard.PlayerSymbol, gameBoard.GameBoard);
			gameBoard.GameBoard[moveIndex] = gameBoard.PlayerSymbol;
			gameBoard.Move = moveIndex;
			var evaluationResult = EvaluateGame(gameBoard.GameBoard);
			gameBoard.Winner = evaluationResult.Winner;
			gameBoard.WinPositions = evaluationResult.WinPositions;
			gameBoard.GameBoard = evaluationResult.GameBoard;
			return gameBoard;
		}

		/// <summary>
		/// Looks at the Game Board for any close-win patterns. A close win can be determined by checking to see any two player moves are
		/// close to each other. Example XX?, X?X, ?XX, OO?, O?O, ?OO. If there are no close wins, just take the first open space.
		/// </summary>
		/// <param name="playerSymbol"></param>
		/// <param name="gameBoard"></param>
		/// <returns></returns>
		private int GuessBestMoveIndex(string playerSymbol, List<string> gameBoard)
		{
			// Determine who the other player is (should be the opposite of the playerSymbol passed in
			var otherPlayerSymbol = playerSymbol == "X" ? "O" : "X";
			// Create a list of close win patterns to warch for
			var patterns = new List<string>()
			{
				$"{otherPlayerSymbol}{otherPlayerSymbol}?", // XX? | OO?
				$"{otherPlayerSymbol}?{otherPlayerSymbol}", // X?X | O?O
				$"?{otherPlayerSymbol}{otherPlayerSymbol}" // ?XX | ?OO
			};
			// Build out a board matrix of all possible wins
			var possibleWinPositions = new List<int[]>()
			{
				new int[] { 0, 1, 2 }, // Row 1
				new int[] { 3, 4, 5 }, // Row 2
				new int[] { 6, 7, 8 }, // Row 3
				new int[] { 0, 3, 6 }, // Column 1
				new int[] { 1, 4, 7 }, // Column 2
				new int[] { 2, 5, 8 }, // Column 3
				new int[] { 0, 4, 8 }, // Diagonal 1
				new int[] { 2, 4, 6 } // Diagonal 2
			};
			// Iterating over all of the possible win combinations, evaulate the positions to see if it matches one of the close win patterns
			var closeWinIndex = possibleWinPositions.FindIndex( p => patterns.Contains( String.Join( gameBoard[p[0]], gameBoard[p[1]], gameBoard[p[2]] ) ) );
			// If closeWinIndes is greater than -1, it means a close win was found.
			if (closeWinIndex > -1)
			{
				// Take the closeWinIndex and get the specific board positions from the possibleWinPositions matrix
				var closeWinPositionCombination = possibleWinPositions[closeWinIndex];
				// Identify the current close win combination to see where the best spot to place the move is
				var combination = String.Join(gameBoard[closeWinPositionCombination[0]], gameBoard[closeWinPositionCombination[1]], gameBoard[closeWinPositionCombination[2]]);
				// Find the index of the open space
				var emptySpaceIndex = combination.IndexOf("?");
				// Return the specific spot on the game board to put the move (thus blocking the close win)
				var positionToPlay = closeWinPositionCombination[emptySpaceIndex];
				// Fallback check to make sure the move about to be made is available
				if (gameBoard[positionToPlay] == "?")
					return positionToPlay; 
			}
			// It's currently not a close game, just return the first open spot on the board.
			return gameBoard.FindIndex(x => x == "?");
		}
	}
}
