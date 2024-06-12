using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public string FunctionName;
    public GameObject DialogMain;
    public bool Bool;
    public float FLoat;
    public int Int;
    public string String;

    public void DialogSetFunc()
    {
        Dialog Dialog=DialogMain.GetComponent<Dialog>();
        if (FunctionName== "StartDialog") 
        {
            Dialog.DialogEnd(Dialog.Conversa);
            int Id=Dialog.DialogIdByName(String);
            Debug.Log(Dialog.DialogIdByName(String).ToString());
            Dialog.DialogStart(Id);
        }
        else if (FunctionName == "EndDialog")
        {
            if (String == "Self") 
            { 
                Dialog.DialogEnd(Dialog.Conversa); 
            }
            else 
            { 
                Dialog.DialogEnd(Dialog.DialogIdByName(String)); 
            }
        }
    }
}
