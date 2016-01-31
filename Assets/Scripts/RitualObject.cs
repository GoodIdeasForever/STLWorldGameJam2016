using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RitualObject : MonoBehaviour 
{
    public Player player;
    public RitualObjectId ritualId;
    public GameObject unselectedVisualization;
    public GameObject selectedVisualization;
    public GameObject interaction1Visualization;
    public GameObject interaction2Visualization;
    public GameObject interaction3Visualization;
    public GameObject interaction4Visualization;

    public void PlayObjectInteraction(RitualObjAnimation anim)
    {
        unselectedVisualization.SetActive(RitualObjAnimation.Deselected == anim);
        selectedVisualization.SetActive(RitualObjAnimation.Selected == anim);
        interaction1Visualization.SetActive(RitualObjAnimation.Action1 == anim);
        interaction2Visualization.SetActive(RitualObjAnimation.Action2 == anim);
        interaction3Visualization.SetActive(RitualObjAnimation.Action3 == anim);
        interaction4Visualization.SetActive(RitualObjAnimation.Action4 == anim);

        int interaction = (int)anim;

        if (interaction < 4)
        {
            if (Player.Red == player)
            {
                GameState.instance.redPlayerRitualGenerator.InteractWithObject((int)ritualId, interaction);
            }
            if (Player.Blue == player)
            {
                GameState.instance.bluePlayerRitualGenerator.InteractWithObject((int)ritualId, interaction);
            }
        }

    }
}