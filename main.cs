namespace GreedyPigWebApp;
using System;

class Program {
  
    public static string Main (string gameType, string strategy, int sitDownAfterRound, int currRoll)
    {

        // this is based on the fact that 5 is the lose number
        const float avgScoreIfNotFail = 3.2f;
        int currScore = 0; int rolls = 1; int rounds = 0;
        
        bool goodStrat = bool.Parse(strategy);

        
        return Simulator.Simulate(goodStrat, sitDownAfterRound, avgScoreIfNotFail, ref currScore, ref rolls, ref rounds, ref currRoll);
    }
}