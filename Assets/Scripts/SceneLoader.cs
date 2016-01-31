using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource changeMenuSound;
   
    public void Awake()
    {
        changeMenuSound = gameObject.GetComponent<AudioSource>();
    }
	public void LoadNewScene(string sceneToLoad)
	{

        if (changeMenuSound != null )
            changeMenuSound.Play();

        SceneManager.LoadScene(sceneToLoad); 

	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif
	}
}
