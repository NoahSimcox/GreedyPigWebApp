using Microsoft.AspNetCore.Mvc;

namespace GreedyPigWebApp;
using System;


class Player
{
    public static string Play(bool goodStrat, int sitDownAfterRound, float avgScoreIfNotFail, ref int currScore, ref int rolls, ref int currRoll)
    {
        
        while(true)
        {
      
            Console.WriteLine("Current Role: ");
            currRoll = Convert.ToInt32(Console.ReadLine());

            currScore += currRoll; rolls++;
      
            if (currRoll == 5)
            {
                return "You failed";
                currScore = 0; rolls = 0;
            }

            if (Simulator.GoodStrat(goodStrat, rolls, currScore, avgScoreIfNotFail))
            {
                return $"Your current score is {currScore} and your next expected score is {Simulator.NextExpectedScore(currScore, avgScoreIfNotFail)} therefore you should give up and lock in your points";
                currScore = 0;
            }
            else if (Simulator.BadStrat(goodStrat, rolls, sitDownAfterRound))
            {
                return $"Your current score is {currScore} and it has been {sitDownAfterRound} rounds therefore you should give up and lock in your points";
                currScore = 0;
                rolls = 1;
            }
      
        }
    }
}