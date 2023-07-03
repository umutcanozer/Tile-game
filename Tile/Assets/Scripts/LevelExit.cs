using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;


    void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.gameObject.tag == "Player") StartCoroutine(Exit());
    }

    IEnumerator Exit()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(levelLoadDelay);
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0; //if the index is latest index than load the first scene
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
