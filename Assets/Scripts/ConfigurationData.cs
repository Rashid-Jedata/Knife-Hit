using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public static class ConfigurationData
{
    #region Fileds And Properties Of Data
    private static int meduimScore;

    public static int MeduimScore
    {
        get { return meduimScore; }
        set
        {
            values["MeduimScore"] = meduimScore;
            meduimScore = value;
        }
    }

    private static int hardScoreScore;
    public static int HardScoreScore
    {
        get { return hardScoreScore; }
        set
        {
            values["HardScoreScore"] = hardScoreScore;
            hardScoreScore = value;
        }
    }


    private static float waitToChangeSpeed;
    public static float WaitToChangeSpeed
    {
        get { return waitToChangeSpeed; }
        set
        {
            values["WaitToChangeSpeed"] = waitToChangeSpeed;
            waitToChangeSpeed = value;
        }
    }

    private static float easyLevelSpeed;
    public static float EasyLevelSpeed
    {
        get { return easyLevelSpeed; }
        set
        {
            values["EasyLevelSpeed"] = easyLevelSpeed;
            easyLevelSpeed = value;
        }
    }

    private static float rangeSpeed;
    public static float RangeSpeed
    {
        get { return rangeSpeed; }
        set
        {
            values["RangeSpeed"] = rangeSpeed;
            rangeSpeed = value;
        }
    }

    private static int knivesCountHardLevel;
    public static int KnivesCountHardLevel
    {
        get { return knivesCountHardLevel; }
        set
        {
            values["KnivesCountHardLevel"] = knivesCountHardLevel;
            knivesCountHardLevel = value;
        }
    }

    private static float musicVolume;
    public static float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            values["MusicVolume"] = musicVolume;
            musicVolume = value;
        }
    }

    private static float backGroundMusicVolume;
    public static float BackGroundMusicVolume
    {
        get { return backGroundMusicVolume; }
        set
        {
            values["BackGroundMusicVolume"] = backGroundMusicVolume;
            backGroundMusicVolume = value;
        }
    }

    private static float knifeSpeed;
    public static float KnifeSpeed
    {
        get { return knifeSpeed; }
        set
        {
            values["KnifeSpeed"] = knifeSpeed;
            knifeSpeed = value;
        }
    }

    private static int scoreAdd;
    public static int ScoreAdd
    {
        get { return scoreAdd; }
        set
        {
            values["ScoreAdd"] = scoreAdd;
            scoreAdd = value;
        }
    }
    #endregion

    #region Fileds 
    private static Dictionary<string, float> values;
    const string dataFileName = "GameData.csv";

    #endregion

    #region  Private Methods
    private static void SetDefaultData()
    {
        values["MeduimScore"] = 30;
        values["HardScoreScore"] = 50;
        values["WaitToChangeSpeed"] = 6;
        values["EasyLevelSpeed"] = 15;
        values["RangeSpeed"] = 20;
        values["KnivesCountHardLevel"] = 4;
        values["MusicVolume"] = 0.089f;
        values["BackGroundMusicVolume"] = 0.01f;
        values["KnifeSpeed"] = 600;
        values["ScoreAdd"] = 10;
    }
    private static string path() { return Path.Combine(Application.streamingAssetsPath, dataFileName); }
    private static void SetValues()
    {
        MeduimScore = (int)values["MeduimScore"];
        HardScoreScore = (int)values["HardScoreScore"];
        WaitToChangeSpeed = values["WaitToChangeSpeed"];
        EasyLevelSpeed = values["EasyLevelSpeed"];
        RangeSpeed = values["RangeSpeed"];
        KnivesCountHardLevel = (int)values["KnivesCountHardLevel"];
        MusicVolume = values["MusicVolume"];
        BackGroundMusicVolume = values["BackGroundMusicVolume"];
        KnifeSpeed = values["KnifeSpeed"];
        ScoreAdd = (int)values["ScoreAdd"];
    }

    #endregion

    #region Public Methods
    public static void GetData()
    {
        NewInstanceData();
        if (!File.Exists(path())) return;

        using (StreamReader sr = new StreamReader(path()))
        {
            try
            {
                string line = sr.ReadLine();

                while (string.IsNullOrEmpty(line))
                {
                    string[] val = line.Split(',');
                    values.Add(val[0].Trim(), Convert.ToInt32(val[1].Trim()));
                    line = sr.ReadLine();
                }

            }
            catch (Exception) { SetDefaultData(); }
        }
        SetValues();
    }

    private static void NewInstanceData()
    {
        values = new Dictionary<string, float>();
        values.Add("MeduimScore", 30);
        values.Add("HardScoreScore", 50);
        values.Add("WaitToChangeSpeed", 6);
        values.Add("EasyLevelSpeed", 15);
        values.Add("RangeSpeed", 20);
        values.Add("KnivesCountHardLevel", 4);
        values.Add("MusicVolume", 0.089f);
        values.Add("BackGroundMusicVolume", 0.01f);
        values.Add("KnifeSpeed", 600);
        values.Add("ScoreAdd", 10);
    }

    public static void SaveData()
    {
        using (StreamWriter sr = new StreamWriter(path()))
        {
            try
            {
                foreach (var item in values)
                {
                    sr.WriteLine($"{item.Key} , {item.Value}");
                }
            }
            catch (Exception) { }
        }
    }

    #endregion


}
