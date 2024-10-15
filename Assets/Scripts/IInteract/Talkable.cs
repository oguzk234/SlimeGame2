using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Talkable : MonoBehaviour, IInteractable
{
    public List<TalkLine> Talks = new List<TalkLine>();
    public void Interact()
    {
        DialogManager.Instance.ReadTalkSet(Talks);
    }


    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DialogManager.Instance.ReadTalkSet(Talks);
        }
    }
    */

}
