using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using System;

public class MovePlayer : MonoBehaviour 
{
    public XboxController controller;
    private static bool didQueryNumCtrls = false;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Blah");
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
	   if(XCI.GetButton(XboxButton.A, controller))
       {
           Debug.Log(String.Format("Got button A for controller {0}", controller));
       }
       if(XCI.GetButton(XboxButton.Y, controller))
       {
           Debug.Log(String.Format("Got button Y for controller {0}", controller));
       }
       if(XCI.GetButton(XboxButton.B, controller))
       {
           Debug.Log(String.Format("Got button B for controller {0}", controller));
       }
       if(XCI.GetButton(XboxButton.X, controller))
       {
           Debug.Log(String.Format("Got button X for controller {0}", controller));
       }
	}
}
