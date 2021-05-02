namespace TicTacToeZ
{
    //represents and manipulates the board
    public class GameState
    {
        public bool IsFirst {get; private set;}
        private TileState[,] board {get; set;}
        private TileState aiPieces {get; set;}
        private TileState playerPieces {get; set;}

        public GameState(bool isFirst)
        {
            board = new TileState[3,3];
            aiPieces = isFirst ? TileState.O : TileState.X;
            playerPieces = isFirst ? TileState.X : TileState.O;
            
            IsFirst = isFirst;
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    board[i,j] = TileState.EMPTY;
                }
            }
        }

        public string WriteBoard()
        {
            string outputString = "";
            for(int i = 0; i < board.GetLength(0); i++)
            {
                outputString += $"{TileStateExtensions.ToReadableString(board[i,0])} | {TileStateExtensions.ToReadableString(board[i,1])} | {TileStateExtensions.ToReadableString(board[i,2])} \n";
            }

            return outputString;
        }

        public bool CheckForPlayerWin()
        {
            int playerValue = 0;

            //check rows
            for(int i = 0; i < board.GetLength(0); i++)
            {
                playerValue = 0;

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] == playerPieces) playerValue++;
                    if(playerValue == 3) return true;
                }
            }

            //check cols
            for(int i = 0; i < board.GetLength(1); i++)
            {
                playerValue = 0;

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[j,i] == playerPieces) playerValue++;
                    if(playerValue == 3) return true;
                }
            }

            //check diags
            playerValue = 0;
            if(board[0,0] == playerPieces) playerValue++;
            if(board[1,1] == playerPieces) playerValue++;
            if(board[2,2] == playerPieces) playerValue++;
            if(playerValue == 3) return true;

            //check other diagonal
            playerValue = 0;
            if(board[2,0] == playerPieces) playerValue++;
            if(board[1,1] == playerPieces) playerValue++;
            if(board[0,2] == playerPieces) playerValue++;
            if(playerValue == 3) return true;

            return false;
        }

        public bool CheckForAIWin()
        {
            int aiValue = 0;

            //check rows
            for(int i = 0; i < board.GetLength(0); i++)
            {
                aiValue = 0;

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] == aiPieces) aiValue++;
                    if(aiValue == 3) return true;
                }
            }

            //check cols
            for(int i = 0; i < board.GetLength(1); i++)
            {
                aiValue = 0;

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[j,i] == aiPieces) aiValue++;
                    
                }
            }

            //check diags
            aiValue = 0;
            if(board[0,0] == aiPieces) aiValue++;
            if(board[1,1] == aiPieces) aiValue++;
            if(board[2,2] == aiPieces) aiValue++;
            if(aiValue == 3) return true;

            //check other diagonal
            aiValue = 0;
            if(board[2,0] == aiPieces) aiValue++;
            if(board[1,1] == aiPieces) aiValue++;
            if(board[0,2] == aiPieces) aiValue++;
            if(aiValue == 3) return true;
            
            return false;
        }

        public bool CheckForTie()
        {
            int occupiedSpaces = 0;

            //check rows
            for(int i = 0; i < board.GetLength(0); i++)
            {

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] != TileState.EMPTY) occupiedSpaces++;
                    if(occupiedSpaces >= 9) return true;
                }
            }

            return false;
        }

        public bool TakeTurn(int position)
        {
            //handles improper input
            if(position < 1 || position >9 ) return false;

            //map number pad to game board
            int col = ((position - 1) % 3);//to make this scalable we could use board.GetLength(). if we had an infinitely sized number pad, we continue with this style of input, for funsies (not performant though)
            int row = (2)-((position - 1) / 3);  //not necessarily intuitive, but we have to flip the numberpad to make it map properly to the array. as well subtract 1, since we start with 1 on the numberpad.



            if(board[row, col] == TileState.EMPTY) board[row, col] = IsFirst ? TileState.X : TileState.O;
            else return false;

            return true;
        }

        //a little bit of a dirty solution, i'd love to go back do some deeper research on tictactoe AI using trees
        public bool AITurn()
        {
            int aiValue = 0; //gets +1 for every one of this AI's placements in a given row/col/diag
            int playerValue = 0;

            //check diagonal
            aiValue = 0;
            playerValue = 0;
            if(board[0,0] == aiPieces) aiValue++;
            else if(board[0,0] == playerPieces) playerValue++;
            if(board[1,1] == aiPieces) aiValue++;
            else if(board[1,1] == playerPieces) playerValue++;
            if(board[2,2] == aiPieces) aiValue++;
            else if(board[2,2] == playerPieces) playerValue++;
            if(aiValue >= 2 || playerValue >= 2)
            {
                if(board [0, 0] == TileState.EMPTY) 
                {
                    board [0, 0] = aiPieces;
                    return true;
                }

                else if(board [1, 1] == TileState.EMPTY) 
                {
                    board [1, 1] = aiPieces;
                    return true;
                }

                else if(board [2, 2] == TileState.EMPTY) 
                {
                    board [2, 2] = aiPieces;
                    return true;
                }
            }

            //check other diagonal
            aiValue = 0;
            playerValue = 0;
            if(board[2,0] == aiPieces) aiValue++;
            else if(board[2,0] == playerPieces) playerValue++;
            if(board[1,1] == aiPieces) aiValue++;
            else if(board[1,1] == playerPieces) playerValue++;
            if(board[0,2] == aiPieces) aiValue++;
            else if(board[0,2] == playerPieces) playerValue++;
            if(aiValue >= 2 || playerValue >= 2)
            {
                if(board [2, 0] == TileState.EMPTY) 
                {
                    board [2, 0] = aiPieces;
                    return true;
                }
                else if(board [1, 1] == TileState.EMPTY)
                {
                     board [1, 1] = aiPieces;
                     return true;
                }
                else if(board [0, 2] == TileState.EMPTY) 
                {
                    board [0, 2] = aiPieces;
                    return true;
                }
            }

            //check rows for placement to win
            for(int i = 0; i < board.GetLength(0); i++)
            {
                aiValue = 0;
                playerValue = 0;

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] == aiPieces) aiValue++;
                    else if(board[i,j] == playerPieces) playerValue++;

                    //if we have 2 pieces in a given row, take/block the win
                    if(aiValue >= 2 || playerValue >= 2)
                    {
                        if(board [i, 0] == TileState.EMPTY)
                        {
                            board [i, 0] = aiPieces;
                            return true;
                        }

                        else if(board [i, 1] == TileState.EMPTY)
                        {
                            board [i, 1] = aiPieces;
                            return true;
                        } 
                        else if(board [i, 2] == TileState.EMPTY)
                        {
                            board [i, 2] = aiPieces;
                            return true;
                        }
                    }
                }
            }

            //check cols for placement to win
            for(int i = 0; i < board.GetLength(1); i++)
            {
                aiValue = 0;
                playerValue = 0;

                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[j,i] == aiPieces) aiValue++;
                    else if(board[j,i] == playerPieces) playerValue++;

                    //if we have 2 pieces in a given col, take/block the win
                    if(aiValue >= 2 || playerValue >= 2)
                    {
                        if(board [0, i] == TileState.EMPTY)
                        { 
                            board [0, i] = aiPieces; 
                            return true;
                        }
                        else if(board [1, i] == TileState.EMPTY) 
                        {
                            board [1, i] = aiPieces;
                            return true;
                        }
                        else if(board [2, i] == TileState.EMPTY)
                        {
                            board [2, i] = aiPieces;
                            return true;
                        } 
                    }
                }
            }
            
            //since we didn't have a win, lets take the strongest moves on the board
            //otherwise, take the best move in the game
            if(board[1,1] == TileState.EMPTY)
            {
                board[1,1] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }

            //otherwise take the corners, which are stronger than the other positions
            if(board[0,0] == TileState.EMPTY)
            {
                board[0,0] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }
            if(board[0,2] == TileState.EMPTY)
            {
                board[0,2] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }
            if(board[2,0] == TileState.EMPTY)
            {
                board[2,0] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }
            if(board[2,2] == TileState.EMPTY)
            {
                board[2,2] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }

            //take the weakest positions
            if(board[0,1] == TileState.EMPTY)
            {
                board[0,1] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }
            if(board[1,0] == TileState.EMPTY)
            {
                board[1,0] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }
            if(board[1,2] == TileState.EMPTY)
            {
                board[1,2] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }
            if(board[2,1] == TileState.EMPTY)
            {
                board[2,1] = aiPieces; //if the player is first, we place O, which goes second
                return true;
            }

            return false;
        } 
    }
}