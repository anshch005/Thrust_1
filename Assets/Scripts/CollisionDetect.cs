using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
     private void OnCollisionEnter(Collision other) 
    {  
        {
        switch(other.gameObject.tag)
        {
            case "Start":
                Debug.Log("Start pull the trigger");
                break;
            case "Finish":
                LevelUp();
                break;
            default:
                ReloadLevel();
                break;
        }
        }
        void ReloadLevel()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextscene =  currentScene + 1;
            
            if (nextscene == SceneManager.sceneCountInBuildSettings)
            {
                nextscene= 0;
            }

            SceneManager.LoadScene(nextscene);
        }

        void LevelUp()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}