using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AiMove : MonoBehaviour
{
    private AIPath path;
    public float Speed;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = Speed;

        path.destination = transform.position;
    }
}
