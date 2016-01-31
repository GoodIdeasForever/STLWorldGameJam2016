using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource changeMenuSound;
    private static SceneLoader instance;
   
    public void Awake()
    {
        if(instance == null)
        {
            instance = new SceneLoader();
        }
        instance = this;
        changeMenuSound = gameObject.GetComponent<AudioSource>();
    }
    
    public static SceneLoader Instance {
        get 
        {
            if(instance == null) {
                instance = new SceneLoader();
            }  
            return instance;
        }
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