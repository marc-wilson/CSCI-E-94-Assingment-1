using System.Collections.Generic;

namespace TicTacToe.Models
{
	/// <summary>
	/// Returned to the client as a response to a Game Board Evaluation or after a move has been made by an Azure Player
	/// </summary>
	public class GameBoardResponse : GameManager
	{
		/// <summary>
		/// Where at on the board the move was made
		/// </summary>
		public int? Move { get; set; } = null;
		/// <summary>
		/// The symbol of the Azure Player
		/// </summary>
		public string AzurePlayerSymbol { get; set; }
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
		/// Initializaer for GameBoardResponse
		/// </summary>
		/// <param name="gameBoard"></param>
		public GameBoardResponse(GameBoardRequest gameBoard)
		{
			GameBoard = gameBoard.GameBoard;
		}
	}
}
