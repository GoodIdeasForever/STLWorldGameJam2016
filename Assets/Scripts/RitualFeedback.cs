using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RitualFeedback : MonoBehaviour
{
    public Slider feedbackSlider;
    public Player player;

    void Update()
    {
        RitualGeneratorFSM.CurrentRitualProgress ritualProgress = null;

        if (Player.Red == player)
        {
            ritualProgress = GameState.instance.redPlayerRitualGenerator.currentRitualProgress;
        }
        if (Player.Blue == player)
        {
            ritualProgress = GameState.instance.bluePlayerRitualGenerator.currentRitualProgress;
        }
        if (ritualProgress.ritualNumber >= 0)
        {
            feedbackSlider.value = (float)ritualProgress.currentSteps / (float)ritualProgress.requiredSteps;
        }
        else
        {
            feedbackSlider.value = 0;
        }
    }
}
