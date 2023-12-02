namespace GreedyPigWebApp;
using System;

class Simulator
{
  private const int DiceMinVal = 1;
  private const int DiceMaxVal = 6;
  private const int Sets = 10000;

  public static string Simulate(bool goodStrat, int sitDownAfterRound, float avgScoreIfNotFail, ref int currScore, ref int rolls, ref int rounds, ref int currRoll)
  {
    
    Random dieRoll = new Random();
    float totalRolls = 0f;
    float totalRounds = 0f;
    int totalPoints = 0;
    int losses = 0;


    for (int j = 0; j < Sets; j++)
    { 

      currScore = 0; rolls = -1; rounds = 0; currRoll = 0;
      
      while (true)
      {

        // Calculates new score and increases rolls
        rolls++; currScore += currRoll;

        // checks for round endings based on strategy
        if (GoodStrat(goodStrat, rolls, currScore, avgScoreIfNotFail))
        {
          rounds++; currScore += currRoll; break;
        }
        else if (BadStrat(goodStrat, rolls, sitDownAfterRound))
        {
          rounds++; break;
        }

        // rolls the die 
        currRoll = dieRoll.Next(DiceMinVal, DiceMaxVal+1);

        // checks for a fail state right after the role
        if (currRoll == 5)
        {
          rounds++; losses++; currScore = 0; break;
        }
      }

      // Calculates totals for the set
      totalPoints += currScore;
      totalRolls += rolls;
      totalRounds += rounds;
    }

    // console outputs
    if (!goodStrat)
      return $"Total points gained when sitting down after {sitDownAfterRound} rounds: {totalPoints}\r\n    Total rolls: {totalRolls}\r\n    Total losses: {losses}\r\n    Average points gained per set: {totalPoints/totalRounds}";
    else
      return $"Total points gained when using the score strategy: {totalPoints}   Total rolls: {totalRolls}    Total losses: {losses}\r\n    Average points gained per set: {totalPoints/totalRounds}";
  }


  public static float NextExpectedScore(int currentScore, float avgScore)
  {
    return ((5f/6) * (currentScore + avgScore));
  }


  // if your next roll nets a lower score then your current, just cut your losses
  public static bool GoodStrat(bool goodStrat, int rolls, int currScore, float avgScoreIfNotFail)
  { 
    return (goodStrat && rolls > 0 && currScore >= NextExpectedScore(currScore, avgScoreIfNotFail));
  }

  // if you get to the agreed upon sit down round you must give up
  public static bool BadStrat(bool goodStrat, int rolls, int sitDownAfterRound)
  {
    return (!goodStrat && rolls > sitDownAfterRound);
  }
}