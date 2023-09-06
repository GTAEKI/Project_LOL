using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    private Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] names = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < names.Length - 1; i++)
            {
               GameObject temp = new GameObject { name = names[i] };
                audioSources[i] = temp.AddComponent<AudioSource>();
                temp.transform.parent = root.transform;
            }

            audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource source in audioSources)
        {
            source.clip = null;
            source.Stop();
        }
        audioClipDict.Clear();
    }

    public void Play(string _path, Define.Sound _type = Define.Sound.Sfx, float _pitch = 1f)
    {
        AudioClip clip = GetOrAddAudioClip(_path, _type);
        Play(clip, _type, _pitch);
    }

    public void Play(AudioClip _clip, Define.Sound _type = Define.Sound.Sfx, float _pitch = 1f)
    {
        if (_clip == null) return;

        if (_type == Define.Sound.Bgm)
        {
            AudioSource source = audioSources[(int)Define.Sound.Bgm];
            if (source.isPlaying) source.Stop();

            source.pitch = _pitch;
            source.clip = _clip;
            source.Play();
        }
        else
        {
            AudioSource source = audioSources[(int)Define.Sound.Sfx];
            source.pitch = _pitch;
            source.PlayOneShot(_clip);
        }
    }

    private AudioClip GetOrAddAudioClip(string _path, Define.Sound _type = Define.Sound.Sfx)
    {
        if (!_path.Contains("Sound/")) _path = $"Sound/{_path}";

        AudioClip clip = null;

        if (_type == Define.Sound.Bgm)
        {
            clip = Managers.Resource.Load<AudioClip>(_path);
        }
        else
        {

            if (!audioClipDict.TryGetValue(_path, out clip))
            {
                clip = Managers.Resource.Load<AudioClip>(_path);
                audioClipDict.Add(_path, clip);
            }
        }

        if (clip == null)
        {
            Debug.LogError($"AudioClip Mission! : {_path}");
        }
        return clip;
    }
}
