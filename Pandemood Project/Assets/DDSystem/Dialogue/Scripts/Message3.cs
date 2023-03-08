using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class Message3 : MonoBehaviour
{
    public DialogManager DialogManager;

    public GameObject[] Example;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("/color:cyan/Friend/color:white/: Hey, you’re finally here, we were waiting for you!", "Amico"));

        dialogTexts.Add(new DialogData("/color:green/You/color:white/: Ehm, no, I...", "Gianfilippo"));
        
        dialogTexts.Add(new DialogData("/color:cyan/Friend/color:white/: Come on! What you're waiting for? Come in and have fun with us!", "Amico"));

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: Sometimes, we’re in such a hurry that we can’t appreciate the little things...", "Lumineo"));

        dialogTexts.Add(new DialogData("/color:yellow/Lumineo/color:white/: Take some time!", "Lumineo"));

        DialogManager.Show(dialogTexts);
    }


}