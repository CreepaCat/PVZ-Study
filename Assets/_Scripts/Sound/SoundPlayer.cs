using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Sound
{
    public class SoundPlayer : MonoBehaviour
    {

        public static SoundPlayer Instance = null;
        AudioSource audioSource;
        Dictionary<string, AudioClip> soundClipDit;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            soundClipDit = new Dictionary<string, AudioClip>();
            audioSource = GetComponent<AudioSource>();
        }
        void Start()
        {

        }

        //资源加载和缓存
        public AudioClip LoadAudio(string path)
        {
            return Resources.Load<AudioClip>(path);
        }

        public AudioClip GetClip(string path)
        {
            if (!soundClipDit.ContainsKey(path))
            {
                soundClipDit[path] = LoadAudio(path);
            }
            return soundClipDit[path];
        }

        //BGM
        public void PlayBGM(string path, float volume = 1.0f)
        {
            audioSource.Stop();
            audioSource.clip = GetClip(path);
            audioSource.volume = volume;
            audioSource.Play();
        }

        public void StopBGM()
        {
            audioSource.Stop();
        }

        //播放音效
        public void PlaySound(string path, float volume = 1.0f)
        {
            audioSource.PlayOneShot(GetClip(path), volume);
            Debug.Log("Play One Shot " + GetClip(path));
            // audioSource.volume = volume;
        }
        //播放音效，使用别的音频源
        public void PlaySound(AudioSource otherAudioSource, string path, float volume = 1.0f)
        {
            otherAudioSource.PlayOneShot(GetClip(path), volume);
        }
    }
}


