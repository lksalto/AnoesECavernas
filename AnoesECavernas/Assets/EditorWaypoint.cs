using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorWaypoint : MonoBehaviour
{
    public bool PathStart;
    public GameObject CursorEditor;
    public bool clicou;
    public int NextPathCount=0;
    // Start is called before the first frame update
    void Start()
    {
        if (CursorEditor == null) 
        {
            CursorEditor = GameObject.FindGameObjectWithTag("Cursor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor em cima DESTE objeto
        if (CursorEditor.transform.position == transform.position) 
        {
            //Clicou com o esquerdo
            if (Input.GetMouseButtonDown(0))
            {
                //Se o cursor n estiver "carregando" um objeto
                if (CursorEditor.GetComponent<CursorEditor>().PointClicked == null)
                {
                    //Ele começa a carregar ESTE objeto
                    CursorEditor.GetComponent<CursorEditor>().PointClicked = transform.gameObject;
                    //Se ESTE objeto for um path
                    if (transform.tag=="Path" || transform.tag == "MiddlePath") 
                    {
                        //Seta que o caminho não é um fim
                        GetComponent<WaypointChanger>().end = false; 
                    }
                }

                //Se um objeto estiver sendo carregado
                else 
                {
                    //Se ESTE objeto for um path
                    if (transform.tag == "Path"|| transform.tag == "MiddlePath")
                    {
                        //Se o objeto carregado for um path
                        if (CursorEditor.GetComponent<CursorEditor>().PointClicked.tag=="Path"|| CursorEditor.GetComponent<CursorEditor>().PointClicked.tag == "MiddlePath") 
                        {
                            //Se o objeto carregado não for ESTE objeto
                            if (CursorEditor.GetComponent<CursorEditor>().PointClicked != transform.gameObject)
                            {
                                //Pega a contagem do objeto CARREGADO;
                                int CarregadoNextPathCount = CursorEditor.GetComponent<CursorEditor>().PointClicked.GetComponent<EditorWaypoint>().NextPathCount;
                                //Seta ESTE objeto como nextpath do objeto carregado
                                CursorEditor.GetComponent<CursorEditor>().PointClicked.GetComponent<WaypointChanger>().nextpath[CarregadoNextPathCount] = transform.gameObject;
                            }
                            //Se o objeto carregado for ESTE objeto tambem
                            else 
                            {
                                //Seta path como fim
                                GetComponent<WaypointChanger>().end = true;
                            }
                        }
                        //Se o objeto carregado não for um path
                        else 
                        {
                            //Seta ESTE objeto como parente do objeto carregado
                            CursorEditor.GetComponent<CursorEditor>().PointClicked.transform.SetParent(transform,true);
                        }
                        //Seta que nenhum objeto será carregado
                        CursorEditor.GetComponent<CursorEditor>().PointClicked = null;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1)) 
            {
                //Se for um caminho
                if (transform.tag == "Path"|| transform.tag == "MiddlePath")
                {
                    //Seta que o caminho não é um fim
                    GetComponent<WaypointChanger>().end = false;

                    //Se não tiver caminhos nulos
                    if (GetComponent<WaypointChanger>().nextpath[NextPathCount] != null)
                    {
                        //Aumenta em 1 a quantidade de proximos caminhos
                        GameObject[] temp = new GameObject[GetComponent<WaypointChanger>().nextpath.Length + 1];
                        GetComponent<WaypointChanger>().nextpath.CopyTo(temp, 0);
                        GetComponent<WaypointChanger>().nextpath = temp;
                        //O ultimo caminho que será contado
                        NextPathCount++;
                    }


                }
            }
        }
    }

}

