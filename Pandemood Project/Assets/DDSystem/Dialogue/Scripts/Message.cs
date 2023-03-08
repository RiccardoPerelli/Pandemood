using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;
using DDSystem.Script;

public class Message : MonoBehaviour
{
    public DialogManager DialogManager;
    public List<List<List<DialogData>>> dialogTexts = new List<List<List<DialogData>>>();
    List<List<DialogData>> dialogTextsS = new List<List<DialogData>>();
    public GameObject[] Example;
    public int sceneNum;
    public int dialogueNum;
    private bool controller = false;


    private void AddDialogues()
    {
        for (int i = 0; i < 6; i++)
        {
            dialogTexts.Add(new List<List<DialogData>>());
        }
        for (int j = 0; j < 6; j++)
        {
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
            dialogTexts[j].Add(new List<DialogData>());
        }
        
        dialogTexts[0][0].Add(new DialogData("/color:green/You/color:white/: Ouch! Where am I? What does it mean?", "Gianfilippo"));
        dialogTexts[0][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: No matter how dark it is, the light will show you the way.", "Lumineo"));
        dialogTexts[0][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: But remember: nothing lasts forever.", "Lumineo"));

        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: It wasn't that difficult, right?", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:green/You/color:white/: Well... Actually...", "Gianfilippo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Now you've learned that fear is part of you!", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: And remember, the light will always guide you on your way!", "Lumineo"));
        dialogTexts[0][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Let's go back to the outside world now!", "Lumineo"));
        if(controller)
            dialogTexts[0][2].Add(new DialogData("/color:yellow/Lumineo/color:white/: Use /size:up/LB /size:init/to activate the Illumination power, it will let you pass through the shadows.", "Lumineo"));
        else
            dialogTexts[0][2].Add(new DialogData("/color:yellow/Lumineo/color:white/: Use /size:up/A /size:init/to activate the Illumination power, it will let you pass through the shadows.", "Lumineo"));
        dialogTexts[0][2].Add(new DialogData("/color:yellow/Lumineo/color:white/: Check the symbol in the /size:up/upper left corner /size:init/to see how much time you've got left.", "Lumineo")); 


        dialogTexts[1][0].Add(new DialogData("/color:cyan/Friend/color:white/: Hey, you’re finally here, we were waiting for you!", "Amico"));
        dialogTexts[1][0].Add(new DialogData("/color:green/You/color:white/: Ehm, no, I...", "Gianfilippo"));
        dialogTexts[1][0].Add(new DialogData("/color:cyan/Friend/color:white/: Come on! What are you waiting for? Come in and have fun with us!", "Amico"));
        dialogTexts[1][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Sometimes, we’re in such a hurry that we can’t appreciate the little things...", "Lumineo"));
        dialogTexts[1][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Take your time!", "Lumineo"));
        
        dialogTexts[1][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Then you know how to have fun!", "Lumineo"));
        dialogTexts[1][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Now come on, you’ve got so much to learn.", "Lumineo"));
        if(controller)
            dialogTexts[1][2].Add(new DialogData("/color:yellow/Lumineo/color:white/: To interact with an object, press /size:up/X /size:init/.", "Lumineo"));
        else
            dialogTexts[1][2].Add(new DialogData("/color:yellow/Lumineo/color:white/: To interact with an object, press /size:up/E /size:init/.", "Lumineo"));
        if(controller)
            dialogTexts[1][3].Add(new DialogData("/color:yellow/Lumineo/color:white/: Look how fast these platforms move! Use /size:up/RB /size:init/to slow them down.", "Lumineo"));
        else
            dialogTexts[1][3].Add(new DialogData("/color:yellow/Lumineo/color:white/: Look how fast these platforms move! Use /size:up/S /size:init/to slow them down.", "Lumineo"));
        dialogTexts[1][4].Add(new DialogData("/color:yellow/Lumineo/color:white/: This portal allows you to go upstairs, press and keep pressing /size:up/jump/size:init/.", "Lumineo"));
        
        dialogTexts[2][0].Add(new DialogData("/color:green/You/color:white/: Where am I?", "Gianfilippo"));
        dialogTexts[2][0].Add(new DialogData("/color:green/You/color:white/: Is anybody there?!", "Gianfilippo"));
        dialogTexts[2][0].Add(new DialogData("/color:green/You/color:white/: Lumineo?", "Gianfilippo"));
        dialogTexts[2][0].Add(new DialogData("/color:green/You/color:white/: What am I supposed to do now?", "Gianfilippo"));
        
        dialogTexts[2][1].Add(new DialogData("/color:green/You/color:white/: Lumineo! Where were you?", "Gianfilippo"));
        dialogTexts[2][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: I've always been here with you!", "Lumineo"));
        dialogTexts[2][1].Add(new DialogData("/color:green/You/color:white/: But I didn't see you, I felt so alone... so...", "Gianfilippo"));
        dialogTexts[2][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Sad?", "Lumineo"));
        dialogTexts[2][1].Add(new DialogData("/color:green/You/color:white/: Right...", "Gianfilippo"));
        dialogTexts[2][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: See? Your emotions are starting to live inside you again, little by little.", "Lumineo"));
        dialogTexts[2][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Try not to forget how to laugh and how to cry. Life is made up of good times, but also bad times.", "Lumineo"));
        dialogTexts[2][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: Now let's go! It's late!!!", "Lumineo"));
        
        dialogTexts[3][0].Add(new DialogData("/color:red/Mom/color:white/: Hey! Where were you? I've been waiting for you up all night!", "Mamma"));
        dialogTexts[3][0].Add(new DialogData("/color:green/You/color:white/: I... I can explain...", "Gianfilippo"));
        dialogTexts[3][0].Add(new DialogData("/color:red/Mom/color:white/: Well, let's hear!", "Mamma"));
        dialogTexts[3][0].Add(new DialogData("/color:green/You/color:white/: I saw a light and went out, then I fell into the sewers...", "Gianfilippo"));
        dialogTexts[3][0].Add(new DialogData("/color:red/Mom/color:white/: The sewers? You're losing your mind!", "Mamma"));
        dialogTexts[3][0].Add(new DialogData("/color:red/Mom/color:white/: You should've told me! I almost called the police!", "Mamma"));
        dialogTexts[3][0].Add(new DialogData("/color:red/Mom/color:white/: I WAS WORRIED!", "Mamma"));
        dialogTexts[3][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Uh-oh... I think you should run...", "Lumineo"));
        if(controller)
            dialogTexts[3][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: It seems like you're too big to fit through here, use /size:up/B /size:init/to become smaller.", "Lumineo"));
        else
        dialogTexts[3][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: It seems like you're too big to fit through here, use /size:up/D /size:init/to become smaller.", "Lumineo"));

        //Tutorial
        dialogTexts[4][0].Add(new DialogData("/color:yellow/???/color:white/: Here you are! I was waiting for you...", "Lumineo"));
        dialogTexts[4][0].Add(new DialogData("/color:green/You/color:white/: What? You can talk?", "Gianfilippo"));
        dialogTexts[4][0].Add(new DialogData("/color:yellow/???/color:white/: If you can talk, I can do it too...", "Lumineo"));
        dialogTexts[4][0].Add(new DialogData("/color:green/You/color:white/: Okay, but... What are you? Why did you bring me here?", "Gianfilippo"));
        dialogTexts[4][0].Add(new DialogData("/color:yellow/???/color:white/: I am the projection of your lost emotions, or, for friends, Lumineo.", "Lumineo"));
        dialogTexts[4][0].Add(new DialogData("/color:green/You/color:white/: Lost emotions?", "Gianfilippo"));
        dialogTexts[4][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Exactly! All this time you have turned off your emotions, and now that you could return to a normal life, you find excuses not to live.", "Lumineo"));
        dialogTexts[4][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: I am here to help you get through this.", "Lumineo"));
        dialogTexts[4][0].Add(new DialogData("/color:green/You/color:white/: Oh... okay...", "Gianfilippo"));
        dialogTexts[4][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Come on, follow me! I'll show you a few things!", "Lumineo"));
        
        dialogTexts[4][1].Add(new DialogData("/color:yellow/Lumineo/color:white/: This is a corner: to turn, press and hold /size:up/UP /size:init/or /size:up/DOWN/size:init/.", "Lumineo"));
        dialogTexts[4][2].Add(new DialogData("/color:yellow/Lumineo/color:white/: Try to catch me!", "Lumineo"));
        if(controller)
            dialogTexts[4][3].Add(new DialogData("/color:yellow/Lumineo/color:white/: Press /size:up/A /size:init/to jump and reach me!", "Lumineo"));
        else
            dialogTexts[4][3].Add(new DialogData("/color:yellow/Lumineo/color:white/: Press /size:up/SPACE /size:init/to jump and reach me!", "Lumineo"));
        dialogTexts[5][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: So, how was it?", "Lumineo"));
        dialogTexts[5][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: You've finally gained all your emotions back.", "Lumineo"));
        dialogTexts[5][0].Add(new DialogData("/color:yellow/Lumineo/color:white/: Do you think you're ready to go back to your life?", "Lumineo"));
        dialogTexts[5][0].Add(new DialogData("/color:green/You/color:white/: ...", "Gianfilippo"));
        dialogTexts[5][0].Add(new DialogData("/color:green/You/color:white/: I've learned that there can't be courage, without fear...", "Gianfilippo"));
        dialogTexts[5][0].Add(new DialogData("/color:green/You/color:white/: And happiness goes along with a little tear...", "Gianfilippo"));
        dialogTexts[5][0].Add(new DialogData("/color:green/You/color:white/: ...and it's okay to get angry, sometimes, as long as you don't let it control you.", "Gianfilippo"));
        dialogTexts[5][0].Add(new DialogData("/color:green/You/color:white/: But all these emotions color our lives, and we can't live without all of them.", "Gianfilippo"));
        dialogTexts[5][0].Add(new DialogData("/color:green/You/color:white/: Thank you, Lumineo!", "Gianfilippo"));

    }

    private void Awake()
    {
        if (Input.GetJoystickNames().Length > 0) //se joystick
            controller = true;
        else
            controller = false;
        AddDialogues();
        DialogManager.Show(dialogTexts[sceneNum][dialogueNum]);
    }

   
}
