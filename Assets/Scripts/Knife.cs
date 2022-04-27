using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour
{

    #region Fields
    Rigidbody2D rb;
    Vector2 intialPos;
    SpriteRenderer sprite;
    #endregion

    #region Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        intialPos = transform.position;
        StartCoroutine(AnimateKnife());
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SendKnife(Vector2.up);
            AudioManager.instance.PlayerClip(AudioClips.Shoot);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(tag) && transform.parent == null)
        {
            rb.velocity = Vector2.zero;
            AudioManager.instance.PlayerClip(AudioClips.Crash);
            SendKnife(Vector2.down);
            Torque();
            GameManager.instance.LostArragement();
        }
        else
        if (collision.CompareTag("Target"))
        {
            if (rb == null) return;

            rb.velocity = Vector2.zero;

            float halfSize = GetComponent<BoxCollider2D>().size.x / 2;

            Vector2 pos = transform.position;

            pos.y += halfSize;

            transform.position = pos;

            AudioManager.instance.PlayerClip(AudioClips.Hit);


            transform.SetParent(collision.transform);
            KnifeManager.instance.NewInstantiate();
            enabled = false;
        }
    }

    #endregion

    #region My Methods


    private IEnumerator AnimateKnife()
    {
        Vector2 downPos = intialPos;
        downPos.y -= .5f;

        float startTime = Time.time;
        float duration = .3f;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;

            transform.position = Vector2.Lerp(downPos, intialPos, t);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        transform.position = intialPos;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }

    internal void SendKnife(Vector2 vector)
    {
        rb.AddForce(vector * ConfigurationData.KnifeSpeed, ForceMode2D.Force);
    }


    internal void Torque()
    {
        rb.AddTorque(10f, ForceMode2D.Impulse);
    }
    #endregion


}
