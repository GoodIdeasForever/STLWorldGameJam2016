using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RitualObject : MonoBehaviour 
{
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
    }
}