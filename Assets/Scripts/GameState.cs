using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BattleResult
{
    RedVictory,
    BlueVictory,
    Draw
}


public sealed class GameState : MonoBehaviour
{

	public int STLLambsScore;
	public int SFPandasScore;
    public Dictionary<string, string> battleResults = new Dictionary<string, string>();

    public static GameState instance;

    public RitualGeneratorFSM redPlayerRitualGenerator = new RitualGeneratorFSM();
    public RitualGeneratorFSM bluePlayerRitualGenerator = new RitualGeneratorFSM();

    void Awake()
    {
        instance = this;
        battleResults.Add("D", "SA");
        battleResults.Add("S", "UC");
        battleResults.Add("U", "DC");
        battleResults.Add("C", "DA");
        battleResults.Add("A", "SU");

        redPlayerRitualGenerator.InitializeRituals(Random.Range(int.MinValue, int.MaxValue));
        bluePlayerRitualGenerator.InitializeRituals(Random.Range(int.MinValue, int.MaxValue));
    }

    public void noGameControllersPresent()
    {
        //TODO: Add in function callback
    }

    public BattleResult GetBattleResult(Summon redSummon, Summon blueSummon)
    {
        if (redSummon == blueSummon)
        {
            return BattleResult.Draw;
        }
        else if (redSummon == Summon.NONE)
        {
            return BattleResult.BlueVictory;
        }
        else if (redSummon == Summon.NONE)
        {
            return BattleResult.RedVictory;
        }

        string redDefeats;
        if(battleResults.TryGetValue(redSummon.ToString(), out redDefeats))
        {
            if (redDefeats.Contains(blueSummon.ToString()))
            {
                ++SFPandasScore;
                return BattleResult.RedVictory;
            }
            else
            {
                ++STLLambsScore;
                return BattleResult.BlueVictory;
            }
        }
        else
        {
            Debug.LogError("Bad Dictionary battleResults Get call");
            return BattleResult.Draw;
        }
    }
}