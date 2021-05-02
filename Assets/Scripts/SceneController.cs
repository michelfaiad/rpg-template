using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(ChangeSceneAfterFade());
    }

    IEnumerator ChangeSceneAfterFade()
    {
        FadeController.inst.FadeToBlack();

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(1);
    }
}
