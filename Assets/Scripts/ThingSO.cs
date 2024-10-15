using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thing",menuName = "ThingSO")]
public class ThingSO : ScriptableObject
{
    public enum ActiveThing { F1D,TalkLines}
    public ActiveThing activeThing;

    public Fight1Dodge fight1Dodge = null;
    public List<TalkLine> talkLines = null;
}
