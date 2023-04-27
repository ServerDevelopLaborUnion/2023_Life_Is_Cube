using UnityEngine;

public class CubeCell : MonoBehaviour
{
    [SerializeField] BiomeFlags cellBiome;
    public BiomeFlags CellBiome => cellBiome;

    private int cellIndex = 0;
    public int CellIndex => cellIndex;

    public void ModifyCellIndex(int idx)
    {
        cellIndex = idx;
    }
}
