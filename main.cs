namespace GreedyPigWebApp;
using System;

class Program {
  
    public static void Main (string gameType, string strategy, int sitDownAfterRound)
    {

        // this is based on the fact that 5 is the lose number
        const float avgScoreIfNotFail = 3.2f;
        int currScore = 0; int rolls = -1; int rounds = 0; int currRoll = 0;
        
        bool goodStrat = bool.Parse(strategy);


        if (Convert.ToChar(gameType) == 'p')
            Player.Play(goodStrat, sitDownAfterRound, avgScoreIfNotFail, ref currScore, ref rolls, ref currRoll);
        else
            Simulator.Simulate(goodStrat, sitDownAfterRound, avgScoreIfNotFail, ref currScore, ref rolls, ref rounds, ref currRoll);
    }
}