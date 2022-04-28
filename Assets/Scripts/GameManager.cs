using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    internal int score { get; private set; }

    internal static GameManager instance;

    private void Awake()
    {
        Physics2D.gravity = Vector2.zero;

        if (instance == null)
            instance = this;
    }

    internal void AddScore()
    {
        score += ConfigurationData.ScoreAdd;
        print("Score : " + score);
        UIScriptMethods.instance.ScoreChangeText(score);
    }

    internal void ReloadScene()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(ReloadSceneIEnumarator());
    }

    private IEnumerator ReloadSceneIEnumarator()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    internal void LostArragement()
    {
        StartCoroutine(LostOptionIEnumarator());
    }

    private IEnumerator LostOptionIEnumarator()
    {
        yield return new WaitForSeconds(1.9f);

        //I could Animate Time Scale from 1 to 0

        float startTime = Time.time;
        float duration = .3f;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;

            Time.timeScale = Mathf.Lerp(1, 0, t);

            yield return null;
        }
        Time.timeScale = 0;
        Lost();
    }

    private void Lost()
    {
        SceneManager.LoadScene("Menu");
    }

}
