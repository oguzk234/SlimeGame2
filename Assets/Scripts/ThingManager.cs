using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingManager : MonoBehaviour
{
    public List<ThingSO> ThingsToDo;

    public int indexNo = 0;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DoThing();
        }
    }

    public void DoNextThing()
    {
        DialogManager.Instance.OnEnd -= DoNextThing;
        //DIGERI ZATEN YOK OLDUGU ICIN GEREK YOK GIBIMSI SANIRIM (FIGHT1MANAGER)

        indexNo++;


        if(indexNo+1 > ThingsToDo.Count)
        {
            return;
        }


        DoThing();
    }
    public void DoThing()
    {
        if (ThingsToDo[indexNo].activeThing == ThingSO.ActiveThing.F1D)
        {
            FightManager.Instance.StartFight1Dodge(ThingsToDo[indexNo].fight1Dodge).OnEnd += DoNextThing;
        }
        else if (ThingsToDo[indexNo].activeThing == ThingSO.ActiveThing.TalkLines)
        {
            DialogManager.Instance.ReadTalkSet(ThingsToDo[indexNo].talkLines);
            DialogManager.Instance.OnEnd += DoNextThing;
        }
    }
}
