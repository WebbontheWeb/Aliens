using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{   
    public string menuSceneName;
    public string gameSceneName;
    public string creditsSceneName;

    void Start()
    {
        laserController.OnPlayerDestroyed += LoadCreditsScene;

        //so scene switcher can move between scenes
        GameObject.DontDestroyOnLoad(gameObject);
    }


    public void LoadGameScene(){
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);

    }

    public void LoadCreditsScene(){
        StartCoroutine(LoadAndExitCredits());
    }

    IEnumerator LoadAndExitCredits()
    {   
        yield return new WaitForSeconds(1f);

        //leaving game in background
        SceneManager.LoadScene(creditsSceneName, LoadSceneMode.Single);
        
        //waiting 5 seconds to leave credits on screen
        yield return new WaitForSeconds(6f);

        //reloading main menu
        SceneManager.LoadScene(menuSceneName, LoadSceneMode.Single);
        //so there aren't two
        //it creates a new going back to the menu scene
        Destroy(gameObject);
    }
}
