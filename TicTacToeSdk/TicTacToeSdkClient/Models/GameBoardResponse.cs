﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace TicTacToeSdk.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    /// <summary>
    /// Returned to the client as a response to a Game Board Evaluation or
    /// after a move has been made by an Azure Player
    /// </summary>
    public partial class GameBoardResponse
    {
        /// <summary>
        /// Initializes a new instance of the GameBoardResponse class.
        /// </summary>
        public GameBoardResponse() { }

        /// <summary>
        /// Initializes a new instance of the GameBoardResponse class.
        /// </summary>
        public GameBoardResponse(int? move = default(int?), string azurePlayerSymbol = default(string), string winner = default(string), IList<int?> winPositions = default(IList<int?>), IList<string> gameBoard = default(IList<string>))
        {
            Move = move;
            AzurePlayerSymbol = azurePlayerSymbol;
            Winner = winner;
            WinPositions = winPositions;
            GameBoard = gameBoard;
        }

        /// <summary>
        /// Where at on the board the move was made
        /// </summary>
        [JsonProperty(PropertyName = "move")]
        public int? Move { get; set; }

        /// <summary>
        /// The symbol of the Azure Player
        /// </summary>
        [JsonProperty(PropertyName = "azurePlayerSymbol")]
        public string AzurePlayerSymbol { get; set; }

        /// <summary>
        /// Player symbol of the winner
        /// </summary>
        [JsonProperty(PropertyName = "winner")]
        public string Winner { get; set; }

        /// <summary>
        /// The specific positions of the winning combonation
        /// </summary>
        [JsonProperty(PropertyName = "winPositions")]
        public IList<int?> WinPositions { get; set; }

        /// <summary>
        /// The game board
        /// </summary>
        [JsonProperty(PropertyName = "gameBoard")]
        public IList<string> GameBoard { get; set; }

    }
}
