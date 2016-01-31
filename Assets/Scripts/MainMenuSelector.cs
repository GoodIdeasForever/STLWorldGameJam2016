using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using XboxCtrlrInput;

public class MainMenuSelector : MonoBehaviour
{
    public List<UnityEngine.UI.Button> menuButtons = new List<UnityEngine.UI.Button>();
    
    private List<XboxButton> registeredButtons = new List<XboxButton> { 
        XboxButton.A, XboxButton.DPadLeft, XboxButton.DPadRight
    };
    public List<String> registeredButtonsActions = new List<String>();
    private static MainMenuSelector instance;
    private int count = 0;
    private bool didQueryNumCtrls = false;
    public XboxController controller;
    

    void Awake()
    {
        if(instance == null)
        {
            instance = new MainMenuSelector();
        }
        instance = this;
    } 
    public static MainMenuSelector Instance 
    {
        get 
        {
            if(instance == null) 
            {
                instance = new MainMenuSelector();
            }  
            return instance;
        }
    }

	// Use this for initialization
    void Start () 
    {
        if (!didQueryNumCtrls)
        {
            didQueryNumCtrls = true;
 			// if(!checkEnoughControllersPresent())
            //  {
            //      GameState.Instance.noGameControllersPresent();
            //  }
        }
        XCI.DEBUG_LogControllerNames();
        UnityEngine.Color c = new UnityEngine.Color();
                c.r = 69;
                c.g = 255;
                c.b = 34;
                c.a = 255;
        menuButtons[0].image.color = c;
	}
    
    void setHighlightColor()
    {
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        XboxButton btn = getButtonSelection();
        if(btn != XboxButton.NONE)
        {
            Debug.Log(String.Format("btn={0}", btn));
            bool apressed = false;
            switch(btn)
            {
                // case XboxButton.DPadLeft:
                //     Debug.Log(String.Format("Count={0}  lft", count));
                //     if(count - 1 <= 0)
                //     {
                //         Debug.Log("BLAH");
                //         count = 2;
                //     }
                //     else
                //     {
                //         count -= 1;    
                //     }
                //     break;
                case XboxButton.DPadRight:
                Debug.Log(String.Format("Count={0}  rt", count));
                    if(count +1 >= 4)
                    {
                        count = 0;
                    }
                    else
                    {
                        count += 1;
                    }
                    break;
                case XboxButton.A:
                    Debug.Log("A button pressed");
                    apressed = true;
                    break;
                default:
                    Debug.Log(String.Format("Button {0} is unmapped", btn));
                    break;
            }
            for(int i=0; i< menuButtons.Count; i++)
            {
                if(count == i)
                {
                    UnityEngine.Color highlightColor = new UnityEngine.Color();
                    highlightColor.r = 69;
                    highlightColor.g = 255;
                    highlightColor.b = 34;
                    highlightColor.a = 255;
                    menuButtons[i].image.color = highlightColor;
                }
                else
                {
                    UnityEngine.Color c = new UnityEngine.Color();
                    c.r = 0;
                    c.g = 222;
                    c.b = 255;
                    c.a = 255;
                    menuButtons[i].image.color = c;
                }
            }
            if(apressed)
            {
                String action = registeredButtonsActions[count];
                if(action == "Exit")
                {
                    SceneLoader.Instance.ExitGame();
                }
                else
                {
                    SceneLoader.Instance.LoadNewScene(action);
                }
            } 
        }
	}
    
    private XboxButton getButtonSelection()
    {
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
}
