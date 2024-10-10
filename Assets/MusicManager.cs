using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] LevelMusic;
    [SerializeField] private AudioClip[] HighLevelMusic;
    [SerializeField] private AudioClip[] LowLevelMusic;

    private void ChangeMusic(AudioClip nextMusic)
    {
        SoundManager.instance.ActivateFade();

        if (!SoundManager.instance.fading)
        {
            SoundManager.instance.musicSource.clip = nextMusic;
            SoundManager.instance.ChangeMusicVolume(1f);
        }
    }

    public void ActivateMiddleMusic(int levelIndex)
    {
        ChangeMusic(LevelMusic[levelIndex]);
    }

    public void ActivateHighMusic(int levelIndex)
    {
        ChangeMusic(HighLevelMusic[levelIndex]);
    }

    public void ActivateLowMusic(int levelIndex)
    {
        ChangeMusic(LowLevelMusic[levelIndex]);
    }
}
