using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnifeManager : MonoBehaviour
{

    #region Fields

    public static KnifeManager instance;

    private GameObject knifePrefab;

    private int KnifesCount { get; set; }

    private int knifes;


    List<Rigidbody2D> KnivesObjects;
    float halfSize;
    [SerializeField]
    SpriteRenderer targetFlash;
    [SerializeField]
    Transform targetPiecesFather;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;
        knifePrefab = Resources.Load("Knife") as GameObject;

        halfSize = knifePrefab.GetComponent<BoxCollider2D>().size.x / 2;

        KnivesObjects = new List<Rigidbody2D>();

    }

    private void Start()
    {
        InstantiateKnife();
        knifes++;

        KnifesCount = Random.Range(2, 7);

    }

   #endregion

    #region My Methods




    private IEnumerator AnimateFlash()
    {
        float startTime = Time.time;
        float duration = 0.025f;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;

            targetFlash.color = new Color(targetFlash.color.r, targetFlash.color.g, targetFlash.color.b, Mathf.Lerp(0, .2f, t));

            yield return null;
        }
        startTime = Time.time;
        duration = .2f;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;

            targetFlash.color = new Color(targetFlash.color.r, targetFlash.color.g, targetFlash.color.b, Mathf.Lerp(.2f, 0, t));

            yield return null;
        }


        targetFlash.color = new Color(targetFlash.color.r, targetFlash.color.g, targetFlash.color.b, 0);
    }

    private GameObject InstantiateKnife()
    {
        GameObject obj = Instantiate(knifePrefab);
        KnivesObjects.Add(obj.GetComponent<Rigidbody2D>());
        return obj;
    }

    internal void NewInstantiate()
    {
        //Add Score First
        GameManager.instance.AddScore();

        if (KnifesCount > knifes)
        {
            InstantiateKnife();
            Flash();
            knifes++;
        }
        else
        {
            //You've completed all the knifes
            //We shall destroy the target
            targetPiecesFather.gameObject.SetActive(true);

            Transform parent = targetPiecesFather.parent;

            parent.DetachChildren();

            Physics2D.gravity = new Vector2(0, -9.81f);

            AudioManager.instance.PlayerClip(AudioClips.TargetDestroyed);

            DestroyPiecesTarget(parent);
            ThrowKnifes();

            Destroy(parent.gameObject);

            GameManager.instance.ReloadScene();
        }

    }

    internal void ExistenceKnivesInScene(int knivesCount)
    {

        Transform target = targetFlash.transform.parent;

        float positionY = -target.GetComponent<CircleCollider2D>().radius + 0.577464f;
        positionY += halfSize;

        float maxAngle = 360 / knivesCount;
        float lastAngle = 0;
        for (int i = 0; i < knivesCount; i++)
        {
            GameObject myknife = InstantiateKnife();

            float angle = lastAngle + Random.Range(20, maxAngle) * Mathf.Deg2Rad;
            lastAngle = angle;
            Vector3 pos = target.position + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * positionY;
            myknife.transform.position = pos;
            myknife.transform.SetParent(target);
            myknife.transform.up = target.position - myknife.transform.position;


            Color color = myknife.GetComponent<SpriteRenderer>().color;

            color.a = 1;

            myknife.GetComponent<Knife>().enabled = false;
            myknife.GetComponent<SpriteRenderer>().color = color;
        }

    }


    private void DestroyPiecesTarget(Transform parent)
    {
        foreach (var item in AllTargetPieces())
        {

            Vector2 direction = parent.position - item.transform.position;
            direction = direction.normalized;

            item.AddForceAtPosition(direction * 200f, parent.position);
            item.AddTorque(4f, ForceMode2D.Impulse);
        }
    }

    private void ThrowKnifes()
    {
        for (int i = 0; i < KnivesObjects.Count - 1; i++)
        {
            KnivesObjects[i].AddTorque(10f, ForceMode2D.Impulse);
            KnivesObjects[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        Rigidbody2D lastOne = KnivesObjects[KnivesObjects.Count - 1];

        lastOne.transform.SetParent(null);

        lastOne.GetComponent<Knife>().SendKnife(Vector2.up);

    }

    List<Rigidbody2D> AllTargetPieces()
    {
        List<Rigidbody2D> pieces = new List<Rigidbody2D>();


        for (int i = 0; i < targetPiecesFather.childCount; i++)
        {
            GameObject child = targetPiecesFather.GetChild(i).gameObject;
            pieces.Add(child.GetComponent<Rigidbody2D>());
            child.AddComponent<DestroyWhenInvisible>();
        }
        return pieces;
    }

    internal void Flash()
    {
        StartCoroutine(AnimateFlash());
    }


    #endregion

}
