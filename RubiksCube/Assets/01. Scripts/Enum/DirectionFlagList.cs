using System.Collections.Generic;

[System.Serializable]
public struct DirectionFlagList
{
    public DirectionFlags this[int index] {
        get {
            return flags[index];
        }
    }

    public List<DirectionFlags> flags;
}