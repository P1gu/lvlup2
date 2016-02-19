using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    //public AudioClip[] clipList;

    public AudioClip firstPhase;
    [Range(0, 100)]
    public int firstPhaseSoundLevel;

    public AudioClip secondPhase;
    [Range(0, 100)]
    public int secondPhaseSoundLevel;

    public AudioClip victory;
    [Range(0, 100)]
    public int victorySoundLevel;

    public AudioClip gameOver;
    [Range(0, 100)]
    public int gameOverSoundLevel;

    public AudioClip menu;
    [Range(0, 100)]
    public int menuSoundLevel;

    public AudioSource currentAudioSource;
    public AudioSource freeAudioSource;
    public float fadeDuration;
    [Range(0, 100)]
    public int soundLevel;

    public enum songs { FIRSTPHASE, SECONDPHASE, VICTORY, GAMEOVER, MENU, SILENCE };
    private AudioClip currentClip;

    float wait;
    bool fadedOut1 = false;
    bool fadedOut2 = false;

    // Use this for initialization
    void Start()
    {
        wait = Time.time;
        playFirstPhaseMusic();

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
            crossFade(currentAudioSource, freeAudioSource, firstPhaseSoundLevel, firstPhase);
        }
        /*if (Time.time - wait > 7 && !fadedOut2)
        {
            fadedOut2 = true;
            crossFade(currentAudioSource, freeAudioSource, songs.SECONDPHASE);
        }*/

    }

    public void playFirstPhaseMusic()
    {
        crossFade(null, freeAudioSource, firstPhaseSoundLevel, firstPhase);
    }

    public void playSecondPhaseMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, secondPhaseSoundLevel, secondPhase);
    }

    public void playMenuMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, menuSoundLevel, menu);
    }

    public void playGameOverMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, gameOverSoundLevel, gameOver);
    }

    public void playVictoryMusic()
    {
        crossFade(currentAudioSource, freeAudioSource, victorySoundLevel, victory);
    }

    public void fadeMusicOut()
    {
        StartCoroutine("fadeOut");
    }

    private void crossFade(AudioSource endingSource, AudioSource startingSource, int soundLevel, AudioClip clip)
    {
        currentClip = clip;
        if (endingSource != null)
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

        tempRef.clip = currentClip;
        tempRef.Play();
        tempRef.volume = 0;
        float start = Time.time;

        
        while (tempRef.volume < (float)soundLevel /100)
        {
            tempRef.volume = Mathf.SmoothStep(0F, (float)soundLevel/100, (Time.time - start) / fadeDuration);
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
            tempRef.volume = Mathf.SmoothStep((float)soundLevel /100, 0F, (Time.time - start) / fadeDuration);
            yield return 0;
        }
        tempRef.Stop();
    }
}