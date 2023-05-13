using UnityEngine;

public class KarmaHandler : MonoBehaviour
{
    private KarmaPanel karmaPanel = null;
    private int karma = 0;

    private void Start()
    {
        karmaPanel = UIManager.Instance.KarmaPanel;

        karma = 100;
        karmaPanel?.DisplayKarma(karma);
    }

    public bool TryUseKarma(int amount)
    {
        if(karma < amount)
            return false;

        karma -= amount;
        karmaPanel?.DisplayKarma(karma);

        return true;
    }

    public void AddKarma(int amount)
    {
        karma += amount;
        karmaPanel?.DisplayKarma(karma);
    }
}
