using System;

namespace NumberGuessingGame
{
    class Program
    {
        private static readonly Random random = new Random();
        private const int MIN_NUMBER = 1;
        private const int MAX_NUMBER = 100;
        
        static void Main(string[] args)
        {
            Console.WriteLine("数当てゲームへようこそ！");
            Console.WriteLine($"{MIN_NUMBER}から{MAX_NUMBER}の間の数字を当ててください。");
            Console.WriteLine("0を入力すると終了します。");
            Console.WriteLine();
            
            PlayGame();
        }
        
        private static void PlayGame()
        {
            int targetNumber = GenerateRandomNumber();
            int attempts = 0;
            
            while (true)
            {
                int userGuess = GetUserInput();
                
                // 0が入力された場合は終了
                if (userGuess == 0)
                {
                    Console.WriteLine("残念でした。さようなら");
                    break;
                }
                
                attempts++;
                
                ComparisonResult result = CompareGuess(userGuess, targetNumber);
                
                if (result == ComparisonResult.Correct)
                {
                    Console.WriteLine($"正解です！{attempts}回で当てました！");
                    
                    if (AskPlayAgain())
                    {
                        targetNumber = GenerateRandomNumber();
                        attempts = 0;
                        Console.WriteLine();
                        Console.WriteLine("新しいゲームを開始します！");
                    }
                    else
                    {
                        Console.WriteLine("ゲームを終了します。ありがとうございました！");
                        break;
                    }
                }
                else if (result == ComparisonResult.TooHigh)
                {
                    Console.WriteLine("もっと小さい数字です。");
                }
                else if (result == ComparisonResult.TooLow)
                {
                    Console.WriteLine("もっと大きい数字です。");
                }
            }
        }
        
        private static int GenerateRandomNumber()
        {
            return random.Next(MIN_NUMBER, MAX_NUMBER + 1);
        }
        
        private static int GetUserInput()
        {
            while (true)
            {
                Console.Write("数字を入力してください: ");
                string? input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("エラー: 範囲内の整数を指定してください。");
                    continue;
                }
                
                if (!int.TryParse(input, out int number))
                {
                    Console.WriteLine("エラー: 範囲内の整数を指定してください。");
                    continue;
                }
                
                // 0は特別扱い（終了用）
                if (number == 0)
                {
                    return number;
                }
                
                // 範囲チェック
                if (number < MIN_NUMBER || number > MAX_NUMBER)
                {
                    Console.WriteLine("エラー: 範囲内の整数を指定してください。");
                    continue;
                }
                
                return number;
            }
        }
        
        private static ComparisonResult CompareGuess(int guess, int target)
        {
            if (guess == target)
                return ComparisonResult.Correct;
            else if (guess > target)
                return ComparisonResult.TooHigh;
            else
                return ComparisonResult.TooLow;
        }
        
        private static bool AskPlayAgain()
        {
            while (true)
            {
                Console.Write("もう一度プレイしますか？ (y/n): ");
                string? input = Console.ReadLine()?.ToLower().Trim();
                
                if (input == "y" || input == "yes")
                    return true;
                else if (input == "n" || input == "no")
                    return false;
                else
                    Console.WriteLine("yまたはnを入力してください。");
            }
        }
    }
    
    enum ComparisonResult
    {
        Correct,
        TooHigh,
        TooLow
    }
}
