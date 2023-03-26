using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class HelpGuideManager : MonoBehaviour
{
    public HelpGuide[] helpGuides;

    [Space]
    public VideoPlayer videoPlayer;
    public Text title;
    public Text content;

    private void Start()
    {
        videoPlayer.GetComponent<VideoPlayer>();
        title.GetComponent<Text>();
        content.GetComponent<Text>();
    }
}
