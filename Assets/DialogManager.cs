using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public GameObject MessageBox;
    private void Awake()
    {
        Instance = this;

        AddLines();
    }
    private void AddLines()
    {

    }


    public void ReadTalkLine(TalkLine talkLine)
    {

    }


}


public class TalkLine
{
    public enum DialogPersona { MC }

    public DialogPersona Persona;
    public string Text;
    public bool Direction;
    public Color BoxColor;

    TalkLine(string text,DialogPersona persona,bool direction,Color color = default)
    {
        Persona = persona;
        Text = text;
        Direction = direction;


        if(color == default(Color))
        {
            switch (Persona)
            {
                case DialogPersona.MC:
                    BoxColor = new Color(131, 231, 95); //Acik yesil
                    break;

            }
        }
        else
        {
            BoxColor = color;
        }


    }
}

