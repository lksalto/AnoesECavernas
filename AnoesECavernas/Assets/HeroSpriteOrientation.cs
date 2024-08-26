using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpriteOrientation : MonoBehaviour
{
    public float timer;
    public float dt = 0.01f;
    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Seta um pequeno timer
        timer+=Time.deltaTime;
        if (timer > dt) { timer = 0; }
        else
        {
            //na metade desse timer pega uma posição como inicial
            if (timer < dt / 2) { startpos = transform.position; }
            else
            {
                //na outra metade analisa a mudança de posição
                if (startpos != transform.position)
                {
                    if (startpos.x > transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                    }
                }
            }
        }
    }
}
