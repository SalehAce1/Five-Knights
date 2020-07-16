﻿using UnityEngine;
using System.Collections;

namespace FiveKnights
{
    public class MusicPlayer
    {
        public float Volume = 1f;
        public GameObject Player;
        public GameObject Spawn;
        public AudioClip Clip;
        public float MinPitch;
        public float MaxPitch;
        public bool Loop;

        private AudioSource audio;
        private Coroutine _loop;

        private IEnumerator LoopMusic()
        {
            while (true)
            {
                yield return new WaitForSeconds(Clip.length);
                AudioClip clip = Clip;
                audio.pitch = Random.Range(MinPitch, MaxPitch);
                audio.volume = Volume;
                audio.PlayOneShot(clip);
                yield return null;
            }
        }

        public void UpdateMusic()
        {
            audio.pitch = Random.Range(MinPitch, MaxPitch);
            audio.volume = Volume;
        }

        public void StopMusic()
        {
            if (!Loop)
                return;

            WDController.Instance.StopCoroutine(_loop);
            audio.Stop();
        }

        public void DoPlayRandomClip()
        {
            GameObject audioPlayer = Player.Spawn
            (
                Spawn.transform.position,
                Quaternion.Euler(Vector3.up)
            );

            audio = audioPlayer.GetComponent<AudioSource>();
            audio.clip = null;
            audio.pitch = Random.Range(MinPitch, MaxPitch);
            audio.volume = Volume;
            audio.PlayOneShot(Clip);

            if (Loop)
            {
                _loop = WDController.Instance.StartCoroutine(LoopMusic());
            }
        }
    }
}