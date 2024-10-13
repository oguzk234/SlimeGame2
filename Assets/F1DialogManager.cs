using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class F1DialogManager : MonoBehaviour
{
    public F1DialogManager Instance;

    public F1DPersona SlimeFP;
    public F1DPersona EvilDogeFP;

    public static F1DPersona SlimeFPStatic;
    public static F1DPersona EvilDogeFPStatic;

    [Header("MessageBoxOptions")]
    public GameObject F1DMessageBox;
    public SpriteRenderer F1DMessageBoxArea;
    public TextMeshProUGUI F1DMessageText;
    public Sprite DefaultMsgBoxArea;
    public Vector3 directionTrueLoc;
    public Vector3 directionFalseLoc;



    private void Awake()
    {
        Instance = this;

        SlimeFPStatic = SlimeFP;
        EvilDogeFPStatic = EvilDogeFP;
    }




    public IEnumerator ReadF1dTalkSet(List<F1DTalkLine> f1DTalks)
    {



        for (int i = 0; i < f1DTalks.Count; i++)
        {
            ReadF1dTalkLine(f1DTalks[i]);
            yield return new WaitForSeconds(0.01f);  //OLMAZSA DÝYALOG ATLIYO
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        F1DMessageBox.SetActive(false);


    }


    public void ReadF1dTalkLine(F1DTalkLine f1DTalk)    /////VERILERI ISLEME KISMI BURASI
    {
        f1DTalk.InitializeDefaults();
        F1DMessageBox.SetActive(true);


        if(F1DMessageBoxArea.sprite != null)
            F1DMessageBoxArea.sprite = f1DTalk.BoxSprite;

        F1DMessageText.text = f1DTalk.Text;
        F1DMessageText.color = f1DTalk.TextColor;
        F1DMessageBoxArea.color = f1DTalk.BoxColor;
        F1DMessageText.font = f1DTalk.FontAsset;

        if (f1DTalk.Direction)
        {
            F1DMessageBox.GetComponent<RectTransform>().localPosition = directionTrueLoc;
        }
        else
        {
            F1DMessageBox.GetComponent<RectTransform>().localPosition = directionFalseLoc;
        }


    }


}





[System.Serializable]
public class F1DTalkLine
{
    public string Text;
    public bool Direction;
    public Sprite BoxSprite;
    public Color BoxColor;
    public Color TextColor;
    public TMP_FontAsset FontAsset;

    public enum F1dPersonaEnum { Slime, EvilDoge }
    public F1dPersonaEnum F1DPersonaDefaultEnum;
    private F1DPersona F1DPersonaDefault;

    public void InitializeDefaults()
    {
        F1DPersonaDefault = F1DPersona.TypeToPersona(F1DPersonaDefaultEnum);

        if (BoxColor == default(Color))
        {
            BoxColor = F1DPersonaDefault.boxColor;
        }
        if (TextColor == default(Color))
        {
            TextColor = F1DPersonaDefault.textColor;
        }
        if (FontAsset == null)
        {
            FontAsset = F1DPersonaDefault.fontAsset;
        }

        if(BoxSprite == null)
        {
 
        }
    }



}

[System.Serializable]
public class F1DPersona
{
    public bool direction = true;
    public Color boxColor;
    public Color textColor;
    public TMP_FontAsset fontAsset;


    public static F1DPersona TypeToPersona(F1DTalkLine.F1dPersonaEnum f1dP)
    {
        switch (f1dP)
        {
            case F1DTalkLine.F1dPersonaEnum.Slime:
                return F1DialogManager.SlimeFPStatic;
            case F1DTalkLine.F1dPersonaEnum.EvilDoge:
                return F1DialogManager.EvilDogeFPStatic;
        }


        return null;
    }
}
