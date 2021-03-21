using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thisisatestihatethis : MonoBehaviour
{
    Vector3 temp = new Vector3(0.1f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    // This is a comment to allow for testing
    void Update()
    {
        gameObject.GetComponent<thisisatestihatethis>().transform.position += temp;
    }
    void testFunc() {
    }
}
