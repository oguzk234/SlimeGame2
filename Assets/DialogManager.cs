using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//BoxColor = new Color(131, 231, 95); //Acik yesil

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [Header("MessageBox")]
    public GameObject MessageBox;
    public SpriteRenderer MessageBoxArea;
    public SpriteRenderer MessageBoxImage;
    public TextMeshProUGUI MessageBoxText;



    [Header("PPSlime")]
    public string PPSlimeName;
    public Sprite PPSlimeSprite;
    public Sprite PPSlimeBoxSprite;
    public Color PPSlimeBoxColor = new Color(131, 2312, 95);
    public Color PPSlimeTextColor;

    [Header("ReadyPersonas")]
    public DialogPersona PPSlime;


    [Header("ReadyDialogues")]
    public List<TalkLine> DDialog1 = new List<TalkLine>();



    private void Awake()
    {
        Instance = this;

        InitializeDialogPersonas();
        InitializeDialogs();
    }
    
    private void InitializeDialogPersonas()
    {
        PPSlime = new DialogPersona(PPSlimeName, PPSlimeSprite, PPSlimeBoxSprite, PPSlimeBoxColor, PPSlimeTextColor);
    }

    private void InitializeDialogs()
    {

    }

    public void ReadTalkSet(List<TalkLine> talkList)
    {

    }

    public void ReadTalkLine(TalkLine talkLine)
    {
        MessageBox.SetActive(true);
        MessageBoxArea.sprite = talkLine.Persona.BoxSprite;       //
        MessageBoxImage.sprite = talkLine.Persona.PersonaSprite;  //BU IKISI HARICI PERSONADAN CEKÝLMÝCEK

        MessageBoxText.text = talkLine.Text;
        MessageBoxText.color = talkLine.TextColor;
        MessageBoxArea.color = talkLine.BoxColor;

        if (talkLine.Direction)
        {
            MessageBoxArea.transform.localScale = Vector3.back;
            MessageBoxImage.transform.localScale = Vector3.back;
        }
        else
        {
            MessageBoxArea.transform.localScale = Vector3.forward;
            MessageBoxImage.transform.localScale = Vector3.forward;
        }
    }


}


public class TalkLine
{
    public DialogPersona Persona;
    public string Text;
    public bool Direction;
    public Color BoxColor;
    public Color TextColor;

    public TalkLine(string text,DialogPersona persona,bool direction = default,Color boxColor = default,Color textColor = default)
    {
        Persona = persona;
        Text = text;
        Direction = direction;

        if (boxColor == default(Color))
            BoxColor = persona.BoxColor;

        if (textColor == default(Color))
            TextColor = persona.TextColor;

    }
}

public class DialogPersona
{
    public string PersonaName; //OPSIYONEL
    public Sprite PersonaSprite;
    public Sprite BoxSprite;
    public Color BoxColor; //USTTEN CEKÝLECEK
    public Color TextColor; //USTTEN CEKÝLECEK

    public DialogPersona(string personaName,Sprite personaSprite,Sprite boxSprite,Color boxColor,Color textColor)
    {
        PersonaName = personaName;
        PersonaSprite = personaSprite;
        BoxSprite = boxSprite;
        BoxColor = boxColor;
        TextColor = textColor;
    }

}

