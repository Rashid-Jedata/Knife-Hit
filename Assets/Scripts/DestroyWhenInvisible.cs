using UnityEngine;
using System.Collections;

public class DestroyWhenInvisible : MonoBehaviour
{

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
