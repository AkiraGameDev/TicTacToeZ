namespace TicTacToeZ
{
    //simply tells us the state of each tile on the board
    public enum TileState
    {
        EMPTY,
        X,
        O
    }

    public static class TileStateExtensions
    {
        public static string ToReadableString(TileState state)
        {
            switch(state)
            {
                case TileState.EMPTY:
                    return " ";
                case TileState.X:
                    return "X";
                case TileState.O:
                    return "O";
            }
            return "";
        }
    }
}