using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKeyboardMoves : MonoBehaviour
{
    public float Speed=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V)) 
        {
            transform.position += new Vector3(Speed * Time.deltaTime, 0f, 0f);
        }
    }
}
