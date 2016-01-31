using UnityEngine;
using System.Collections;

public sealed class GameState : MonoBehaviour 
{

	public int STLLambsScore;
	public int SFPandasScore;
    
    private GameState() : base()
    {
    }
    
    private class Nested
    {
        static Nested()
        {
        }
        internal static readonly GameState instance = new GameState();
    }
    
    public static GameState Instance 
    {
        get
        {
            return Nested.instance;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}