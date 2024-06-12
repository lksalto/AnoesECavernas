using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    //vai ser usado para comparar os nomes(para ver se são parte do mesmo dialogo)
    private string cName=" ";

    //Os dialogos
    public Dialogs[] AllDialogs;
    //usado pra armazenar todos os gameobject
    private GameObject[] AllDialogsAux;
    //array que indica se o gameobject em AllDialogsAux é dialog (1) ou subdialogo (0) ///poderia ter sido booleano, mas caso exista mais de um estado possivel deixei em int
    private int[] BinaryDialogStart;
    //quantidade de dialogos diferentes(já considerando os subdialogos)
    private int dialogcount;

    //O struct dos dialogos
    [System.Serializable]
    public struct Dialogs
    {
        //[HideInInspector]
        public string Name;

        public GameObject[] gameObj;
    }


    public int TestDialogId, Conversa;

    // Start is called before the first frame update
    void Start()
    {
        //nmr total de dialogos e subdialogos
        int childCount=transform.GetChild(0).gameObject.transform.childCount;
        BinaryDialogStart = new int[childCount];
        AllDialogsAux= new GameObject[childCount];

        for (int i=0; i<childCount; i++) 
        {
            
            //pega os gameobject
            AllDialogsAux[i] = transform.GetChild(0).GetChild(i).gameObject;

            if(i==0)
            { 
                //Torna cName o nome do primeiro dialogo(deve ser obrigatoriamente um dialogo e n subdialogo)
                cName = AllDialogsAux[i].name;
                BinaryDialogStart[i] = 1; //Indentifica como dialogo
                dialogcount++;
            }
            else if (AllDialogsAux[i].name.Length>= cName.Length && cName == AllDialogsAux[i].name.Substring(0, cName.Length)) //compara se o nome é o mesmo
            {
                BinaryDialogStart[i] = 0;//Indentifica como subdialogo
            }
            else 
            {
                //Muda o cName para o nome do proximo dialogo
                cName = AllDialogsAux[i].name;
                BinaryDialogStart[i] = 1;//Indentifica como dialogo
                dialogcount++;
            }
        }

        //usado para identificar qual subdialogo está
        int subdialogtracker = 1;
        //usado para identificar qual dialogo está
        int dialogtracker = 0;

        //usado para indentificar quantos subdialogos tem cada dialogo (+1 para o dialogo em si)
        int[] subdialogmatrix= new int[dialogcount];

        //faz a cotagem dos subdialogos e armazena na matrix
        for(int i=1; i<childCount; i++) 
        {
            //Se for dialogo
            if (BinaryDialogStart[i] == 1)
            {                                                     //      Nmr de subdialogs em cada dialog
                subdialogmatrix[dialogtracker] = subdialogtracker;//Exemplo: [Dialog1,Dialog2,Dialog3];
                dialogtracker++;                                  //         [   1   ,   5   ,   2   ];
                subdialogtracker = 1;                             
            }
            else 
            {
                subdialogtracker++;
            }
        }
        //o for só armazena o valor quado chega no próximo dialogo, aqui o ultimo dialogo é armazenado
        subdialogmatrix[dialogtracker] = subdialogtracker;

        //inicializa a matriz dos dialogos
        AllDialogs = new Dialogs[dialogcount];

        //reinicia os trackers
        dialogtracker = 0;
        subdialogtracker = 0;

        //inicializa a matriz de gameobjects dentro do primeiro dialogo
        AllDialogs[0].gameObj = new GameObject[subdialogmatrix[0]];
        //armazena o primeiro gameobject de dialogo
        AllDialogs[0].gameObj[0] = AllDialogsAux[0];
        //armazena o nome do dialogo
        AllDialogs[0].Name = AllDialogs[0].gameObj[0].name;



        for (int i=1; i<childCount; i++) 
        { 
            if (BinaryDialogStart[i]==1)
            {
                //proximo dialogo
                dialogtracker++;
                //inicializa a matriz de gameobjects dentro do atual dialogo
                AllDialogs[dialogtracker].gameObj = new GameObject[subdialogmatrix[dialogtracker] ];
                AllDialogs[dialogtracker].gameObj[0] = AllDialogsAux[i];
                AllDialogs[dialogtracker].Name = AllDialogs[dialogtracker].gameObj[0].name;
                //reinicia a contagem de subdialogos
                subdialogtracker = 0;
            }
            else
            {
                subdialogtracker++;
                //armazena os subdialogos
                AllDialogs[dialogtracker].gameObj[subdialogtracker] = AllDialogsAux[i];
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {DialogStart(TestDialogId);}
        //if (Input.GetKeyDown(KeyCode.Alpha1)) { TestDialogId = 0; }
        //if (Input.GetKeyDown(KeyCode.Alpha2)) { TestDialogId = 1; }
        //if (Input.GetKeyDown(KeyCode.Alpha3)) { TestDialogId = 2; }
        if (Input.GetKeyDown(KeyCode.Space)) { DialogEnd(Conversa); }
    }
    public int DialogIdByName(string name) 
    {
        int DialogId = 0;
        for (int i = 0; i < AllDialogs.Length; i++)
        {
            if (AllDialogs[i].Name == name)
            {
                Debug.Log("Id: "+i.ToString());
                DialogId = i;
                break;
            }
        }
        return DialogId;
    }
    public void DialogStart(int DialogId) 
    {
        if (Conversa == -1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            AllDialogs[DialogId].gameObj[0].SetActive(true);
            Conversa = DialogId;
        }
    }
    public void DialogEnd(int DialogId)
    {
        if (DialogId != -1)
        {
            int DialogSize = AllDialogs[DialogId].gameObj.Length;
            int Ativo = -1;
            for (int i = 0; i < AllDialogs[DialogId].gameObj.Length; i++)
            {
                if (AllDialogs[DialogId].gameObj[i].activeInHierarchy)
                {
                    Ativo = i;
                    break;
                }
            }
            if (Ativo != -1)
            {
                AllDialogs[DialogId].gameObj[Ativo].SetActive(false);

                if (DialogSize > Ativo + 1)
                {
                    AllDialogs[DialogId].gameObj[Ativo + 1].SetActive(true);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    Conversa = -1;
                }
            }
        }
    }
    public void DialogChangeId(int DialogId) 
    {
        TestDialogId=DialogId;
    }
    
}
