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
    public Dictionary<string, string> battleResults;

    public static GameState instance;

    void Awake()
    {
        instance = this;
        battleResults.Add("D", "SA");
        battleResults.Add("S", "UC");
        battleResults.Add("U", "DC");
        battleResults.Add("C", "DA");
        battleResults.Add("A", "SU");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
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
        string redDefeats;
        if(battleResults.TryGetValue(redSummon.ToString(), out redDefeats))
        {
            if (redDefeats.Contains(blueSummon.ToString()))
            {
                return BattleResult.RedVictory;
            }
            else
            {
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