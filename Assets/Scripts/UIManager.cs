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
    Socks,
    NONE
}

public enum RitualObjAnimation
{
    Action1,
    Action2,
    Action3,
    Action4,
    Selected,
    Deselected
}

public enum Player
{
    Red,
    Blue
}

public enum Direction
{
    Left,
    Right
}

public enum ActionButton
{
    A,
    B,
    X,
    Y
}

public enum Summon
{
    D,
    C,
    U,
    S,
    A
}

public class UIManager : MonoBehaviour {

    private static UIManager instance;

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

    public ScaleToFullScreen tvCanvasScaler;

    RitualObjectId currentSelectionRed;
    RitualObjectId currentSelectionBlue;

    public bool acceptingInput { get; set; }

    public float showSummonGrahicDelay = 2f;
    public float showVSGraphicDelay = 3f;
    public float hideVSGraphicDelay = 4f;
    public float showAttackGraphicDelay = 4.5f;
    public float showResultGrahicDelay = 6f;
    public float resetTVCanvasDelay = 7f;

    public List<RectTransform> redSummonAnimations;
    public List<RectTransform> blueSummonAnimations;
    public List<RectTransform> attackAnimations;
    public RectTransform versusAnimation;
    public List<RectTransform> battleResultAnimations;

    void Awake()
    {
        if(instance == null)
        {
            instance = new UIManager();
        }
        instance = this;
    } 

    void Start()
    {
        acceptingInput = true;
    }

    public static UIManager Instance {
        get {
            if(instance == null) {
                instance = new UIManager();
            }  
            return instance;
        }
    }
	
	public RitualObjectId SelectRitualObject(Direction direction, Player play)
    {
        RitualObjectId tempSelection = RitualObjectId.NONE;
        if (play == Player.Red && acceptingInput)
        {
            if (currentSelectionRed == RitualObjectId.NONE)
            {
                tempSelection = RitualObjectId.Jersey;
            }
            else if (currentSelectionRed == RitualObjectId.Socks && direction == Direction.Right)
            {
                DisplaySelection(currentSelectionRed, RitualObjAnimation.Deselected, play);
                tempSelection = RitualObjectId.Jersey;
            }
            else if (currentSelectionRed == RitualObjectId.Jersey && direction == Direction.Left)
            {
                DisplaySelection(currentSelectionRed, RitualObjAnimation.Deselected, play);
                tempSelection = RitualObjectId.Socks;
            }
            else if (direction == Direction.Right)
            {
                DisplaySelection(currentSelectionRed, RitualObjAnimation.Deselected, play);
                tempSelection = (RitualObjectId)(((int)currentSelectionRed)+1);
            }
            else if (direction == Direction.Left)
            {
                DisplaySelection(currentSelectionRed, RitualObjAnimation.Deselected, play);
                tempSelection = (RitualObjectId)(((int)currentSelectionRed) - 1);
            }
            ClearPlayerSelections(play);
            currentSelectionRed = tempSelection;
        }
        else if (play == Player.Red && acceptingInput)
        {
            if (currentSelectionBlue == RitualObjectId.NONE)
            {
                tempSelection = RitualObjectId.Jersey;
            }
            else if (currentSelectionBlue == RitualObjectId.Socks && direction == Direction.Right)
            {
                DisplaySelection(currentSelectionBlue, RitualObjAnimation.Deselected, play);
                tempSelection = RitualObjectId.Jersey;
            }
            else if (currentSelectionBlue == RitualObjectId.Jersey && direction == Direction.Left)
            {
                DisplaySelection(currentSelectionBlue, RitualObjAnimation.Deselected, play);
                tempSelection = RitualObjectId.Socks;
            }
            else if (direction == Direction.Right)
            {
                DisplaySelection(currentSelectionBlue, RitualObjAnimation.Deselected, play);
                tempSelection = (RitualObjectId)(((int)currentSelectionBlue) + 1);
            }
            else if (direction == Direction.Left)
            {
                DisplaySelection(currentSelectionBlue, RitualObjAnimation.Deselected, play);
                tempSelection = (RitualObjectId)(((int)currentSelectionBlue) - 1);
            }

            ClearPlayerSelections(play);
            currentSelectionBlue = tempSelection;
        }
        DisplaySelection(tempSelection, RitualObjAnimation.Selected, play);
        return tempSelection;
    }

    public bool TrySelectRitualAction(ActionButton button, Player play)
    {
        if (play == Player.Red && acceptingInput)
        {
            if (currentSelectionRed == RitualObjectId.NONE)
            {
                return false;
            }
            else
            {
                DisplaySelection(currentSelectionRed, (RitualObjAnimation)button, play);
                return true;
            }
        }
        else if (play == Player.Blue && acceptingInput)
        {
            if (currentSelectionBlue == RitualObjectId.NONE)
            {
                return false;
            }
            else
            {
                DisplaySelection(currentSelectionBlue, (RitualObjAnimation)button, play);
                return true;
            }
        }
        return false;
    }

