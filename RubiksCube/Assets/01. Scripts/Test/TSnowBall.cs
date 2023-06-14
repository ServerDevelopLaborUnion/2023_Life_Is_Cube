using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class TSnowBall : MonoBehaviour
{
    public float speed;
    public float rotateAmount;

    private void Update()
    {
        transform.position += new Vector3(1, 0, -1) * Time.deltaTime * speed;
        transform.eulerAngles += new Vector3(0, 0, -rotateAmount) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(1);
        if (collision.gameObject.name == "Idle")
        {
            collision.transform.SetParent(transform.GetChild(0));
            collision.transform.localPosition = new Vector3(3.78f, 4.69f, 0);
            collision.transform.eulerAngles = new Vector3(180, -73, 0);
            collision.transform.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
