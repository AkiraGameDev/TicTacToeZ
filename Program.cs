using System;

namespace TicTacToeZ
{
    
    class Program
    {
        static MenuState menuState = MenuState.MAINMENU;
        static Commands command = Commands.INVALID;
        static string outputString = "";
        static GameState board;
        static bool didPlayerWin;
        static bool isGameTied = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Christian Orellana's Tic Tac Toe!");

            while(command != Commands.QUIT)
            {
                switch(menuState)
                {
                    case MenuState.MAINMENU:
                        outputString += "You can type \"Start\" to start the game, or \"Quit\" at any time to quit. Although, I'd prefer you didn't quit -- it's been a while since I've played against anyone.\n";
                        outputString += "For ease of use, the board maps 1 to 1 with a number pad. When you take your turn, the following numbers reference the following positions.\n";
                        outputString += " 7 | 8 | 9 \n";
                        outputString += " 4 | 5 | 6 \n";
                        outputString += " 1 | 2 | 3 ";
                        Console.WriteLine(outputString);
                        command = ToCommand(Console.ReadLine().Trim().ToUpper());
                        HandleMainMenuInput();
                        break;
                    
                    case MenuState.SETUP:
                        outputString = "Alright, would you like to go FIRST or SECOND? Either way, I'll probably win. If you start losing, you can RESTART at any time.";
                        Console.WriteLine(outputString);
                        command = ToCommand(Console.ReadLine().Trim().ToUpper());
                        HandleSetupInput();
                        break;
                    
                    case MenuState.GAME:
                        outputString = "Here's the board:\n";
                        outputString += board.WriteBoard();
                        outputString += $"Where would you like to go? (You're {(board.IsFirst ? "X" : "O")})";
                        Console.WriteLine(outputString);

                        
                        HandleGameInput();

                        outputString = board.WriteBoard();
                        Console.WriteLine(outputString);
                        break;

                    case MenuState.POSTGAME:
                        if(didPlayerWin) outputString = "Wow, looks like you won! Nice work.";
                        else if(isGameTied) outputString = "It seems we can't manage to best each other.";
                        else outputString = "I've won. Again!";

                        outputString += "\nWell, that was fun. Use RESTART if you want to play again!";
                        Console.WriteLine(outputString);
                        command = ToCommand(Console.ReadLine().Trim().ToUpper());
                        HandlePostGameInput();
                        break;
                }          
            }

            outputString = "Alright, I'll see you later! Lets play again sometime ;)";
            Console.WriteLine(outputString);
        }

        private static Commands ToCommand(string inputString)
        {
            return Enum.TryParse(inputString, ignoreCase: true, out Commands result) ? result : Commands.INVALID;
        }

        private static void HandleMainMenuInput()
        {
            switch(command)
            {
                case Commands.START:
                    menuState = MenuState.SETUP;
                    break;
            }
        }

        private static void HandleSetupInput()
        {
            
            switch(command)
            {
                case Commands.FIRST:
                    menuState = MenuState.GAME;
                    outputString = "You can be \"X\". Your turn.";
                    Console.WriteLine(outputString);
                    board = new GameState(true); //boolean refers to if player goes first or not
                    break; 

                case Commands.SECOND:
                    menuState = MenuState.GAME;
                    outputString = "Feeling confident? I like that. You're \"O\". My turn.";
                    Console.WriteLine(outputString);
                    board = new GameState(false);
                    break; 
            }
        }

        private static void HandleGameInput()
        {
            int input;

            //handle any possible need to quit or restart
            string inputString = Console.ReadLine().Trim().ToUpper();
            if(inputString == "QUIT")
            {
                command = Commands.QUIT;
                return;
            }
            else if(inputString == "RESTART")
            {
                command = Commands.RESTART;
                menuState = MenuState.SETUP;
                return;
            }

            //if we didn't want to quit, we get our input
            Int32.TryParse(inputString, out input);
            bool validInput = board.TakeTurn(input);

            //if our input was valid, we place and let the AI take it's turn.
            if(validInput) outputString = board.WriteBoard();
            else outputString = "Invalid input, try again.";

            if(board.CheckForPlayerWin())
            {
                didPlayerWin = true;
                menuState = MenuState.POSTGAME;
            }

            //if AI failed to place a piece, we've tied
            if(board.AITurn() == false)
            {
                isGameTied = true;
                menuState = MenuState.POSTGAME;
            }

            //ai won
            if(board.CheckForAIWin())
            {
                didPlayerWin = false;
                menuState = MenuState.POSTGAME;
            }

            if(board.CheckForTie())
            {
                isGameTied = true;
                menuState = MenuState.POSTGAME;
            }
            
            Console.WriteLine(outputString);
        }

        private static void HandlePostGameInput()
        {
            switch(command)
            {
                case Commands.RESTART:
                    menuState = MenuState.SETUP;
                    didPlayerWin = false;
                    isGameTied = false;
                    break;
            }
        }
    }
}
