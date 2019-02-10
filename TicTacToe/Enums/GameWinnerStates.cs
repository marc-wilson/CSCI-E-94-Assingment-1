namespace TicTacToe.Enums
{
	/// <summary>
	/// Game Winner States - X, O, tie or inconclusive. Enums can't hold strings (or I'm doing it wrong, so making const strings instead!)
	/// </summary>
	public class GameWinnerStates
	{
		/// <summary>
		/// Game Winner is X
		/// </summary>
		public const string X = "X";
		/// <summary>
		/// Game Winner is O
		/// </summary>
		public const string O = "O";
		/// <summary>
		/// Game Winner is inconclusive (still an open move to be made)
		/// </summary>
		public const string Inconclusive = "inconclusive";
		/// <summary>
		/// Game is completed and Tied
		/// </summary>
		public const string Tie = "tie";
	}
}
