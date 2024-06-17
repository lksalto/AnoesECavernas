using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigRace : MonoBehaviour
{
    public GameObject[] javalis;
    public Vector2 range;

    // Start is called before the first frame update
    void Start()
    {
        CorrectValues(range);
        for(int i = 0; i < javalis.Length; i++) 
        {
            javalis[i].GetComponent<EnemyLife>().initialSpeed= Random.Range(range.x,range.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CorrectValues(Vector2 range) 
    {
        if (range.x > range.y)
        {
            float aux = range.x;
            range.x = range.y;
            range.y = aux;
        }
        if (range.x < 0 || range.y < 0)
        {
            range.x = Mathf.Abs(range.x);
            range.y = Mathf.Abs(range.y);
        }
        if (range.x == 0) { range.x = 1; }
        if (range.y == 0) { range.y = 1; }
    }
}
