using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ButtonAtk : MonoBehaviour
{
    //public float timescale=1;
    public float Dmg;
    //public float dmgntime;
    public float AtkRadius;
    public float AtkDuration;
    public float AtkCoolDown;
    public float Timer;
    public bool button;
    private GameObject[] enemys;
    [HideInInspector]
    public EnemyMang[] Struenemies, auxStr;
    //public EnemyMang[] auxStr;

    [System.Serializable]
    public struct EnemyMang
    {
        public GameObject enemy;
        public bool dmgTook;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AtkRadius);
    }
    private void Update()
    {
        //Time.timeScale = timescale;

        if (button) { Timer += Time.deltaTime; }
        if (Timer > AtkCoolDown)
        {
            Timer = 0f;
            button = false;
        }
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        auxStr = new EnemyMang[enemys.Length];
        for (int i = 0; i < auxStr.Length; i++)
        {
            auxStr[i].enemy = enemys[i];
        }
        Struenemies = StructCopyTo(auxStr, Struenemies);

        //dmgntime = DmgOnButton(Dmg,button);
        for (int i = 0; i < Struenemies.Length; i++)
        {

            if (OnRadiusOf(Struenemies[i].enemy.transform, AtkRadius))
            {
                if (DmgOnButton(Dmg, button) != 0f)
                {
                    if (!Struenemies[i].dmgTook) { Struenemies[i].enemy.GetComponent<EnemyLife>().TakeHit(DmgOnButton(Dmg, button), 0); }
                    Struenemies[i].dmgTook = true;
                }
                else
                {
                    Struenemies[i].dmgTook = false;
                }
            }
        }
    }
    public void Button()
    {
        button = true;
    }
    private float DmgOnButton(float dmg, bool button)
    {
        float Cooldmg;
        if (!button)
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
    private EnemyMang[] StructCopyTo(EnemyMang[] Source, EnemyMang[] Destination)
    {
        //Debug.Log("entrou1");
        EnemyMang[] Aux = new EnemyMang[Source.Length];
        if (Destination.Length > Source.Length)
        {
            Debug.Log("entrou>");
            int indexAdd = 0;
            for (int i = 0; i < Source.Length; i++)
            {
                if (Destination[i + indexAdd].enemy != Source[i].enemy)
                {
                    indexAdd++;
                }
                /*Destination[i].enemy = Source[i].enemy;
                Destination[i].dmgTook=Destination[i+ indexAdd].dmgTook;*/
                Aux[i] = Destination[i + indexAdd];
                Debug.Log(Aux[i].ToString());
            }
            Destination = new EnemyMang[Source.Length];
            Destination = Aux;
        }
        else if (Destination.Length < Source.Length)
        {
            Debug.Log("entrou<");
            Destination.CopyTo(Aux, 0);
            for (int i = 0; i < Source.Length; i++)
            {
                Aux[i].enemy = Source[i].enemy;
            }
            Destination = new EnemyMang[Source.Length];
            Destination = Aux;
        }
        return Destination;

    }
}
