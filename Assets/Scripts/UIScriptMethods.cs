using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScriptMethods : MonoBehaviour
{
    #region Fileds
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider backGroundMusicSlider;
    [SerializeField] Text scoreText;
    const string scoreCst = "Score : ";
    internal static UIScriptMethods instance;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;

        ConfigurationData.GetData();
    }

    private void Start()
    {
        GetComponent<AudioSource>()
      .volume = ConfigurationData.BackGroundMusicVolume;
        //Cant work cuz AudioManager is null in that case
        if (musicSlider == null) return;
        musicSlider.onValueChanged.AddListener(delegate
        {
            ConfigurationData.MusicVolume = musicSlider.value;
        });

        backGroundMusicSlider.onValueChanged.AddListener(delegate
        {
            ConfigurationData.BackGroundMusicVolume = backGroundMusicSlider.value;
        });

        //I did it here instead of AudioManager So that I will control
        //the volume of the clip in the menu as well with one code

    }

    #endregion

    #region My Methods
    public void Play()
    {
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


    #endregion




}
