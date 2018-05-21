using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CaptAnimGif : MonoBehaviour
{
    public KeyCode keyCodeForRecording = KeyCode.V;
    public Material materialForNoRecording = null;
    public Material materialForRecording = null;
    public float intervalSeconds = 0.1f;
    public int maxFrames = 50;
    public string baseFileName = Path.DirectorySeparatorChar + "work" + Path.DirectorySeparatorChar + "tmp" + Path.DirectorySeparatorChar + "capt-anim";
    public string command = Path.DirectorySeparatorChar + "local" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "im" + Path.DirectorySeparatorChar + "convert -loop 0 -delay 10 *.png out.gif";

    private bool capt;
    private bool capt_end;
    private float time;
    private int counter;

    void Start()
    {
        capt = false;
        capt_end = false;
        time = 0f;
        counter = 0;
    }

    void Update()
    {
        if (capt)
        {
            time += Time.deltaTime;
            if (time >= intervalSeconds)
            {
                time -= intervalSeconds;
                counter++;
                if (counter <= maxFrames)
                    ScreenCapture.CaptureScreenshot(baseFileName + string.Format("{0:D6}", counter) + ".png");
                else
                    capt_end = true;
            }
            if (Input.GetKeyDown(keyCodeForRecording) || counter >= maxFrames)
                capt_end = true;
        }
        else
        {
            if (Input.GetKeyDown(keyCodeForRecording) && time == 0f)
                capt = true;

        }

    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Material mat = capt ? materialForRecording : materialForNoRecording;
        if (mat == null)
        {
            Graphics.Blit(src, dest);
        }
        else
        {
            Graphics.Blit(src, dest, mat);
        }
        if (capt_end)
            capt = false;
    }
}