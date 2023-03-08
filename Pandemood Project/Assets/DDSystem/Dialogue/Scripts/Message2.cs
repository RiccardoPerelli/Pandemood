using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class Message2 : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: It wasn't that difficult, right?", "Lumineo"));

        dialogTexts.Add(new DialogData("/color:green/You/color:white/: Well... Actually...", "Gianfilippo"));
        
        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: Now you've learned that fear is part of you!", "Lumineo"));

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: And remember, the light will always guide you on your way!", "Lumineo"));

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: Follow me!", "Lumineo"));

        DialogManager.Show(dialogTexts);
    }


}
