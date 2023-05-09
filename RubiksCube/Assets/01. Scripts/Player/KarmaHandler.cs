using UnityEngine;

public class KarmaHandler : MonoBehaviour
{
    private StatPanel statPanel = null;
    private int karma = 0;

    private void Awake()
    {
        statPanel = DEFINE.MainCanvas.Find("StatPanel").GetComponent<StatPanel>();
    }

    private void Start()
    {
        karma = 0;
        statPanel?.DisplayKarma(karma);
    }

    public bool TryUseKarma(int amount)
    {
        if(karma < amount)
            return false;

        karma -= amount;
        statPanel?.DisplayKarma(amount);

        return true;
    }

    public void AddKarma(int amount)
    {
        karma += amount;
        statPanel?.DisplayKarma(amount);
    }
}
