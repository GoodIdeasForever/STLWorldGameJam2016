using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;
using System;

public class MovePlayer : MonoBehaviour 
{
    public XboxController controller;
    public AudioSource changeSelection;
    
    private static bool didQueryNumCtrls = false;
    private List<XboxButton> registeredButtons = new List<XboxButton> { XboxButton.A, XboxButton.B, 
        XboxButton.X, XboxButton.Y, XboxButton.DPadLeft, XboxButton.DPadRight
    };
    private RitualObjectId curRitual = RitualObjectId.NONE;
    private Player curPlayer = Player.Red;

	// Use this for initialization
	void Start () 
    {
        if (!didQueryNumCtrls)
        {
            didQueryNumCtrls = true;
 			if(!checkEnoughControllersPresent())
             {
                 GameState.instance.noGameControllersPresent();
             }
        }
        UIManager.Instance.acceptingInput = true;
        XCI.DEBUG_LogControllerNames();
	}
    
    private bool checkEnoughControllersPresent()
    {
        bool rslt = false;
        int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
        Debug.Log(String.Format("Num plugged in controllers = {0}", queriedNumberOfCtrlrs));
		if(queriedNumberOfCtrlrs == 1)
		{
            Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
            rslt = true; //TODO: Testing only delete
		}
		else if (queriedNumberOfCtrlrs == 0)
		{
		  Debug.Log("No Xbox controllers plugged in!");
        }
		else
		{
            Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
            rslt = true;
		}
        return rslt;
    }
    
	// Update is called once per frame
	void Update () 
    {
        XboxButton btn = getButtonSelection();
        if(btn != XboxButton.NONE)
        {
            bool doPlaySound = true;
            Debug.Log(String.Format("Determing which controller {0}", controller));
            switch(controller)
            {
                case XboxController.First:
                    curPlayer = Player.Red;
                    break;
                case XboxController.Second:
                    curPlayer = Player.Blue;
                    break;
            }
            Debug.Log(String.Format("Getting button action for button={0}", btn));
            switch (btn)
            {
                case XboxButton.A:
                    Debug.Log(String.Format("Got button A for controller {0}", controller));
                    doRitualActionSelection(ActionButton.A);
                    break;
                case XboxButton.Y:
                    Debug.Log(String.Format("Got button Y for controller {0}", controller));
                    doRitualActionSelection(ActionButton.Y);
                    break;
                case XboxButton.B:
                    Debug.Log(String.Format("Got button B for controller {0}", controller));
                    doRitualActionSelection(ActionButton.B);
                    break;
                case XboxButton.X:
                    Debug.Log(String.Format("Got button X for controller {0}", controller));
                    doRitualActionSelection(ActionButton.X);
                    break;
                case XboxButton.DPadLeft:
                    Debug.Log(String.Format("Got left pad for controller {0}. Getting Ritual", controller));
                    curRitual = UIManager.Instance.SelectRitualObject(Direction.Left, curPlayer);
                    Debug.Log(String.Format("controller {0} ritual {1}", controller, curRitual));
                    break;
                case XboxButton.DPadRight:
                    Debug.Log(String.Format("Got right pad for controller {0}. Getting Ritual", controller));
                    curRitual = UIManager.Instance.SelectRitualObject(Direction.Right, curPlayer);
                    Debug.Log(String.Format("controller {0} ritual {1}", controller, curRitual));
                    break;
                default:
                    Debug.Log(String.Format("Got button {0} from controller {1} however {2} it is unmapped", btn, controller, btn));
                    doPlaySound = false;
                    break;
            }
            if(doPlaySound)
            {
                playSound();
            }
        }
	}
    
    private void doRitualActionSelection(ActionButton btn)
    {
        if(curPlayer != null && curRitual != RitualObjectId.NONE)
        {
            if(UIManager.Instance.TrySelectRitualAction(btn, curPlayer))
            {
                Debug.Log(String.Format("Ritual for player {0} successful", curPlayer));
            }
            else
            {
                Debug.Log(String.Format("Ritual for player {0} failed", curPlayer));
            }
        }
        else
        {
            Debug.Log(String.Format("Player={0}  Ritual={1} . Either one not set", curPlayer, curRitual));
        }
    }
    
    private XboxButton getButtonSelection()
    {
        Debug.Log(String.Format("Controller = {0}", controller));
        foreach(XboxButton btn in registeredButtons)
        {
            if(XCI.GetButtonDown(btn, controller))
            {
                Debug.Log(String.Format("Got Button donw for btn {0}", btn));
                return btn;
            }
        }
        return XboxButton.NONE;
    }
    
    private void playSound()
    {
        // changeSelection.playSound();
    }
}
