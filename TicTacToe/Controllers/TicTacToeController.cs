using System.Net;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Enums;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
	/// <summary>
	/// Entry point for TicTacToe API
	/// </summary>
	[Route("api/tictactoe")]
    [ApiController]
    public class TicTacToeController : ControllerBase
    {
		private const string BAD_REQ_MSG = "Invalid Game Board. The Game Board must consist of 9 moves. Valid characters are 'X', 'O', and '?'. X and O Count must always be within +/- 1 of eachother. Example Input: { 'gameBoard': ['X', '?', '?', '?', '?', '?', '?', '?', '?'] }";
		/// <summary>
		/// Evaluates a Game Board and executes a move if the game is still in progress.
		/// </summary>
		/// <param name="request">An Object with a property (List of string) named 'GameBoard' that contains the current board</param>
		/// <returns>GameBoardResponse</returns>
		[HttpPost("executemove")]
		[ProducesResponseType(typeof(GameBoardResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(GameBoardResponse), (int)HttpStatusCode.BadRequest)]
		public IActionResult ExecuteMove([FromBody] GameBoardRequest request)
		{
			// Evaluate the Game Board to make sure it's valid for further game play
			if (request.ValidateBoard() == false)
				// Game Board is invalid -- return Bad Request Response with hint text
				return BadRequest(BAD_REQ_MSG);
			// Evaluate the game board to determine game state
			var gameBoard = new GameBoardResponse(request);
			var evaluation = gameBoard.EvaluateGame(gameBoard.GameBoard);
			gameBoard.Winner = evaluation.Winner;
			gameBoard.WinPositions = evaluation.WinPositions;
			gameBoard.GameBoard = evaluation.GameBoard;
			// If game is inconclusive, execute the next move
			if (gameBoard.Winner == GameWinnerStates.Inconclusive)
			{
				gameBoard = gameBoard.ExecuteMoveAsAzurePlayer(gameBoard);
			}
			return Ok(gameBoard);
		}

		/// <summary>
		/// Accepts a game board with a PlayerSymbol and executes a move under that PlayerSymbol and evaluates it.
		/// </summary>
		/// <param name="request"></param>
		/// <returns>GameBoardResponseEC</returns>
		[HttpPost("calculatemove")]
		[ProducesResponseType(typeof(GameBoardResponseEC), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(GameBoardResponseEC), (int)HttpStatusCode.BadRequest)]
		public IActionResult CalculateMove([FromBody] GameBoardRequestEC request)
		{
			// Evaluate the Game Board to make sure it's valid for further game play
			if (request.ValidateBoard() == false)
				// Game Board is invalid -- return Bad Request Response with hint text
				return BadRequest(BAD_REQ_MSG);

			// Evaluate the game board to determine game state
			var gameBoard = new GameBoardResponseEC(request);
			var evaluation = gameBoard.EvaluateGame(gameBoard.GameBoard);
			gameBoard.PlayerSymbol = request.PlayerSymbol;
			gameBoard.Winner = evaluation.Winner;
			gameBoard.WinPositions = evaluation.WinPositions;
			gameBoard.GameBoard = evaluation.GameBoard;
			// If game is inconclusive, execute the next move
			if (gameBoard.Winner == GameWinnerStates.Inconclusive)
			{
				gameBoard = gameBoard.ExecuteMove();
			}
			return Ok(gameBoard);
		}

		/// <summary>
		/// Disallow HttpDelete Verb
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		public BadRequestResult DeleteNotAllowed()
		{
			return new BadRequestResult();
		}
		/// <summary>
		/// Disallow HttpGet Verb
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public BadRequestResult GetNotAllowed()
		{
			return new BadRequestResult();
		}
		/// <summary>
		/// Disallow HttpPut Verb
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		public BadRequestResult PutNotAllowed()
		{
			return new BadRequestResult();
		}
		/// <summary>
		/// Disallow HttpPatch Verb
		/// </summary>
		/// <returns></returns>
		[HttpPatch]
		public BadRequestResult PatchNotAllowed()
		{
			return new BadRequestResult();
		}
		/// <summary>
		/// Disallow HttpHead Verb
		/// </summary>
		/// <returns></returns>
		[HttpHead]
		public BadRequestResult HeadNotAllowed()
		{
			return new BadRequestResult();
		}

	}
}