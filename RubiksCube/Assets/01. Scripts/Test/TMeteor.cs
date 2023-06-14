using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMeteor : MonoBehaviour
{
    public float speed;

    [SerializeField] GameObject particle;
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(1);
        GameObject obj = Instantiate(particle);
        obj.transform.position = transform.position;
        Destroy(gameObject);
    }
}
