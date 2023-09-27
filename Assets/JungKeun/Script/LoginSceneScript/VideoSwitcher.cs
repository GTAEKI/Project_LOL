using UnityEngine;
using UnityEngine.Video;

public class VideoSwitcher : MonoBehaviour
{
    public VideoPlayer firstVideoPlayer;   // 첫 번째 Video Player
    public VideoPlayer secondVideoPlayer;  // 두 번째 Video Player

    private bool switchedToSecondVideo = false;

    void Start()
    {
        

        firstVideoPlayer.Play();
        secondVideoPlayer.Stop();
        Debug.Log(firstVideoPlayer.time);

    }

    void Update()
    {

        if (!switchedToSecondVideo && firstVideoPlayer.isPrepared && firstVideoPlayer.isPlaying)
        {
            //Debug.Log(firstVideoPlayer.time);
            //Debug.Log(firstVideoPlayer.clip.length);
            if (firstVideoPlayer.time >= firstVideoPlayer.clip.length-1)
            {
                switchedToSecondVideo = true;
                firstVideoPlayer.Stop();
                secondVideoPlayer.isLooping = true; // 두 번째 영상을 무한 루프로 설정
                secondVideoPlayer.Play();
            }
        }
    }
}
