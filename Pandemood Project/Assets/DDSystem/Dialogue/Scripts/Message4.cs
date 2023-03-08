using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class Message4 : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: Then you know how to have fun!", "Lumineo"));

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: Now come on, you’ve got so much to learn.", "Lumineo"));

        DialogManager.Show(dialogTexts);
    }


}