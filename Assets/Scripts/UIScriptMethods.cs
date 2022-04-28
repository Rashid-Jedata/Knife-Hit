using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIScriptMethods : MonoBehaviour
{
    #region Fields
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider backGroundMusicSlider;
    [SerializeField] Text scoreText;
    [SerializeField] List<SpriteRenderer> UIKnives;
    const string scoreCst = "Score : ";
    internal static UIScriptMethods instance;
    List<SpriteRenderer> UsedKnives;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        ConfigurationData.GetData();

        if (musicSlider == null) return;
        musicSlider.value = ConfigurationData.MusicVolume;
        backGroundMusicSlider.value = ConfigurationData.BackGroundMusicVolume;

    }

    private void Start()
    {
        GetComponent<AudioSource>()
      .volume = ConfigurationData.BackGroundMusicVolume;

        ////Cant work cuz AudioManager is null in that case
        //if (musicSlider == null) return;
        //musicSlider.onValueChanged.AddListener(delegate
        //{
        //    ConfigurationData.MusicVolume = musicSlider.value;
        //});

        //backGroundMusicSlider.onValueChanged.AddListener(delegate
        //{
        //    ConfigurationData.BackGroundMusicVolume = backGroundMusicSlider.value;
        //});


        //I did it here instead of AudioManager So that I will control
        //the volume of the clip in the menu as well with one code

    }


    #endregion

    #region My Methods
    public void Play()
    {
        ConfigurationData.BackGroundMusicVolume = backGroundMusicSlider.value;
        ConfigurationData.MusicVolume = musicSlider.value;
        print($" Music Volume is {ConfigurationData.BackGroundMusicVolume}");
        ; ConfigurationData.SaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ScoreChangeText(int value)
    {
        scoreText.text = scoreCst + value;
    }

    internal void ShowKnivesStart(int knives)
    {
        UsedKnives = new List<SpriteRenderer>();
        int count = 0;
        while (knives > count)
        {
            Color color = UIKnives[count].color;

            UIKnives[count].color = new Color(color.r, color.g, color.b, 1);
            UsedKnives.Add(UIKnives[count]);
            count++;
        }
    }

    internal void UpdateKnives()
    {
        if (UsedKnives.Count < 1) return;
        Color color = UsedKnives[UsedKnives.Count - 1].color;

        UsedKnives[UsedKnives.Count - 1].color = new Color(color.r,color.g,color.b,.113f);
        UsedKnives.RemoveAt(UsedKnives.Count - 1);

    }

    #endregion





}
