using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EditorEnemy : MonoBehaviour
{
    public bool Prefabs;
    public GameObject[] enemys;
    public int id;
    private int idaux;
    //vida
    public float life = 1;
    public float magicResist = 0.5f;
    public float initialSpeed;
    public float speed = 10f;
    public int dmg = 1;

    private EnemyLife stats;
    public GameObject EnemyEditor;
    // Start is called before the first frame update
    void Start()
    {
        EnemyEditor = GameObject.FindGameObjectWithTag("EnemyEditor");
        if (!Prefabs)
        {
            for (int i = 0, j = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).GetComponent<EnemyLife>() != null)
                {
                    enemys[j] = transform.parent.GetChild(i).gameObject;
                    j++;
                    if (j > enemys.Length)
                    {
                        GameObject[] temp = enemys;
                        enemys = new GameObject[j];
                        temp.CopyTo(enemys, 0);
                    }
                }
            }
        }
        idaux = id;
        GetStats(enemys[id].GetComponent<EnemyLife>());
        InputFieldSetInitial();
        EnemyEditor.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (id >= 0 && id < enemys.Length) { stats = enemys[id].GetComponent<EnemyLife>(); }

        if (idaux != id) 
        {
            GetStats(stats);
            InputFieldSetInitial();
            idaux=id;
        }
        else
        {
            InputFieldStats();
            SetStats(stats);
        }

        if (Input.GetKeyDown(KeyCode.U)) 
        {
            SpawnEnemy(id);
        }
    }
    public void SpawnEnemy(int id) 
    {
       GameObject inst= Instantiate(enemys[id]) as GameObject;
        inst.SetActive(true);
    }
    public void GetStats(EnemyLife stats) 
    {
        life = stats.life;
        magicResist = stats.magicResist;
        initialSpeed = stats.initialSpeed;
        speed = stats.speed;
        dmg = stats.dmg;
    }
    public void SetStats(EnemyLife stats) 
    {
        stats.life=life;
        stats.magicResist= magicResist;
        stats.initialSpeed = initialSpeed;
        stats.speed=speed;
        stats.dmg=dmg;
    }
    public void InputFieldSetInitial()
    {
        EnemyEditor.transform.GetChild(0).GetComponent<TMP_InputField>().text = id.ToString();
        EnemyEditor.transform.GetChild(1).GetComponent<TMP_InputField>().text = life.ToString();
        EnemyEditor.transform.GetChild(2).GetComponent<TMP_InputField>().text = magicResist.ToString();
        EnemyEditor.transform.GetChild(3).GetComponent<TMP_InputField>().text = initialSpeed.ToString();
        EnemyEditor.transform.GetChild(4).GetComponent<TMP_InputField>().text = speed.ToString();
        EnemyEditor.transform.GetChild(5).GetComponent<TMP_InputField>().text = dmg.ToString();

    }
    public void InputFieldStats() 
    {
        id= int.Parse(EnemyEditor.transform.GetChild(0).GetComponent<TMP_InputField>().text);
        life = float.Parse(EnemyEditor.transform.GetChild(1).GetComponent<TMP_InputField>().text);
        magicResist = float.Parse(EnemyEditor.transform.GetChild(2).GetComponent<TMP_InputField>().text);
        initialSpeed = float.Parse(EnemyEditor.transform.GetChild(3).GetComponent<TMP_InputField>().text);
        speed = float.Parse(EnemyEditor.transform.GetChild(4).GetComponent<TMP_InputField>().text);
        dmg = int.Parse(EnemyEditor.transform.GetChild(5).GetComponent<TMP_InputField>().text);
    }
}
