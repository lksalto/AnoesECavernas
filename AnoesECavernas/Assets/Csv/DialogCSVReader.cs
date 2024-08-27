using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class DialogCSVReader : MonoBehaviour
{
    
    public TextAsset csv;//csv a ser lido
    public GameObject DialogParent,NormalTemplate,ButtonTemplate;//O parent de dialogo, e os templates
    private string nome, Texto, ImageName;//nome do dialog;texto do dialog;nome da imagem no dialog
    private int ButtonCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //quando clica x
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            //pega cada linha do csv
            string[] line = csv.text.Split(new string[] {"\n" },System.StringSplitOptions.None);
            //para cada linha do csv
            for(int i = 0; i < line.Length; i++) 
            {
                //pega cada elemento da linha
                string[] element = line[i].Split(new string[] { ";" }, System.StringSplitOptions.None);

                //Tira qualquer caracter invalido do ultimo elemento da linha
                element[element.Length - 1] = element[element.Length - 1].Split(Path.GetInvalidFileNameChars())[0];

                //se for um nome
                if (element[0] == "Nome") 
                {
                    //pega o nome do csv
                    nome = element[1];
                }
                //Se for um texto
                else if (element[0] == "Txt") 
                {
                    ButtonCount = 0;
                    //pega o texto
                    Texto = element[1];
                    //pega o nome da imagem que vai usar
                    ImageName = element[2];
                    //Tira qualquer caracter invalido do nome da imagem
                    ImageName = ImageName.Split(Path.GetInvalidFileNameChars())[0] + ".png";
                    //cria um path até a imagem
                    string path = Path.Combine("Assets", "Csv","DialogImages",ImageName);


                    //cria um dialog ou subdialog
                    Instantiate(NormalTemplate, DialogParent.transform);
                    //encontra o objeto pegando o ultimo filho do dialogparet
                    GameObject Instanciado = DialogParent.transform.GetChild(DialogParent.transform.childCount - 1).gameObject;
                    //muda o nome
                    Instanciado.name = nome;
                    //muda o texto
                    Instanciado.GetComponent<TextMeshProUGUI>().text = Texto;
                    //pega a imagem a partir do path
                    //tava dando erro//Texture2D sprite = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
                    Texture2D sprite=Instanciado.GetComponent<Texture2D>();//inventei isso aqui só pra n dar problema, é uma cagada sem igual
                    //muda a imagem(e transforma Texture@d em sprite
                    Instanciado.transform.GetChild(0).GetComponent<Image>().sprite = ToSprite(sprite);
                }
                else if (element[0] == "AddButton") 
                {
                    ButtonCount++;
                    //encontra o objeto pegando o ultimo filho do dialogparet
                    GameObject Instanciado = DialogParent.transform.GetChild(DialogParent.transform.childCount - 1).gameObject;
                    Instanciado.AddComponent<KeepButtons>();
                    Instantiate(ButtonTemplate, Instanciado.transform);
                    GameObject Button = Instanciado.transform.GetChild(Instanciado.transform.childCount -1).gameObject;
                    Button.GetComponent<DialogButton>().FunctionName=element[1];
                    Button.GetComponent<DialogButton>().String=element[2];
                    Button.GetComponent<DialogButton>().DialogMain=DialogParent.transform.parent.gameObject;

                }
            }
            //Ativa o dialog
            DialogParent.transform.parent.GetComponent<Dialog>().enabled=true;

        }
    }
    Sprite ToSprite(Texture2D tex) 
    {
        Rect rec = new Rect(0, 0, tex.width, tex.height);
        Sprite spirit = Sprite.Create(tex, rec, new Vector2(0, 0), 1);
        return spirit;
    }
}
