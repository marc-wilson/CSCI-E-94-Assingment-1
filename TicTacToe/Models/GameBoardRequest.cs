using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{
	/// <summary>
	/// Model to define incoming payload of the GameBoard
	/// </summary>
	public class GameBoardRequest
	{
		/// <summary>
		/// Game Board containing open positions and moves
		/// </summary>
		public List<string> GameBoard { get; set; }
		/// <summary>
		/// Validates the state of the game board
		/// </summary>
		/// <returns>boolean value determining if the game board is valid or not</returns>
		public bool ValidateBoard()
		{
			// If the game board is null or not exactly 9 spaces, it's invalid.
			if (GameBoard == null || GameBoard.Count != 9)
				return false;
			// Define list of valid game board chars
			var validChars = new List<string>() { "X", "O", "?" };
			// Make sure all spaces adhere to the valid chars
			var hasValidChars = GameBoard.Find(m => validChars.Contains(m) == false);
			// Found an invalid char, game board is invalid
			if (hasValidChars != null)
				return false;

			// Get count of X's
			var xCount = GameBoard.Count(x => x == "X");
			// Get count of O's
			var oCount = GameBoard.Count(o => o == "O");
			// Get Difference between oCount and xCount
			var diff = xCount - oCount;
			// Check to make sure the X/O count is within +/- 1 of each other
			if (diff > 1 || diff < -1)
				// X/O plays are invalid.
				return false;

			// Game Board is valid
			return true;
		}
	}
}
