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
    public TMP_FontAsset PPSlimeFontAsset;

    [Header("PPWife")]
    public string PPWifeName;
    public Sprite PPWifeSprite;
    public Sprite PPWifeBoxSprite;
    public Color PPWifeBoxColor = new Color(131, 2312, 95);
    public Color PPWifeTextColor;
    public TMP_FontAsset PPWifeFontAsset;

    [Header("PPMole")]
    public string PPMoleName;
    public Sprite PPMoleSprite;
    public Sprite PPMoleBoxSprite;
    public Color PPMoleBoxColor = new Color(131, 2312, 95);
    public Color PPMoleTextColor;
    public TMP_FontAsset PPMoleFontAsset;

    [Header("ReadyPersonas")]
    public DialogPersona PPSlime;
    public DialogPersona PPWife;
    public DialogPersona PPMole;


    [Header("ReadyDialogues")]
    public List<TalkLine> DDialog1 = new List<TalkLine>();
    public List<TalkLine> DDialog2 = new List<TalkLine>();



    private void Awake()
    {
        Instance = this;

        InitializeDialogPersonas();
        InitializeDialogs();
    }

    private void Update()
    {
        DialogDebug();
    }
    private void DialogDebug()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ReadTalkSet(DDialog1);
            foreach(TalkLine talkLine in DDialog1)
            {
                print(talkLine.Text);
            }
        }
    }

    private void InitializeDialogPersonas()
    {
        PPSlime = new DialogPersona(PPSlimeName, PPSlimeSprite, PPSlimeBoxSprite, PPSlimeBoxColor, PPSlimeTextColor,PPSlimeFontAsset);
        PPWife = new DialogPersona(PPWifeName, PPWifeSprite, PPWifeBoxSprite, PPWifeBoxColor, PPWifeTextColor, PPWifeFontAsset);
        PPMole = new DialogPersona(PPMoleName, PPMoleSprite, PPMoleBoxSprite, PPMoleBoxColor, PPMoleTextColor, PPMoleFontAsset);
    }

    private void InitializeDialogs()
    {
        DDialog1.Add(new TalkLine("Merhaba!", PPSlime));
        DDialog1.Add(new TalkLine("Nasilsin?", PPSlime));
        DDialog1.Add(new TalkLine("Iyiyim", PPWife));
        DDialog1.Add(new TalkLine("Iyiyim TERS", PPWife,true));
        DDialog1.Add(new TalkLine("ZOOOORT?", PPSlime));
        DDialog1.Add(new TalkLine("Gorusmek uzere!", PPSlime));
        DDialog1.Add(new TalkLine("zortirizort", PPMole,true));
        DDialog1.Add(new TalkLine("H-h-h-hello", PPMole));
    }






    public void ReadTalkSet(List<TalkLine> talkList)
    {
        StartCoroutine(ReadTalkSetCoroutine(talkList));
    }
    IEnumerator ReadTalkSetCoroutine(List<TalkLine> talkList)
    {
        for (int i = 0; i < talkList.Count; i++)
        {
            ReadTalkLine(talkList[i]);
            yield return new WaitForSeconds(0.01f);  //OLMAZSA DÝYALOG ATLIYO
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        MessageBox.SetActive(false);
    }




    public void ReadTalkLine(TalkLine talkLine)
    {
        MessageBox.SetActive(true);
        MessageBoxArea.sprite = talkLine.Persona.BoxSprite;       //
        MessageBoxImage.sprite = talkLine.Persona.PersonaSprite;  //BU IKISI HARICI PERSONADAN CEKÝLMÝCEK

        MessageBoxText.text = talkLine.Text;
        MessageBoxText.color = talkLine.TextColor;
        MessageBoxArea.color = talkLine.BoxColor;
        MessageBoxText.font = talkLine.FontAsset;

        if (talkLine.Direction)
        {
            MessageBoxArea.GetComponent<SpriteRenderer>().flipX = true;
            MessageBoxImage.GetComponent<SpriteRenderer>().flipX = true;
            MessageBoxText.GetComponent<RectTransform>().pivot = new Vector2(0.75f, 0.5f);
        }
        else
        {
            MessageBoxArea.GetComponent<SpriteRenderer>().flipX = false;
            MessageBoxImage.GetComponent<SpriteRenderer>().flipX = false;
            MessageBoxText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
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
    public TMP_FontAsset FontAsset;

    public TalkLine(string text,DialogPersona persona,bool direction = default,Color boxColor = default,Color textColor = default,TMP_FontAsset fontAsset = null)
    {
        Persona = persona;
        Text = text;
        Direction = direction;

        if (boxColor == default(Color))
            BoxColor = persona.BoxColor;
        else BoxColor = boxColor;


        if (textColor == default(Color))
            TextColor = persona.TextColor;
        else TextColor = textColor;



        if (fontAsset == null)
            FontAsset = persona.FontAsset;
        else FontAsset = fontAsset;

    }
}

public class DialogPersona
{
    public string PersonaName; //OPSIYONEL
    public Sprite PersonaSprite;
    public Sprite BoxSprite;
    public Color BoxColor; //USTTEN CEKÝLECEK
    public Color TextColor; //USTTEN CEKÝLECEK
    public TMP_FontAsset FontAsset;

    public DialogPersona(string personaName,Sprite personaSprite,Sprite boxSprite,Color boxColor,Color textColor,TMP_FontAsset fontAsset)
    {
        PersonaName = personaName;
        PersonaSprite = personaSprite;
        BoxSprite = boxSprite;
        BoxColor = boxColor;
        TextColor = textColor;
        FontAsset = fontAsset;
    }

}

