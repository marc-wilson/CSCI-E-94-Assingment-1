using System.Collections.Generic;

namespace TicTacToe.Models
{
	/// <summary>
	/// GameBoardRequest for the Extra Credit. Same as GameBoardRequest, but allows for a playerSymbol in the incomming payload
	/// </summary>
	public class GameBoardRequestEC : GameBoardRequest
	{
		/// <summary>
		/// Symbol of the player who is making the move (X or O)
		/// </summary>
		public string PlayerSymbol { get; set; }
	}
}
