using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverallFeedback : MonoBehaviour {

	public Slider feedbackSlider;
	public Player player;

	void Update()
	{
		int ritualProgress = 0;

		if (Player.Red == player)
		{
			ritualProgress = GameState.instance.SFPandasScore;
		}
		if (Player.Blue == player)
		{
			ritualProgress = GameState.instance.STLLambsScore;
		}
		if (ritualProgress >= 0)
		{
			feedbackSlider.value = (float)ritualProgress / (float)4;
		}
		else
		{
			feedbackSlider.value = 0;
		}
	}
}
