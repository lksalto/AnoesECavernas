using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLines : MonoBehaviour
{
    public GameObject allpath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cada Path
        for(int i = 0; i < allpath.transform.childCount; i++) 
        {
            //Cada Ponto do path
            for(int j = 0; j < allpath.transform.GetChild(i).childCount; j++) 
            {
                //Se não for o ultimo
                if(j != allpath.transform.GetChild(i).childCount - 1) 
                {
                    //Cria uma linha(a partir de um gameobject filho do waypoint) do ponto atual até o proximo
                    allpath.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<LineRenderer>().SetPosition(0, allpath.transform.GetChild(i).GetChild(j).position);
                    allpath.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<LineRenderer>().SetPosition(1, allpath.transform.GetChild(i).GetChild(j+1).position);
                }
                //Se for o ultimo
                else 
                {
                    //Se o caminho tiver bifurcações(Compara se a quantidade de filhos do ultimo elemento é menor que a quantidade de proximos caminhos)
                    if (allpath.transform.GetChild(i).GetComponent<WaypointChanger>().nextpath.Length > allpath.transform.GetChild(i).GetChild(j).transform.childCount) 
                    {
                        for (int n=1; n < allpath.transform.GetChild(i).GetComponent<WaypointChanger>().nextpath.Length; n++) 
                        {
                            //Se o caminho n estiver nulo
                            if (allpath.transform.GetChild(i).GetComponent<WaypointChanger>().nextpath[n] != null)
                            {
                                //Cria um lineRender
                                Instantiate(allpath.transform.GetChild(i).GetChild(j).GetChild(0), allpath.transform.GetChild(i).GetChild(j));

                                //Cria uma linha(a partir de algum gameobject filho do waypoint) do ponto atual até o proximo CAMINHO
                                allpath.transform.GetChild(i).GetChild(j).GetChild(n).GetComponent<LineRenderer>().SetPosition(0, allpath.transform.GetChild(i).GetChild(j).position);
                                GameObject FirstInNextPath = allpath.transform.GetChild(i).GetComponent<WaypointChanger>().nextpath[n];
                                allpath.transform.GetChild(i).GetChild(j).GetChild(n).GetComponent<LineRenderer>().SetPosition(1, FirstInNextPath.transform.position);
                            }
                        }
                    
                        //Cria uma linha(a partir de um gameobject filho do waypoint) do ponto atual até o proximo CAMINHO
                        allpath.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<LineRenderer>().SetPosition(0, allpath.transform.GetChild(i).GetChild(j).position); 
                        GameObject FirstInFirstNextPath = allpath.transform.GetChild(i).GetComponent<WaypointChanger>().nextpath[0];
                        allpath.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<LineRenderer>().SetPosition(1, FirstInFirstNextPath.transform.position);
                    }
                    //Se o caminho não tiver bifurcações
                    else 
                    {
                        //Cria uma linha(a partir de um gameobject filho do waypoint) do ponto atual até o proximo CAMINHO
                        allpath.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<LineRenderer>().SetPosition(0, allpath.transform.GetChild(i).GetChild(j).position);
                        GameObject FirstInFirstNextPath = allpath.transform.GetChild(i).GetComponent<WaypointChanger>().nextpath[0];
                        allpath.transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<LineRenderer>().SetPosition(1, FirstInFirstNextPath.transform.position);
                    }
                }
            }
        }
    }
}
