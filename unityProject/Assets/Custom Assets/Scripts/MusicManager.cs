using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] clipList;

    public AudioSource currentAudioSource;
    public AudioSource freeAudioSource;
    public float fadeDuration = 2;

    public enum songs { FIRSTPHASE, SECONDPHASE, VICTORY, GAMEOVER, MENU, SILENCE };
    private songs newSong;

    float wait;
    bool fadedOut1 = false;
    bool fadedOut2 = false;

    // Use this for initialization
    void Start()
    {
        //wait = Time.time;
        //playFirstPhaseMusic();

        EventManager.OnPhase1Start += playFirstPhaseMusic;
        EventManager.OnPhase2Start += playSecondPhaseMusic;
        EventManager.OnGameOver += playGameOverMusic;
        EventManager.OnEnterMenu += playMenuMusic;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Time.time - wait > 3 && !fadedOut1)
        {
            fadedOut1 = true;
            crossFade(currentAudioSource, freeAudioSource, songs.SECONDPHASE);
        }
        if (Time.time - wait > 10 && !fadedOut2)
        {
            fadedOut2 = true;
            crossFade(currentAudioSource, null, songs.SILENCE);
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

        
        while (tempRef.volume < 1F)
        {
            tempRef.volume = Mathf.Lerp(0F, 1F, Mathf.SmoothStep(0F, 1F, (Time.time - start) / fadeDuration));
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
            tempRef.volume = Mathf.Lerp(tempVolume, 0F, Mathf.SmoothStep(0F, 1F, (Time.time - start) / fadeDuration));
            yield return 0;
        }
        tempRef.Stop();
    }
}