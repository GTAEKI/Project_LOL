using Photon.Pun;
using UnityEngine;

public class CameraSetup : MonoBehaviourPun
{
    public CameraController followCam;

    //Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            followCam = FindObjectOfType<CameraController>();
            followCam.player = transform.gameObject;
            followCam.camFOV = followCam.GetComponent<Camera>().fieldOfView;

            //Managers.Input.Init();
        }
   }

    //private void OnEnable()
    //{
    //    if (photonView.IsMine)
    //    {
    //        followCam = FindObjectOfType<CameraController>();

    //        followCam.player = transform.gameObject;
    //        followCam.camFOV = followCam.GetComponent<Camera>().fieldOfView;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
