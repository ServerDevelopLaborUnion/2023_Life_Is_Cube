using UnityEngine;

public class Bridge : MonoBehaviour
{
    private BridgeHandler bridgeHandler;

    private void Awake()
    {
        bridgeHandler = transform.parent.GetComponent<BridgeHandler>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") == false)
            return;

        Debug.Log("Trying to Change Stage");
        bridgeHandler.TryChangeStage();
    }
}
