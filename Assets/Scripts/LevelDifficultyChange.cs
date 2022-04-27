using UnityEngine;
using System.Collections;

public enum Level
{
    Easy, Meduim, Hard
}

public class LevelDifficultyChange : MonoBehaviour
{

    #region Fileds
    private Level level;

    [SerializeField] Target target;


    #endregion

    #region Unity Methods

    private void Start()
    {
        SetDifficultyUsingScore();

        Speed();

    }

    #endregion

    #region My Methods
    private void SetDifficultyUsingScore()
    {
        if (GameManager.instance.score > ConfigurationData.HardScoreScore) level = Level.Hard;
        else if (GameManager.instance.score > ConfigurationData.MeduimScore) level = Level.Meduim;
        else level = Level.Easy;
    }

    internal int Speed()
    {
        switch (level)
        {
            case Level.Easy:
                target.speed = ConfigurationData.EasyLevelSpeed;
                break;
            case Level.Meduim:
                StartCoroutine(ChangeSpeed());
                break;
            case Level.Hard:
                KnifeManager.instance.ExistenceKnivesInScene(Random.Range(1, ConfigurationData.KnivesCountHardLevel));
                StartCoroutine(ChangeSpeed());
                break;
        }

        return 0;
    }


    private IEnumerator ChangeSpeed()
    {
        target.speed = Random.Range(-ConfigurationData.RangeSpeed, ConfigurationData.RangeSpeed);
        yield return new WaitForSeconds(Random.Range(1, ConfigurationData.WaitToChangeSpeed));
        StartCoroutine(ChangeSpeed());
    }

    #endregion





}
