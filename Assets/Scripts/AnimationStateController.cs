using UnityEngine;
using System.Collections;

public class AnimationStateController : MonoBehaviour {
    
    public GameObject[] animations;

    public void PlayAnimation(int animId)
    {
        Clear();
        animations[animId].SetActive(true);
    }

    public void Clear()
    {
        foreach (var anim in animations)
        {
            anim.SetActive(false);
        }
    }

}
