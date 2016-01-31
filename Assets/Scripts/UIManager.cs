using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RitualObjectId
{
    Jersey,
    BobbleHead,
    PizzaBox,
    Buddha,
    FoamFinger,
    Socks
}

public enum RitualObjAnimation
{
    Selected,
    Action1,
    Action2,
    Action3,
    Action4,
    None
}

public enum Player
{
    Red,
    Blue
}

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public List<RectTransform> bluejerseyAnimations;
    public List<RectTransform> bluebobbleHeadAnimations;
    public List<RectTransform> bluepizzaBoxAnimations;
    public List<RectTransform> bluebuddhaAnimations;
    public List<RectTransform> bluefoamFingerAnimations;
    public List<RectTransform> bluesocksAnimations;
    public List<RectTransform> redjerseyAnimations;
    public List<RectTransform> redbobbleHeadAnimations;
    public List<RectTransform> redpizzaBoxAnimations;
    public List<RectTransform> redbuddhaAnimations;
    public List<RectTransform> redfoamFingerAnimations;
    public List<RectTransform> redsocksAnimations;

    void Awake()
    {
        instance = this;
    }
	
	public void DisplayAnimation(RitualObjectId ritObject, RitualObjAnimation anim, Player play)
    {

    }

    public void ClearPlayerSelections(Player player)
    {

    }
}
