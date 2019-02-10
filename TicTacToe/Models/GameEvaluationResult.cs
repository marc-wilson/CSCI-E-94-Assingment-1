using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
	/// <summary>
	/// Result of a game board evaluation
	/// </summary>
	public class GameEvaluationResult
	{
		/// <summary>
		/// Winner X/O/inconclusive/tie
		/// </summary>
		public string Winner { get; set; }
		/// <summary>
		/// Win Positions of the winning combination
		/// </summary>
		public List<int> WinPositions { get; set; }
		/// <summary>
		/// Resulting Game Board
		/// </summary>
		public List<string> GameBoard { get; set; }

		/// <summary>
		/// Game Evaluation Initializer
		/// </summary>
		/// <param name="winner"></param>
		/// <param name="winPositions"></param>
		/// <param name="gameBoard"></param>
		public GameEvaluationResult(string winner, List<int> winPositions, List<string> gameBoard)
		{
			Winner = winner;
			WinPositions = winPositions;
			GameBoard = gameBoard;
		}
	}
}
