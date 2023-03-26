using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class HelpGuide
{
    public string Name;
    public VideoClip clip;
    [TextArea(1,5)]
    public string content;
}
