using UnityEngine;

public class DEFINE
{
    public const int EnemyLayer = 1 << 7;

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