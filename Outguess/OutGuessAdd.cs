using System.Numerics;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OutGuessAddendum {
    internal class Program {
        static void Main(string[] args) {
            Header("\t\tWelcome to Outguess!\n\tI've chosen a number between 1 and 100!\n\tYou have up to 10 attempts to guess my hidden number!");
            Random random = new Random();
            double totalRounds = 0;
            int guessIndex = 0;
            int totalWins = 0;
            int guess = 0;
            double amountWagered = 0.0;
            bool playAgain = false;
            int attempts;
            int cpuNumber = random.Next(1, 5);
            double totalCash = 0.0;
            totalCash = PromptTotalCash("Enter the amount of money you plan on bringing to the table: $");
            do {
                cpuNumber = random.Next(1, 5);
                attempts = 10;
                double remainingBalance = totalCash - amountWagered;
                do {
                    amountWagered = PromptTryDouble("Enter the amount you will wager this round: $");
                    if (amountWagered < 0.0 || amountWagered > totalCash) {
                        Console.WriteLine($"Invalid input! Please enter a valid wager (up to {totalCash:C}): $ ");
                    } else {
                    }//end loop
                } while (amountWagered <= 0.0 || amountWagered > totalCash);//end do while
                    Console.WriteLine("\nHow many guesses, (max. 10), do you think you need to guess the hidden number? ");
                do {
                    attempts = PromptIntTryParse("\nEnter the number of guesses you will attempt: ");
                    if (!(attempts <= 10 && attempts > 0)) {//then
                        attempts = PromptIntTryParse("\nEnter a valid attempt between 1 and 10!:");
                        Console.WriteLine("\tEnter your guess between 1 and 100!");
                    }//end if

                } while (!(attempts <= 10 && attempts >= 1));//end do while
                    totalCash -= amountWagered;
                for (int a = 1; a <= attempts; a++) {
                    guess = PromptTryParse($"\nYou have {attempts - a + 1} attempts left: ");
                    int remainingAttempts = attempts - a;
                    guessIndex++;
                    while (!(guess >= 1 && guess <= 100)) {
                        guess = PromptTryParse("\nError! Please enter a valid integer!: ");
                    }//end while
                    if (guess == cpuNumber) {
                        Console.WriteLine("\nCongratulations! You Win!\n");
                        totalWins++;
                        a = attempts = 1;
                        totalCash += amountWagered + WagerMultiplier(guessIndex, amountWagered);
                        playAgain = PlayAgain();
                        Console.WriteLine($"Your new balance is: {totalCash:c}");
                    } else if ( remainingAttempts == 0) {
                        Console.WriteLine($"Sorry! you used up all your guesses!\nThe hidden number was {cpuNumber}.");
                        Console.WriteLine($"Your remaining balance is {remainingBalance:C}");
                        playAgain = PlayAgain();
                    } else if (guess > cpuNumber) {
                        Console.WriteLine($"\nToo high.");
                    } else {
                        Console.WriteLine($"\nToo low.");
                    }//end else if
                }//end for loop

                totalRounds++;
                Console.WriteLine($"Total Rounds: {totalRounds} / Total Wins: {totalWins}");
            Console.WriteLine($" Cash amount: {totalCash:C}");
            } while (playAgain); //end while
                double winPercent = (totalWins / totalRounds) * 100;
                Console.WriteLine($"Your win percentage is {Math.Round(winPercent,2)}%.");



        }//end main
        static void OutguessAdd() {

        }//end function

        static void Outguess() {
            Random random = new Random();
            int cpuNumber = random.Next(1, 8);
            int guess = 0;
            string playing;
            Console.WriteLine("\nHow many guesses, (max. 10), do you think you need to guess the hidden number? ");
            int attempts = PromptIntTryParse("\nEnter the number of guesses you will attempt: ");
            Console.WriteLine("\tEnter your guess between 1 and 100!");
            int guessLeft = attempts;
            for (int a = 1; a <= attempts; a++) {
                guess = PromptTryParse($"\nYou have {guessLeft} attempts left: ");
                while (!(guess >= 1 && guess <= 100)) {
                    guess = PromptTryParse("\nError! Please enter a valid integer!: ");
                }//end while
                    if (guess == cpuNumber) {
                        Console.WriteLine("\nCongratulations! You Win!\n");
                        PlayAgain();
                        return;
                    } else if (guess > cpuNumber) {
                        Console.WriteLine($"\nToo high.");
                    } else
                        Console.WriteLine($"\nToo low.");
                if (guessLeft == 0) {
                    Console.WriteLine($"\nSorry! You used up all your attempts.\nThe answer was {cpuNumber}.\n");
                    PlayAgain();
                    return;
                }//end if   
                        guessLeft--;
            }//end for loop

        }//end function
        static double WagerMultiplier(int guessIndex, double amountWagered) {
            switch (guessIndex) {
                case 1:
                    return amountWagered * 10;
                case 2:
                    return amountWagered * 9;
                case 3:
                    return amountWagered * 8;
                case 4:
                    return amountWagered * 7;
                case 5:
                    return amountWagered * 6;
                case 6:
                    return amountWagered * 5;
                case 7:
                    return amountWagered * 4;
                case 8:
                    return amountWagered * 3;
                case 9:
                    return amountWagered * 2;
                case 10: 
                    return amountWagered * 1;
                default:
                    return 0; //game over

            }//end switch
        }//end function

        static bool PlayAgain() {
            string playing;
            bool tryAgain = false;

            do {
                playing = Prompt("Do you want to play again? (---Yes---/---No---): ");
                if (playing.ToLower() == "no") {
                    Console.WriteLine("\n\t\tGame Over!");
                    tryAgain = false;
                } else if (playing.ToLower() == "yes") {
                    Console.WriteLine("\n\t\tLets play again!\n");
                    tryAgain = true;
                    //Console.Clear();
                } else {
                    Console.WriteLine("\nInvalid Response! Please type 'Yes' or 'No' \n");
                }//end else if
            } while (playing.ToLower() != "yes" && playing.ToLower() != "no");
            return tryAgain;
        }//end function

        static double PromptTotalCash(string dataRequest) {
            //Variable
            double userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
            Console.Write(dataRequest);
            do {
                isValid = double.TryParse(Console.ReadLine(), out userInput);
                if (!isValid || userInput < 1) {
                    Console.WriteLine("\nError! Please enter a valid value: ");
                }//end if
            } while (isValid == false);//end do while
            return userInput;
        }//end function

        static int PromptIntTryParse(string dataRequest) {
            //VARIABLE
            int userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
            Console.Write(dataRequest);
            do {
                isValid = int.TryParse(Console.ReadLine(), out userInput);
                if (!isValid) {
                    Console.WriteLine("Error! Please enter a valid number: ");
                }//end if
            } while (isValid == false);//end do while
            return userInput;
        }//end function
        static double PromptTryDouble(string dataRequest) {
            //Variable
            double userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
                Console.Write(dataRequest);
            do {
                isValid = double.TryParse(Console.ReadLine(), out userInput);
                if (!isValid || userInput <= 0) {
                    Console.WriteLine("\nError! please enter a valid positive amount: ");
                }//end if
            } while (isValid == false);//end do while
            return userInput;
        }//end function

        #region PROMPT FUNCTIONS
        static int PromptTryParse(string dataRequest) {
            //VARIABLE
            int userInput = 0;
            bool isValid = false;

            //INPUT VALIDATION LOOP
                Console.WriteLine(dataRequest);
            do {
                isValid = int.TryParse(Console.ReadLine(), out userInput);
                if (!isValid) {
                    Console.WriteLine("Error! Please enter a valid number: ");
                }//end if
            } while (isValid == false);//end do while
            return userInput;
        }//end function

        static void Header(string text) {
            Console.WriteLine("================================================================");
            Console.WriteLine(text);
            Console.WriteLine("=================================================================");
        }//end function
        static string Prompt(string data) {
            //Variable:
            string userInput = "";

            //Request info from user
            Console.Write(data);

            //Recieve Response
            userInput = Console.ReadLine();

            return userInput;
        }//end function
        static int PromptInt(string data) {
            //Variable
            int userInput = 0;

            //Request and Receive info
            userInput = int.Parse(Prompt(data));

            return userInput;
        }//end function
        static double PromptDouble(string data) {
            //Variable
            double userInput = 0.0;

            //Request and Recieve info
            userInput = double.Parse(Prompt(data));

            return userInput;
        }//end function
        #endregion
    }//end class
}//end namespace
