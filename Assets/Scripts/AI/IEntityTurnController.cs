using System.Collections.Generic;

namespace AI {
    /// <summary>
    /// Common interface for AI/Player controls.
    /// </summary>
    public interface IEntityTurnController {
        void BeginTurn();
        bool IsTurnOver();
            
        bool IsMoveAvailable();
        List<Move> GetMoves();
        void DoneWithMoves();
    }
}
