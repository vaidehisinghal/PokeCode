using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Clouds : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "4K_11.mp4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
