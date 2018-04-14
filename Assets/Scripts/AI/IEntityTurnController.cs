using System.Collections.Generic;

namespace AI {
    /// <summary>
    /// Common interface for AI/Player controls.
    /// </summary>
    public interface IEntityTurnController {
        /// <summary>
        /// Beginning a new turn for a side.
        /// </summary>
        void BeginTurn();
        /// <summary>
        /// Checks if the AI has finished processing the turn.
        /// </summary>
        /// <returns>True if the turn has been processed, else false.</returns>
        bool IsTurnOver();
        
        /// <summary>
        /// Checks if the AI has moves ready.
        /// </summary>
        /// <returns>True if moves are ready, else false.</returns>
        bool IsMoveAvailable();
        /// <summary>
        /// Gets the list of ready moves.
        /// </summary>
        List<Move> GetMoves();
        /// <summary>
        /// Tells the AI the RoundController is done with the moves list.
        /// </summary>
        void DoneWithMoves();
    }
}
