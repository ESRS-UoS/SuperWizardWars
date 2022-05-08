using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static AudioClip lightsaber, knife, bow, fireball, death, hit, heal, newItem, shield, victory, confirm;
    static AudioSource audioSource;
    public AudioMixerGroup music;
    public AudioMixerGroup sfx;
    static AudioMixerGroup staticMusic;
    static AudioMixerGroup staticSfx;

    // Start is called before the first frame update
    void Start()
    {
        lightsaber = Resources.Load<AudioClip>("lightsaber");
        knife = Resources.Load<AudioClip>("knife");
        bow = Resources.Load<AudioClip>("bow");
        fireball = Resources.Load<AudioClip>("fireball");
        death = Resources.Load<AudioClip>("death");
        hit = Resources.Load<AudioClip>("hit");
        heal = Resources.Load<AudioClip>("heal");
        newItem = Resources.Load<AudioClip>("newItem");
        shield = Resources.Load<AudioClip>("shield");
        victory = Resources.Load<AudioClip>("victory");
        confirm = Resources.Load<AudioClip>("confirm");

        audioSource = GetComponent<AudioSource>();
        staticSfx = sfx;
        staticMusic = music;
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "lightsaber":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(lightsaber);
                break;
            case "knife":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(knife);
                break;
            case "bow":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(bow);
                break;
            case "fireball":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(fireball);
                break;
            case "death":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(death);
                break;
            case "hit":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(hit);
                break;
            case "heal":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(heal);
                break;
            case "newItem":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(newItem);
                break;
            case "shield":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(shield);
                break;
            case "victory":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(victory);
                break;
            case "confirm":
                audioSource.outputAudioMixerGroup = staticSfx;
                audioSource.PlayOneShot(confirm);
                break;
        }
    }
}
