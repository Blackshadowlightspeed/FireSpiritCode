using UnityEngine;

public class muteAudio : MonoBehaviour
{

    public void Mute()
    {
        AudioListener.volume = 0;
    }

    public void Unmute()
    {
        AudioListener.volume = 1;
    }
}
