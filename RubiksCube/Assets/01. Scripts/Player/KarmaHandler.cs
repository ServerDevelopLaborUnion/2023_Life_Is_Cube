using UnityEngine;

public class KarmaHandler : MonoBehaviour
{
    private StatPanel statPanel = null;
    private int karma = 0;

    private void Start()
    {
        statPanel = UIManager.Instance.StatPanel;

        karma = 100;
        statPanel?.DisplayKarma(karma);
    }

    public bool TryUseKarma(int amount)
    {
        if(karma < amount)
            return false;

        karma -= amount;
        statPanel?.DisplayKarma(karma);

        return true;
    }

    public void AddKarma(int amount)
    {
        karma += amount;
        statPanel?.DisplayKarma(karma);
    }
}
