using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AiMove : MonoBehaviour
{
    private AIPath path;
    private AIDestinationSetter destination;
    public float Speed=1;
    public GameObject cursor, clickedPos,HeroToGoPrefab;
    private GameObject HeroManager;
    public float CursorRadius,DestinationRadius;
    public bool selecionado,clicado,reached;
    public Vector3 InitalPos,LastPos;
    public GameObject ShadowSelected;
    private Color shadowcolor;
    // Start is called before the first frame update
    void Awake()
    {
        path = GetComponent<AIPath>();
        destination = GetComponent<AIDestinationSetter>();
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        HeroManager = GameObject.FindGameObjectWithTag("HeroManager");
        InitalPos = transform.position;
        ShadowSelected = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = Speed;

        
        ShadowSelected.GetComponent<SpriteRenderer>().color= shadowcolor;

        if (OnRadiusOf(cursor.transform, CursorRadius))
        {
            shadowcolor = Color.red;
            if (Input.GetMouseButtonDown(0))
            {
                //selecionado = true;

            }
            else if (Input.GetMouseButtonDown(1))
            {
                selecionado = !selecionado;
                if (!selecionado)
                {
                    HeroManager.GetComponent<HeroManager>().DeSelectHero(transform.gameObject);
                }
                else
                {
                    HeroManager.GetComponent<HeroManager>().AddHero(transform.gameObject);
                    if (!clicado)
                    {
                        clicado = true;
                        GameObject point = Instantiate(HeroToGoPrefab, HeroManager.transform) as GameObject;
                        point.name = transform.name + "PointToGo";
                        clickedPos = point;
                    }
                }
            }
        }
        else if (selecionado) 
        {
            shadowcolor = Color.green;
            if (Input.GetMouseButtonDown(0) && cursor.GetComponent<Cursor>().caminhoBool)
            {
                clickedPos.transform.position=cursor.transform.position ;
                reached=false;
                destination.enabled = true;
                path.enabled = true;
                destination.target=clickedPos.transform;
            }
        }
        else 
        {
            shadowcolor = new Color(0.1603774f, 0.1603774f, 0.1603774f);
        }
        if (clickedPos!=null && OnRadiusOf(clickedPos.transform, DestinationRadius)) 
        {
            Reached();
        }
    }
    public void ReturnToInitialPos() 
    {
        transform.position = InitalPos;
    }
    public void ReturnToLastPos()
    {
        transform.position = LastPos;
    }
    public void Reached() 
    {
        destination.enabled = false;
        path.enabled = false;
        reached = true;
    }
    private bool OnRadiusOf(Transform gamobj,float radius) 
    {
        if(Vector3.Distance(gamobj.position,transform.position)<radius)
        {
            return true;
        }
        else 
        {
            return false; 
        }
    }
    private bool OnSquareOf(Transform gamobj, float radius)
    {
        if (transform.position.x < gamobj.position.x + radius
           && transform.position.x > gamobj.position.x - radius
           && transform.position.y > gamobj.position.y - radius
           && transform.position.y < gamobj.position.y + radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
