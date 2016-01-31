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
    A,
    NONE
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
    public RitualObject bluejersey;
    public RitualObject bluebobbleHead;
    public RitualObject bluepizzaBox;
    public RitualObject bluebuddha;
    public RitualObject bluefoamFinger;
    public RitualObject bluesocks;
    public RitualObject redjersey;
    public RitualObject redbobbleHead;
    public RitualObject redpizzaBox;
    public RitualObject redbuddha;
    public RitualObject redfoamFinger;
    public RitualObject redsocks;

    RitualObjectId currentSelectionRed;
    RitualObjectId currentSelectionBlue;

    public bool acceptingInput { get; set; }

    public float showSummonGrahicDelay = 2f;
    public float showVSGraphicDelay = 3f;
    public float hideVSGraphicDelay = 4f;
    public float showAttackGraphicDelay = 4.5f;
    public float showResultGrahicDelay = 6f;
    public float resetTVCanvasDelay = 7f;

    public AnimationStateController redSummonAnimations;
    public AnimationStateController blueSummonAnimations;
    public AnimationStateController attackAnimations;
    public RectTransform versusAnimation;
    public AnimationStateController battleResultAnimations;

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
	
    // For debug testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectRitualObject(Direction.Right, Player.Red);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectRitualObject(Direction.Left, Player.Red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrySelectRitualAction(ActionButton.A, Player.Red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrySelectRitualAction(ActionButton.B, Player.Red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TrySelectRitualAction(ActionButton.X, Player.Red);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TrySelectRitualAction(ActionButton.Y, Player.Red);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplaySummonBattle((Summon)Random.Range(0, 6), (Summon)Random.Range(0, 6));
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
        else if (play == Player.Blue && acceptingInput)
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
            redjersey.PlayObjectInteraction(RitualObjAnimation.Deselected);
            redbobbleHead.PlayObjectInteraction(RitualObjAnimation.Deselected);
            redpizzaBox.PlayObjectInteraction(RitualObjAnimation.Deselected);
            redbuddha.PlayObjectInteraction(RitualObjAnimation.Deselected);
            redfoamFinger.PlayObjectInteraction(RitualObjAnimation.Deselected);
            redsocks.PlayObjectInteraction(RitualObjAnimation.Deselected);
            /*
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
            */
            currentSelectionRed = RitualObjectId.NONE;
        }
        else if (player == Player.Blue)
        {
            bluejersey.PlayObjectInteraction(RitualObjAnimation.Deselected);
            bluebobbleHead.PlayObjectInteraction(RitualObjAnimation.Deselected);
            bluepizzaBox.PlayObjectInteraction(RitualObjAnimation.Deselected);
            bluebuddha.PlayObjectInteraction(RitualObjAnimation.Deselected);
            bluefoamFinger.PlayObjectInteraction(RitualObjAnimation.Deselected);
            bluesocks.PlayObjectInteraction(RitualObjAnimation.Deselected);
            /*
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
            */
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
                    //foreach (var ui in redbobbleHeadAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //redbobbleHeadAnimations[(int)anim].gameObject.SetActive(true);
                    redbobbleHead.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.Buddha:
                    //foreach (var ui in redbuddhaAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //redbuddhaAnimations[(int)anim].gameObject.SetActive(true);
                    redbuddha.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.FoamFinger:
                    //foreach (var ui in redfoamFingerAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //redfoamFingerAnimations[(int)anim].gameObject.SetActive(true);
                    redfoamFinger.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.Jersey:
                    //foreach (var ui in redjerseyAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //redjerseyAnimations[(int)anim].gameObject.SetActive(true);
                    redjersey.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.PizzaBox:
                    //foreach (var ui in redpizzaBoxAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //redpizzaBoxAnimations[(int)anim].gameObject.SetActive(true);
                    redpizzaBox.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.Socks:
                    //foreach (var ui in redsocksAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //redsocksAnimations[(int)anim].gameObject.SetActive(true);
                    redsocks.PlayObjectInteraction(anim);
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
                    //foreach (var ui in bluebobbleHeadAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //bluebobbleHeadAnimations[(int)anim].gameObject.SetActive(true);
                    bluebobbleHead.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.Buddha:
                    //foreach (var ui in bluebuddhaAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //bluebuddhaAnimations[(int)anim].gameObject.SetActive(true);
                    bluebuddha.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.FoamFinger:
                    //foreach (var ui in bluefoamFingerAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //bluefoamFingerAnimations[(int)anim].gameObject.SetActive(true);
                    bluefoamFinger.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.Jersey:
                    //foreach (var ui in bluejerseyAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //bluejerseyAnimations[(int)anim].gameObject.SetActive(true);
                    bluejersey.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.PizzaBox:
                    //foreach (var ui in bluepizzaBoxAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //bluepizzaBoxAnimations[(int)anim].gameObject.SetActive(true);
                    bluepizzaBox.PlayObjectInteraction(anim);
                    break;
                case RitualObjectId.Socks:
                    //foreach (var ui in bluesocksAnimations)
                    //{
                    //    ui.gameObject.SetActive(false);
                    //}
                    //bluesocksAnimations[(int)anim].gameObject.SetActive(true);
                    bluesocks.PlayObjectInteraction(anim);
                    break;
                default:
                    break;

            }
        }
    }

    public void DisplaySummonBattle(Summon redSummon, Summon blueSummon)
    {
        acceptingInput = false;
        tvCanvasScaler.BeginScaleOut();
        BattleResult result = GameState.instance.GetBattleResult(redSummon, blueSummon);
        StartCoroutine(DelayShowSummonAnimations(redSummon, blueSummon));
        StartCoroutine(DelayShowVSAnimation());
        StartCoroutine(DelayHideVSAnimation());
        StartCoroutine(DelayShowAttackAnimation(redSummon,blueSummon,result));
        StartCoroutine(DelayShowResultAnimations(result));
        StartCoroutine(DelayResetTVCanvas());

    }

    IEnumerator DelayShowSummonAnimations(Summon redSummon, Summon blueSummon)
    {
        float timeDelta = 0f;
        while (timeDelta < showSummonGrahicDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        if (redSummon != Summon.NONE)
            redSummonAnimations.PlayAnimation((int)redSummon);
        if (blueSummon != Summon.NONE)
            blueSummonAnimations.PlayAnimation((int)blueSummon);
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
            attackAnimations.PlayAnimation((int)(result == BattleResult.RedVictory ? redSummon : blueSummon));
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
        battleResultAnimations.PlayAnimation((int)result);
    }

    IEnumerator DelayResetTVCanvas()
    {
        float timeDelta = 0f;
        while (timeDelta < resetTVCanvasDelay)
        {
            timeDelta += Time.deltaTime;
            yield return null;
        }
        redSummonAnimations.Clear();
        blueSummonAnimations.Clear();
        attackAnimations.Clear();
        versusAnimation.gameObject.SetActive(false);
        battleResultAnimations.Clear();
        if (GameState.instance.SFPandasScore >= 3 || GameState.instance.STLLambsScore >= 3)
        {

            battleResultAnimations.PlayAnimation((int)(GameState.instance.SFPandasScore >= 3 ? BattleResult.RedVictory : BattleResult.BlueVictory));
            StartCoroutine(DelayLoadMainMenuInSeconds(3f));
        }
        else
            tvCanvasScaler.BeginScaleIn();
    }
    IEnumerator DelayLoadMainMenuInSeconds(float seconds)
    {
        float delta = 0f;
        while (delta < seconds)
        {
            yield return null;
            delta = Time.deltaTime;
        }
        Application.LoadLevel("TitleScene");
    }
}
