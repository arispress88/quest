using System;
using System.Collections.Generic;

// Every class in the program is defined within the "Quest" namespace
// Classes within the same namespace refer to one another without a "using" statement
namespace Quest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while(playAgain)
            {
                Console.WriteLine("Enter your name:");
                string playerName = Console.ReadLine();

                Robe robe = new Robe();
                robe.Colors = new List<string> {"red", "blue", "green"};
                robe.Length = 60;

                Hat hat = new Hat();
                hat.ShininessLevel = 7;

                Adventurer theAdventurer = new Adventurer(playerName, robe, hat);

                Console.WriteLine(theAdventurer.GetDescription()); //Fetches the code from Adventurer.cs

                // Create a few challenges for our Adventurer's quest
                // The "Challenge" Constructor takes three arguments
                //   the text of the challenge
                //   a correct answer
                //   a number of awesome points to gain or lose depending on the success of the challenge
                Challenge twoPlusTwo = new Challenge("2 + 2?", 4, 10);
                Challenge theAnswer = new Challenge(
                "What's the answer to life, the universe and everything?", 42, 25);
                Challenge whatSecond = new Challenge(
                "What is the current second?", DateTime.Now.Second, 50);

                int randomNumber = new Random().Next() % 10;
                Challenge guessRandom = new Challenge("What number am I thinking of?", randomNumber, 25);

                Challenge favoriteBeatle = new Challenge(
                @"Who's your favorite Beatle?
                1) John
                2) Paul
                3) George
                4) Ringo
                ",
                4, 20
                );

                // "Awesomeness" is like our Adventurer's current "score"
                // A higher Awesomeness is better

                // Here we set some reasonable min and max values.
                //  If an Adventurer has an Awesomeness greater than the max, they are truly awesome
                //  If an Adventurer has an Awesomeness less than the min, they are terrible
                int minAwesomeness = 0;
                int maxAwesomeness = 100;

                // A list of challenges for the Adventurer to complete
                // Note we can use the List class here because have the line "using System.Collections.Generic;" at the top of the file.
                List<Challenge> challenges = new List<Challenge>()
                {
                    twoPlusTwo,
                    theAnswer,
                    whatSecond,
                    guessRandom,
                    favoriteBeatle
                };

                //5 ADDITIONAL QUESTIONS
                Challenge mathQuestion = new Challenge("What is the square root of 144?", 12, 15);
                Challenge teeth = new Challenge("How many teeth does an adult human have?", 32, 20);
                Challenge football = new Challenge("How many points is a touchdown worth in football?", 6, 10);
                Challenge yard = new Challenge("How many feet are in a yard?", 3, 10);
                Challenge basketball = new Challenge("How many players are on the basketball court at a time, per team?", 5, 20);

                List<Challenge> allChallenges = new List<Challenge>()
                {
                    twoPlusTwo,
                    theAnswer,
                    whatSecond,
                    guessRandom,
                    favoriteBeatle,
                    mathQuestion,
                    teeth,
                    football,
                    yard,
                    basketball
                };

                List<Challenge> selectedChallenges = new List<Challenge>();
                Random random = new Random();
                int successfulChallenges = 0;

                while (selectedChallenges.Count < 5)
                {
                    int randomIndex = random.Next(0, allChallenges.Count);
                    Challenge randomChallenge = allChallenges[randomIndex];

                    if (!selectedChallenges.Contains(randomChallenge))
                    {
                        selectedChallenges.Add(randomChallenge);
                    }
                }

                // Loop through all the challenges and subject the Adventurer to them
                //ASKS THE USER ONE QUESTION AT TIME AS OPPOSED TO ALL AT ONCE
                foreach (Challenge challenge in selectedChallenges)
                {
                    bool challengeResult = challenge.RunChallenge(theAdventurer);
                    if (challengeResult)
                    {
                        successfulChallenges++;
                    }
                }

                // This code examines how Awesome the Adventurer is after completing the challenges
                // And praises or humiliates them accordingly
                //OUTPUTS A PARTICULAR LINE AT THE END DEPENDING ON THE AWESOMENESS SCORE FROM ADVENTURER.CS
                if (theAdventurer.Awesomeness >= maxAwesomeness)
                {
                 Console.WriteLine("YOU DID IT! You are truly awesome!");
                }
                else if (theAdventurer.Awesomeness <= minAwesomeness)
                {
                    Console.WriteLine("Get out of my sight. Your lack of awesomeness offends me!");
                }
                else
                {
                    Console.WriteLine("I guess you did...ok? ...sorta. Still, you should get out of my sight.");
                }

                Prize prize = new Prize("Congrats! You've won a shiny thing!");
                prize.ShowPrize(theAdventurer);
                Console.WriteLine("Do you want to play again? (yes/no)");
                string playAgainInput = Console.ReadLine().ToLower();
                playAgain = playAgainInput == "yes" || playAgainInput == "y";

                if (playAgain)
                {
                    int bonusAwesomeness = successfulChallenges * 10; // calculates the bonus for the next quest

                    theAdventurer.Awesomeness += bonusAwesomeness; // adds bonus awesomeness to the initial Awesomeness on the next quest

                    successfulChallenges = 0; //Resets the successful challenges count for the next quest
                }

                selectedChallenges.Clear();
            }
        }
    }
}