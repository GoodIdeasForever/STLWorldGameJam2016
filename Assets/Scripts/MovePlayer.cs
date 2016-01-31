using UnityEngine;
using System.Collections;
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

	// Use this for initialization
	void Start () 
    {
        if (!didQueryNumCtrls)
        {
            didQueryNumCtrls = true;
 			int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
            Debug.Log(String.Format("Num plugged in controllers = {0}", queriedNumberOfCtrlrs));
			if(queriedNumberOfCtrlrs == 1)
			{
				Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
			}
			else if (queriedNumberOfCtrlrs == 0)
			{
				Debug.Log("No Xbox controllers plugged in!");
			}
			else
			{
				Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
			}
        }
        XCI.DEBUG_LogControllerNames();
	}
	
	// Update is called once per frame
	void Update () 
    {
        btn = getButtonSelection();
        if(typeof(Xbox) == typeof(btn))
        {
            bool doPlaySound = true;
            switch (btn)
            {
                case XboxButton.A:
                    Debug.Log(String.Format("Got button A for controller {0}", controller));
                case XboxButton.Y:
                    Debug.Log(String.Format("Got button Y for controller {0}", controller));
                case XboxButton.B:
                    Debug.Log(String.Format("Got button B for controller {0}", controller));
                case XboxButton.X:
                    Debug.Log(String.Format("Got button X for controller {0}", controller));
                case XboxButton.DPadLeft:
                    Debug.Log(String.Format("Got left pad for controller {0}", controller));
                case XboxButton.DPadRight:
                    Debug.Log(String.Format("Got right pad for controller {0}", controller));
                default:
                    Debug.Log(String.Format("Got button {0} from controller {1} however {2} it is unmapped"), btn, controller, btn);
                    doPlaySound = false;
            }
            if(doPlaySound)
            {
                playSound();
            }
        }
	}
    
    private XboxButton getButtonSelection()
    {
        for(XboxButton btn in registeredButtons)
        {
            if(XCI.GetButtonDown(btn, controller))
            {
                return btn;
            }
        }
        return null;  
        
    }
    
    private void playSound()
    {
        changeSelection.playSound();
    }
}
