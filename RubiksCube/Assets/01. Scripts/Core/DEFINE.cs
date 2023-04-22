using UnityEngine;

public class DEFINE
{
    public const int EnemyLayer = 1 << 7;
    public const int CubeLayer = 1 << 8;
    public const int InteractableLayer = 1 << 10;

    public const float InteractRadius = 3f;

    private static Transform playerTrm = null;
    public static Transform PlayerTrm {
        get {
            if(playerTrm == null)
                playerTrm = GameObject.Find("Player").transform;

            return playerTrm;
        }
    }

    private static Camera mainCam = null;
    public static Camera MainCam {
        get {
            if(mainCam == null)
                mainCam = Camera.main;
            
            return mainCam;
        }
    }
}