    public void ClearPlayerSelections(Player player)
    {
        if (player == Player.Red)
        {
            foreach (var canvas in redjerseyAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in redbobbleHeadAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in redfoamFingerAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in redpizzaBoxAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in redsocksAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in redbuddhaAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            currentSelectionRed = RitualObjectId.NONE;
        }
        else if (player == Player.Blue)
        {
            foreach (var canvas in bluejerseyAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in bluebobbleHeadAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in bluefoamFingerAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in bluepizzaBoxAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in bluesocksAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            foreach (var canvas in bluebuddhaAnimations)
            {
                canvas.gameObject.SetActive(false);
            }
            currentSelectionBlue = RitualObjectId.NONE;
        }
    }

    void DisplaySelection(RitualObjectId ritualObj, RitualObjAnimation anim, Player play)
    {
        if (play == Player.Red)
        {
            switch(ritualObj)
            {
                case RitualObjectId.BobbleHead:
                    foreach (var ui in redbobbleHeadAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    redbobbleHeadAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.Buddha:
                    foreach (var ui in redbuddhaAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    redbuddhaAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.FoamFinger:
                    foreach (var ui in redfoamFingerAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    redfoamFingerAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.Jersey:
                    foreach (var ui in redjerseyAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    redjerseyAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.PizzaBox:
                    foreach (var ui in redpizzaBoxAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    redpizzaBoxAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.Socks:
                    foreach (var ui in redsocksAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    redsocksAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                default:
                    break;

            }
        }
        else if (play == Player.Blue)
        {
            switch (ritualObj)
            {
                case RitualObjectId.BobbleHead:
                    foreach (var ui in bluebobbleHeadAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    bluebobbleHeadAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.Buddha:
                    foreach (var ui in bluebuddhaAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    bluebuddhaAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.FoamFinger:
                    foreach (var ui in bluefoamFingerAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    bluefoamFingerAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.Jersey:
                    foreach (var ui in bluejerseyAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    bluejerseyAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.PizzaBox:
                    foreach (var ui in bluepizzaBoxAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    bluepizzaBoxAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                case RitualObjectId.Socks:
                    foreach (var ui in bluesocksAnimations)
                    {
                        ui.gameObject.SetActive(false);
                    }
                    bluesocksAnimations[(int)anim].gameObject.SetActive(true);
                    break;
                default:
                    break;

            }
        }
    }

    public void DisplaySummonBattle(Summon redSummon, Summon blueSummon)
    {
        acceptingInput = false;
        tvCanvasScaler.BeginScaleIn();
        BattleResult result = GameState.instance.GetBattleResult(redSummon, blueSummon);
        StartCoroutine(DelayShowSummonAnimations(redSummon, blueSummon));
        StartCoroutine(DelayShowVSAnimation());
        StartCoroutine(DelayHideVSAnimation());
        StartCoroutine(DelayShowAttackAnimation(redSummon,blueSummon,result));
        StartCoroutine(DelayShowResultAnimations(result));

    }

    IEnumerator DelayShowSummonAnimations(Summon redSummon, Summon blueSummon)
    {
        float timeDelta = 0f;
        while (timeDelta < showSummonGrahicDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        redSummonAnimations[(int)redSummon].gameObject.SetActive(true);
        blueSummonAnimations[(int)blueSummon].gameObject.SetActive(true);
    }

    IEnumerator DelayShowVSAnimation()
    {
        float timeDelta = 0f;
        while (timeDelta < showVSGraphicDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        versusAnimation.gameObject.SetActive(true);
    }

    IEnumerator DelayHideVSAnimation()
    {
        float timeDelta = 0f;
        while (timeDelta < hideVSGraphicDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        versusAnimation.gameObject.SetActive(false);
    }

    IEnumerator DelayShowAttackAnimation(Summon redSummon, Summon blueSummon, BattleResult result)
    {
        float timeDelta = 0f;
        while (timeDelta < showAttackGraphicDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        if (result != BattleResult.Draw)
        {
            attackAnimations[(int)(result == BattleResult.RedVictory ? redSummon : blueSummon)].gameObject.SetActive(true);
        }
    }

    IEnumerator DelayShowResultAnimations(BattleResult result)
    {
        float timeDelta = 0f;
        while (timeDelta < showResultGrahicDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        battleResultAnimations[(int)result].gameObject.SetActive(true);
    }

    IEnumerator DelayResetTVCanvas()
    {
        float timeDelta = 0f;
        while (timeDelta < resetTVCanvasDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        foreach(var anim in redSummonAnimations)
        {
            anim.gameObject.SetActive(false);
        }
        foreach (var anim in blueSummonAnimations)
        {
            anim.gameObject.SetActive(false);
        }
        foreach (var anim in attackAnimations)
        {
            anim.gameObject.SetActive(false);
        }
        versusAnimation.gameObject.SetActive(false);
        foreach (var anim in battleResultAnimations)
        {
            anim.gameObject.SetActive(false);
        }
        tvCanvasScaler.BeginScaleIn();
    }
}
