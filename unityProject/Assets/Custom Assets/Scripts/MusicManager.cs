using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] clipList;

    public AudioSource currentAudioSource;
    public AudioSource freeAudioSource;
    public float fadeDuration;
    [Range(0, 100)]
    public int soundLevel;

    public enum songs { FIRSTPHASE, SECONDPHASE, VICTORY, GAMEOVER, MENU, SILENCE };
    private songs newSong;

    float wait;
    bool fadedOut1 = false;
    bool fadedOut2 = false;

    // Use this for initialization
    void Start()
    {
        wait = Time.time;
        playFirstPhaseMusic();
        soundLevel = 100;

        currentAudioSource.loop = true;
        freeAudioSource.loop = true;

        EventManager.OnPhase1Start += playFirstPhaseMusic;
        EventManager.OnPhase2Start += playSecondPhaseMusic;
        EventManager.OnGameOver += playGameOverMusic;
        EventManager.OnEnterMenu += playMenuMusic;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - wait > 0 && !fadedOut1)
        {
            fadedOut1 = true;
            crossFade(currentAudioSource, freeAudioSource, songs.FIRSTPHASE);
        }
        /*if (Time.time - wait > 7 && !fadedOut2)
        {
            fadedOut2 = true;
            crossFade(currentAudioSource, freeAudioSource, songs.SECONDPHASE);
        }*/

    }

    public void playFirstPhaseMusic()
    {
        crossFade(null, freeAudioSource, songs.FIRSTPHASE);
    }

    public void playSecondPhaseMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, songs.SECONDPHASE);
    }

    public void playMenuMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, songs.MENU);
    }

    public void playGameOverMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, songs.GAMEOVER);
    }

    public void playVictoryMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, songs.VICTORY);
    }

    public void fadeMusicOut()
    {
        crossFade(currentAudioSource, null, songs.SILENCE);
    }

    private void crossFade(AudioSource endingSource, AudioSource startingSource, songs song, float fadeDuration = 10)
    {
        newSong = song;
        if(endingSource != null)
        {
            StartCoroutine("fadeOut");
        }
        if (startingSource != null)
        {
            StartCoroutine("fadeIn");
        }
    }

    public IEnumerator fadeIn()
    {
        AudioSource tempRef = freeAudioSource;
        freeAudioSource = currentAudioSource;
        currentAudioSource = tempRef;

        tempRef.clip = clipList[(int)newSong];
        tempRef.Play();
        tempRef.volume = 0;
        float start = Time.time;

        
        while (tempRef.volume < soundLevel/100)
        {
            tempRef.volume = Mathf.SmoothStep(0F, soundLevel/100, (Time.time - start) / fadeDuration);
            yield return 0;
        }
    }

    public IEnumerator fadeOut()
    {
        StopCoroutine("fadeIn");
        float start = Time.time;
        AudioSource tempRef = currentAudioSource;
        float tempVolume = tempRef.volume;
        while (tempRef.volume > 0F)
        {
            tempRef.volume = Mathf.SmoothStep(soundLevel/100, 0F, (Time.time - start) / fadeDuration);
            yield return 0;
        }
        tempRef.Stop();
    }
}