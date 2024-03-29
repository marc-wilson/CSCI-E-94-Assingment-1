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
    /// GameBoardRequest for the Extra Credit. Same as GameBoardRequest, but
    /// allows for a playerSymbol in the incomming payload
    /// </summary>
    public partial class GameBoardRequestEC
    {
        /// <summary>
        /// Initializes a new instance of the GameBoardRequestEC class.
        /// </summary>
        public GameBoardRequestEC() { }

        /// <summary>
        /// Initializes a new instance of the GameBoardRequestEC class.
        /// </summary>
        public GameBoardRequestEC(string playerSymbol = default(string), IList<string> gameBoard = default(IList<string>))
        {
            PlayerSymbol = playerSymbol;
            GameBoard = gameBoard;
        }

        /// <summary>
        /// Symbol of the player who is making the move (X or O)
        /// </summary>
        [JsonProperty(PropertyName = "playerSymbol")]
        public string PlayerSymbol { get; set; }

        /// <summary>
        /// Game Board containing open positions and moves
        /// </summary>
        [JsonProperty(PropertyName = "gameBoard")]
        public IList<string> GameBoard { get; set; }

    }
}
