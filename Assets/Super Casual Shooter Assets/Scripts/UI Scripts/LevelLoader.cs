using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator CrossFade;

    // Reference to the button
    public Button button;

    void Start()
    {
        // Assign a function to be called when the button is clicked
        button.onClick.AddListener(load);
    }

    public void load()
    {
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        CrossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene();
        int nextLevelIndex = 1 - scene.buildIndex;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
