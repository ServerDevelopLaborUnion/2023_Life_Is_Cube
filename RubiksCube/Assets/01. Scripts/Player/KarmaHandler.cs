using UnityEngine;

public class KarmaHandler : MonoBehaviour
{
    private int karma = 0;

    public bool TryUseKarma(int amount)
    {
        if(karma < amount)
            return false;

        karma -= amount;
        return true;
    }

    public void AddKarma(int amount) => karma += amount;
}
