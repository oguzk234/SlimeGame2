using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//BoxColor = new Color(131, 231, 95); //Acik yesil

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public bool isOnDialogue;

    [Header("MessageBox")]
    public GameObject MessageBox;
    public Image MessageBoxArea;  // 24 e 13.5 default boyutu
    public Image MessageBoxImage;
    public Image MessageBoxExtraImage;
    public TextMeshProUGUI MessageBoxText;
    public float DefaultReadingTime;



    [Header("PPSlime")]
    public string PPSlimeName;
    public Sprite PPSlimeSprite;
    public Sprite PPSlimeBoxSprite;
    public Sprite PPSlimeBoxExtraSprite;
    public Color PPSlimeBoxColor = new Color(131, 2312, 95);
    public Color PPSlimeTextColor;
    public TMP_FontAsset PPSlimeFontAsset;

    [Header("PPWife")]
    public string PPWifeName;
    public Sprite PPWifeSprite;
    public Sprite PPWifeBoxSprite;
    public Sprite PPWifeBoxExtraSprite;
    public Color PPWifeBoxColor = new Color(131, 2312, 95);
    public Color PPWifeTextColor;
    public TMP_FontAsset PPWifeFontAsset;

    [Header("PPMole")]
    public string PPMoleName;
    public Sprite PPMoleSprite;
    public Sprite PPMoleBoxSprite;
    public Sprite PPMoleBoxExtraSprite;
    public Color PPMoleBoxColor = new Color(131, 2312, 95);
    public Color PPMoleTextColor;
    public TMP_FontAsset PPMoleFontAsset;

    [Header("PPSign")]
    public string PPSignName;
    public Sprite PPSignSprite;
    public Sprite PPSignBoxSprite;
    public Sprite PPSignBoxExtraSprite;
    public Color PPSignBoxColor;
    public Color PPSignTextColor;
    public TMP_FontAsset PPSignFontAsset;

    [Header("PPMoustacheSlime")]
    public string PPMoustacheSlimeName;
    public Sprite PPMoustacheSlimeSprite;
    public Sprite PPMoustacheSlimeBoxSprite;
    public Sprite PPMoustacheSlimeBoxExtraSprite;
    public Color PPMoustacheSlimeBoxColor;
    public Color PPMoustacheSlimeTextColor;
    public TMP_FontAsset PPMoustacheSlimeFontAsset;

    [Header("PPLambSlime")]
    public string PPLambSlimeName;
    public Sprite PPLambSlimeSprite;
    public Sprite PPLambSlimeBoxSprite;
    public Sprite PPLambSlimeBoxExtraSprite;
    public Color PPLambSlimeBoxColor;
    public Color PPLambSlimeTextColor;
    public TMP_FontAsset PPLambSlimeFontAsset;

    [Header("PPHappySlime")]
    public string PPHappySlimeName;
    public Sprite PPHappySlimeSprite;
    public Sprite PPHappySlimeBoxSprite;
    public Sprite PPHappySlimeBoxExtraSprite;
    public Color PPHappySlimeBoxColor;
    public Color PPHappySlimeTextColor;
    public TMP_FontAsset PPHappySlimeFontAsset;

    [Header("PPGutsSlime")]
    public string PPGutsSlimeName;
    public Sprite PPGutsSlimeSprite;
    public Sprite PPGutsSlimeBoxSprite;
    public Sprite PPGutsSlimeBoxExtraSprite;
    public Color PPGutsSlimeBoxColor;
    public Color PPGutsSlimeTextColor;
    public TMP_FontAsset PPGutsSlimeFontAsset;


    [Header("ReadyPersonas")]
    public List<DialogPersona> PersonaList = new List<DialogPersona>();
    public enum Persona { PPSlime, PPWife, PPMole, PPSign,PPMoustacheSlime,PPLambSlime,PPHappySlime,PPGutsSlime }

    public DialogPersona PPSlime;
    public DialogPersona PPWife;
    public DialogPersona PPMole;
    public DialogPersona PPSign;
    public DialogPersona PPMoustacheSlime;
    public DialogPersona PPLambSlime;
    public DialogPersona PPHappySlime;
    public DialogPersona PPGutsSlime;


    [Header("ReadyDialogues")]
    public List<TalkLine> DDialog1 = new List<TalkLine>();
    public List<TalkLine> DDialog2 = new List<TalkLine>();

    [Header("ReadyTalkBoxTextDatas")]
    public DialogPersona.TalkBoxTextData DefaultTalkBoxTextData = new DialogPersona.TalkBoxTextData(1150,231,1200,320);
    public DialogPersona.TalkBoxTextData MiddleTalkBoxTextData = new DialogPersona.TalkBoxTextData(960, 277, 1370, 320);


    private void Awake()
    {
        Instance = this;

        InitializeDialogPersonas();
        InitializeDialogs();
    }

    private void Update()
    {
        //DialogDebug();
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
        PPSlime = new DialogPersona(PPSlimeName, PPSlimeSprite, PPSlimeBoxSprite, PPSlimeBoxExtraSprite, PPSlimeBoxColor, PPSlimeTextColor, PPSlimeFontAsset, DefaultTalkBoxTextData);
        PPWife = new DialogPersona(PPWifeName, PPWifeSprite, PPWifeBoxSprite, PPWifeBoxExtraSprite, PPWifeBoxColor, PPWifeTextColor, PPWifeFontAsset, DefaultTalkBoxTextData);
        PPMole = new DialogPersona(PPMoleName, PPMoleSprite, PPMoleBoxSprite, PPMoleBoxExtraSprite, PPMoleBoxColor, PPMoleTextColor, PPMoleFontAsset, DefaultTalkBoxTextData);
        PPSign = new DialogPersona(PPSignName, PPSignSprite, PPSignBoxSprite, PPSignBoxExtraSprite, PPSignBoxColor, PPSignTextColor, PPSignFontAsset, MiddleTalkBoxTextData);
        PPMoustacheSlime = new DialogPersona(PPMoustacheSlimeName, PPMoustacheSlimeSprite, PPMoustacheSlimeBoxSprite, PPMoustacheSlimeBoxExtraSprite, PPMoustacheSlimeBoxColor, PPMoustacheSlimeTextColor, PPMoustacheSlimeFontAsset, DefaultTalkBoxTextData);
        PPLambSlime = new DialogPersona(PPLambSlimeName, PPLambSlimeSprite, PPLambSlimeBoxSprite, PPLambSlimeBoxExtraSprite, PPLambSlimeBoxColor, PPLambSlimeTextColor, PPLambSlimeFontAsset, DefaultTalkBoxTextData);
        PPHappySlime = new DialogPersona(PPHappySlimeName, PPHappySlimeSprite, PPHappySlimeBoxSprite, PPHappySlimeBoxExtraSprite, PPHappySlimeBoxColor, PPHappySlimeTextColor, PPHappySlimeFontAsset, DefaultTalkBoxTextData);
        PPGutsSlime = new DialogPersona(PPGutsSlimeName, PPGutsSlimeSprite, PPGutsSlimeBoxSprite, PPGutsSlimeBoxExtraSprite, PPGutsSlimeBoxColor, PPGutsSlimeTextColor, PPGutsSlimeFontAsset, DefaultTalkBoxTextData);

    }

    private void InitializeDialogs()
    {
        DDialog1.Add(new TalkLine("Nasilsin?", PPSlime));
        DDialog1.Add(new TalkLine("Iyiyim", PPWife));
        DDialog1.Add(new TalkLine("Iyiyim TERS", PPWife,true));
        DDialog1.Add(new TalkLine("zortirizort", PPMole,true));
        DDialog1.Add(new TalkLine("E", PPMole));
        DDialog1.Add(new TalkLine("AJAJAJ", PPLambSlime));
        DDialog1.Add(new TalkLine("ZIBAPWIJP", PPHappySlime));
        DDialog1.Add(new TalkLine("ZIBAPWIJP", PPMoustacheSlime));
        DDialog1.Add(new TalkLine("ZIBAPWIJP", PPGutsSlime));
    }






    public void ReadTalkSet(List<TalkLine> talkList)  //BUNA BASKA SEYLER EKLERSEN SICARSIN "F1D" DA
    {
        StartCoroutine(ReadTalkSetCoroutine(talkList));
    }
    public IEnumerator ReadTalkSetCoroutine(List<TalkLine> talkList)
    {
        isOnDialogue = true;
        PlayerMove.Instance.isMovable = false;

        for (int i = 0; i < talkList.Count; i++)
        {
            ReadTalkLine(talkList[i]);
            yield return new WaitForSeconds(0.01f);  //OLMAZSA DÝYALOG ATLIYO
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        PlayerMove.Instance.isMovable = true;
        isOnDialogue = false;
        MessageBox.SetActive(false);
    }




    public void ReadTalkLine(TalkLine talkLine)    /////VERILERI ISLEME KISMI BURASI
    {
        MessageBox.SetActive(true);

        if(talkLine.Persona == null)
        {
            talkLine.Persona = DialogPersona.CharEnumToPersona(talkLine.DefaultPersona);   //EÐER CONSTRUCTOR YERINE EDITORDEN OLUSTURUNCA PERSONA ALMASI ICIN
            talkLine.InitializeFromEditor();
        }


        MessageBoxArea.sprite = talkLine.Persona.BoxSprite;       //
        MessageBoxImage.sprite = talkLine.Persona.PersonaSprite;  //BU IKISI HARICI PERSONADAN CEKÝLMÝCEK
        MessageBoxExtraImage.sprite = talkLine.Persona.BoxExtraSprite;
        //if(talkLine.Persona.BoxExtraSprite != null) { MessageBoxExtraImage.sprite = talkLine.Persona.BoxExtraSprite; }


        //MessageBoxText.text = talkLine.Text;
        StartTyping(talkLine.Text, talkLine.ReadingTime, MessageBoxText);
        MessageBoxText.color = talkLine.TextColor;
        MessageBoxArea.color = talkLine.BoxColor;
        MessageBoxText.font = talkLine.FontAsset;


        if (talkLine.Direction)
        {
            //MessageBoxArea.GetComponent<SpriteRenderer>().flipX = true;
            //MessageBoxImage.GetComponent<SpriteRenderer>().flipX = true;
            MessageBoxArea.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            MessageBoxImage.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            MessageBoxText.GetComponent<RectTransform>().pivot = new Vector2(0.75f, 0.5f);
            MessageBoxExtraImage.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
        else
        {

            MessageBoxText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            MessageBoxArea.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            MessageBoxImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            MessageBoxExtraImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }


        MessageBoxText.rectTransform.position = new Vector2(talkLine.Persona.TalkBoxTextDataa.PosX, talkLine.Persona.TalkBoxTextDataa.PosY);
        MessageBoxText.rectTransform.sizeDelta = new Vector2(talkLine.Persona.TalkBoxTextDataa.Width, talkLine.Persona.TalkBoxTextDataa.Height);  
    }


    #region WritingAnimation


    private Coroutine typingCoroutine;

    public void StartTyping(string fullText, float duration, TextMeshProUGUI textUI = null, TextMeshPro text = null)
    {
        if(duration == 0) { duration = DefaultReadingTime; }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        if((textUI == null && text == null) ||(textUI != null && text != null))
        {
            Debug.LogWarning("HATALI TEXT GIRISI"); return;
        }

        if(textUI != null) { typingCoroutine = StartCoroutine(TypeTextUI(fullText, duration, textUI)); }
        if(text != null) { typingCoroutine = StartCoroutine(TypeText(fullText, duration, text)); }
    }       // Yazýyý yüzdelik olarak yazan coroutine
    private IEnumerator TypeTextUI(string fullText, float duration, TextMeshProUGUI textUI)
    {
        if(duration == -1) { textUI.text = fullText; yield break; }

        textUI.text = "";
        int totalCharacters = fullText.Length;
        float timePerCharacter = duration / totalCharacters;

        for (int i = 0; i <= totalCharacters; i++)
        {
            float percentage = (float)i / totalCharacters;
            int charsToShow = Mathf.FloorToInt(percentage * totalCharacters);

            textUI.text = fullText.Substring(0, charsToShow);
            yield return new WaitForSeconds(timePerCharacter);
        }

        textUI.text = fullText;
    }
    private IEnumerator TypeText(string fullText, float duration, TextMeshPro text)
    {
        if (duration == -1) { text.text = fullText; yield break; }

        text.text = "";
        int totalCharacters = fullText.Length;
        float timePerCharacter = duration / totalCharacters;

        for (int i = 0; i <= totalCharacters; i++)
        {
            float percentage = (float)i / totalCharacters;
            int charsToShow = Mathf.FloorToInt(percentage * totalCharacters);

            text.text = fullText.Substring(0, charsToShow);
            yield return new WaitForSeconds(timePerCharacter);
        }

        text.text = fullText;
    }

    #endregion


}


[System.Serializable]
public class TalkLine     ////////BURASI VERI ISLEMIYOOOO
{
    public DialogPersona Persona;
    public string Text;
    public bool Direction;
    public Color BoxColor;
    public Color TextColor;
    public TMP_FontAsset FontAsset;
    public float ReadingTime; //INITIALZE EDILMIYOR EDITORDE AYARLANICAK  ====  0 Ise Otomatik olarak okunurken deðiþtirikecej

    public DialogManager.Persona DefaultPersona;

    public TalkLine(string text,DialogPersona persona ,bool direction = default,Color boxColor = default,Color textColor = default,TMP_FontAsset fontAsset = null)
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

    public void InitializeFromEditor()
    {
        Persona = DialogPersona.CharEnumToPersona(DefaultPersona);

        if(BoxColor == default(Color)) { BoxColor = Persona.BoxColor; }
        if(TextColor == default(Color)) { TextColor = Persona.TextColor; }
        if(FontAsset == null) { FontAsset = Persona.FontAsset; }
    }

}


//[System.Serializable]
public class DialogPersona
{
    public string PersonaName; //OPSIYONEL
    public Sprite PersonaSprite;
    public Sprite BoxSprite;
    public Sprite BoxExtraSprite;
    public Color BoxColor; //USTTEN CEKÝLECEK
    public Color TextColor; //USTTEN CEKÝLECEK
    public TMP_FontAsset FontAsset;
    public TalkBoxTextData TalkBoxTextDataa;

    public DialogPersona(string personaName,Sprite personaSprite,Sprite boxSprite, Sprite boxExtraSprite, Color boxColor,Color textColor,TMP_FontAsset fontAsset,TalkBoxTextData talkBoxTextData)
    {
        PersonaName = personaName;
        PersonaSprite = personaSprite;
        BoxSprite = boxSprite;
        BoxExtraSprite = boxExtraSprite;
        BoxColor = boxColor;
        TextColor = textColor;
        FontAsset = fontAsset;
        TalkBoxTextDataa = talkBoxTextData;

        DialogManager.Instance.PersonaList.Add(this);  //KENDINI EKLEME BUGLU OLABILIR
    }

    
    public static DialogPersona CharEnumToPersona(DialogManager.Persona persona)
    {
        switch (persona)
        {
            case DialogManager.Persona.PPSlime:
                return DialogManager.Instance.PPSlime;


            case DialogManager.Persona.PPWife:
                return DialogManager.Instance.PPWife;


            case DialogManager.Persona.PPMole:
                return DialogManager.Instance.PPMole;

            case DialogManager.Persona.PPSign:
                return DialogManager.Instance.PPSign;

            case DialogManager.Persona.PPMoustacheSlime:
                return DialogManager.Instance.PPMoustacheSlime;

            case DialogManager.Persona.PPLambSlime:
                return DialogManager.Instance.PPLambSlime;

            case DialogManager.Persona.PPHappySlime:
                return DialogManager.Instance.PPHappySlime;

            case DialogManager.Persona.PPGutsSlime:
                return DialogManager.Instance.PPGutsSlime;

        }

        return null;
    }


    public struct TalkBoxTextData
    {
        public int PosX;
        public int PosY;

        public int Width;
        public int Height;

        public TalkBoxTextData(int posX,int posY, int width, int height)
        {
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
        }
    }

}


