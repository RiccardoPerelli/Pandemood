using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;

public class Message : MonoBehaviour
{
    public DialogManager DialogManager;
    public List<List<List<DialogData>>> dialogTexts = new List<List<List<DialogData>>>();
    List<List<DialogData>> dialogTextsS = new List<List<DialogData>>();
    public GameObject[] Example;
    public int sceneNum;
    public int dialogueNum;

    private void AddDialogues()
    {
        for (int i = 0; i < 5; i++)
        {
            dialogTexts.Add(new List<List<DialogData>>());
        }
        for (int j = 0; j < 5; j++)
        {
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
        }
        dialogTexts[0][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Here you are! I was waiting for you...", "Lumineo"));
        dialogTexts[0][0].Add(new DialogData("/color:green/You/color:white/: What? Can you talk?", "Gianfilippo"));
        dialogTexts[0][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: If you can talk I can do it too...", "Lumineo"));
        dialogTexts[0][0].Add(new DialogData("/color:green/You/color:white/: Okay, but.. What does it mean? Where am I?", "Gianfilippo"));
        dialogTexts[0][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: No matter how dark it is, the light will show you the way", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: It wasn't that difficult, right?", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:green/You/color:white/: Well... Actually...", "Gianfilippo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Now you've learned that fear is part of you!", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: And remember, the light will always guide you on your way!", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Follow me!", "Lumineo"));
        dialogTexts[1][0].Add(new DialogData("/color:cyan/Friend/color:white/: Hey, you’re finally here, we were waiting for you!", "Amico"));
        dialogTexts[1][0].Add(new DialogData("/color:green/You/color:white/: Ehm, no, I...", "Gianfilippo"));
        dialogTexts[1][0].Add(new DialogData("/color:cyan/Friend/color:white/: Come on! What you're waiting for? Come in and have fun with us!", "Amico"));
        dialogTexts[1][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Sometimes, we’re in such a hurry that we can’t appreciate the little things...", "Lumineo"));
        dialogTexts[1][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Take some time!", "Lumineo"));
        dialogTexts[1][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Then you know how to have fun!", "Lumineo"));
        dialogTexts[1][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Now come on, you’ve got so much to learn.", "Lumineo"));
    
    }

    private void Awake()
    {
        AddDialogues();
        DialogManager.Show(dialogTexts[sceneNum][dialogueNum]);
    }

   
}
