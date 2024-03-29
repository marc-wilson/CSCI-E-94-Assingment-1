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
    /// Model to define incoming payload of the GameBoard
    /// </summary>
    public partial class GameBoardRequest
    {
        /// <summary>
        /// Initializes a new instance of the GameBoardRequest class.
        /// </summary>
        public GameBoardRequest() { }

        /// <summary>
        /// Initializes a new instance of the GameBoardRequest class.
        /// </summary>
        public GameBoardRequest(IList<string> gameBoard = default(IList<string>))
        {
            GameBoard = gameBoard;
        }

        /// <summary>
        /// Game Board containing open positions and moves
        /// </summary>
        [JsonProperty(PropertyName = "gameBoard")]
        public IList<string> GameBoard { get; set; }

    }
}
