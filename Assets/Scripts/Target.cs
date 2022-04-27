using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Dont set it in Configration Data
    internal float speed { get; set; }

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }



}
