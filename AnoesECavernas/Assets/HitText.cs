using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HitText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed;
    Color color;

    // Update is called once per frame

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        color = text.color;
    }
    void Update()
    {
        
        transform.position = transform.position + new Vector3(0, Mathf.Abs(transform.position.y) * speed * Time.deltaTime,0);
        
        color.a += Time.deltaTime * speed;

        text.color = color;
    }
}
