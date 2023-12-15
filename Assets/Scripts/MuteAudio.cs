using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    public void MuteToggle(int muted)
    {
        if (muted == 0)
        {
            AudioListener.volume = 0;
            muted = 1;
        }
        else if (muted == 1)
        {
            AudioListener.volume = 1;
            muted = 0;
        }
    }
}
