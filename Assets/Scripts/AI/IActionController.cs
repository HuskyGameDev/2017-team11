using System.Collections.Generic;

namespace AI {
    /// <summary>
    /// Common interface for AI/Player controls.
    /// </summary>
    public interface IActionController {
        void BeginTurn();
        bool IsTurnOver();
        List<Move> GetMoves();
        void DoneWithMoveList();
    }
}
