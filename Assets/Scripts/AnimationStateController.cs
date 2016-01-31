using UnityEngine;
using System.Collections;

public class AnimationStateController : MonoBehaviour {
    
    public GameObject[] animations;
    public AudioClip[] audio;
    public AudioSource aSource;
    public void PlayAnimation(int animId)
    {
        Clear();
        animations[animId].SetActive(true);
        if (animId < audio.Length)
        {
            aSource.clip = audio[animId];
            aSource.Play();
        }
    }

    public void Clear()
    {
        foreach (var anim in animations)
        {
            anim.SetActive(false);
        }
    }

}
