using System;
using System.Collections.Generic;
using TicTacToe.Enums;

namespace TicTacToe.Models
{
	/// <summary>
	/// Returned to the client as a response to a Game Board Evaluation or after a move has been made by an Azure Player
	/// </summary>
	public class GameBoardResponseEC : GameManager
	{
		/// <summary>
		/// Where at on the board the move was made
		/// </summary>
		public int? Move { get; set; } = null;
		/// <summary>
		/// The symbol of the Player
		/// </summary>
		public string PlayerSymbol { get; set; }
		/// <summary>
		/// Player symbol of the winner
		/// </summary>
		public string Winner { get; set; }
		/// <summary>
		/// The specific positions of the winning combonation
		/// </summary>
		public List<int> WinPositions { get; set; }
		/// <summary>
		/// The game board
		/// </summary>
		public List<string> GameBoard { get; set; }

		/// <summary>
		/// Initializaer for GameBoardRequestEC
		/// </summary>
		/// <param name="request"></param>
		public GameBoardResponseEC(GameBoardRequestEC request)
		{
			GameBoard = request.GameBoard;
		}

		/// <summary>
		/// Executes the next move on the board and sets the PlayerSymbol appropriately
		/// </summary>
		public GameBoardResponseEC ExecuteMove()
		{
			// Temporary
			var firstSpotOpenIndex = GameBoard.FindIndex(x => x == "?");
			GameBoard[firstSpotOpenIndex] = PlayerSymbol;
			Move = firstSpotOpenIndex;
			var evaluationResult = EvaluateGame(GameBoard);
			Winner = evaluationResult.Winner;
			WinPositions = evaluationResult.WinPositions;
			GameBoard = evaluationResult.GameBoard;
			return this;
		}
	}
}
