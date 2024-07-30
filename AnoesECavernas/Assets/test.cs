using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class test : MonoBehaviour
{
    public float Dmg;
    public float AtkRadius;
    public float AtkCoolDown;
    public float Timer;
    public GameObject[] enemys; 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,AtkRadius);
    }
    private void Update()
    {
        Timer +=Time.deltaTime;
        if (Timer > AtkCoolDown) 
        {
            Timer = 0f;
        }
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemys.Length; i++) 
        {
            if (OnRadiusOf(enemys[i].transform, AtkRadius)) 
            {
                enemys[i].GetComponent<EnemyLife>().life-=DmgNTime(Dmg);
            }
        }
    }
    private float DmgNTime(float dmg) 
    {
        float Cooldmg;
        if (Timer < AtkCoolDown / 3) 
        {
            Cooldmg = 0f;
        }
        else 
        {
            Cooldmg = dmg;
        }
        return Cooldmg;
    }
    private bool OnRadiusOf(Transform gamobj, float radius)
    {
        if (Vector3.Distance(gamobj.position, transform.position) < radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
