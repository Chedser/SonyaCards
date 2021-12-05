using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
 [SerializeField] GameObject audioWin;
 [SerializeField] GameObject audioLose;
 [SerializeField] AudioSource drawSound;

    public void PlaySound(string str) {

        switch (str) {

            case "win": audioWin.transform.GetChild(Random.Range(0, audioWin.transform.childCount - 1)).GetComponent<AudioSource>().Play();  break;
            case "lose": audioLose.transform.GetChild(Random.Range(0, audioLose.transform.childCount - 1)).GetComponent<AudioSource>().Play(); break;
            default: drawSound.Play(); break;

        }  

    }

    public void StopPlay() {

        foreach (AudioSource audio in audioWin.GetComponentsInChildren<AudioSource>()) {

            if (audio.isPlaying) { audio.Stop(); }

        }

        foreach (AudioSource audio in audioLose.GetComponentsInChildren<AudioSource>())
        {

            if (audio.isPlaying) { audio.Stop(); }

        }

    }

}
