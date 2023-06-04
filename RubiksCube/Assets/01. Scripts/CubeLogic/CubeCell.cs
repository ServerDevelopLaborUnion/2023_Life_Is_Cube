using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCell : MonoBehaviour
{
    [SerializeField] BiomeFlags cellBiome;
    public BiomeFlags CellBiome => cellBiome;

    private List<CubeCell> neighborCells = new List<CubeCell>();
    public List<CubeCell> NeighborCells => neighborCells;

    private Queue<Transform> extraGroundPlanes = new Queue<Transform>();
    private Queue<Transform> usedGroundPlanes = new Queue<Transform>();
    private Cube cube = null;

    private int cellIndex = 0;
    public int CellIndex => cellIndex;

    private void Awake()
    {
        Transform extraGround = transform.Find("ExtraGrounds");
        for (int i = 0; i < extraGround.childCount; i++)
            extraGroundPlanes.Enqueue(extraGround.GetChild(i));

        cube = transform.parent.parent.GetComponent<Cube>();
    }

    public void ModifyCellIndex(int idx)
    {
        cellIndex = idx;
    }

    public Vector3[] CheckNeighborCell()
    {
        //이웃한 셀이 있다면 바닥 늘리기
        List<CubeCell> cells = cube.ActivatedCells;
        List<Vector3> resultsX = new List<Vector3>();
        List<Vector3> resultsZ = new List<Vector3>();

        for (int y = cellIndex / 3 - 2; y <= cellIndex / 3 + 2; y++)
        {
            if (y < 0 || y > 2)
                continue;

            for (int x = cellIndex % 3 - 2; x <= cellIndex % 3 + 2; x++)
            {
                if (x < 0 || x > 2)
                    continue;

                if(y * 3 + x == cellIndex)
                    continue;

                // if (new Vector2(y - cellIndex / 3, x - cellIndex % 3 ).magnitude > 1)
                if(y - cellIndex / 3 != 0 &&  x - cellIndex % 3 != 0)
                    continue;

                if (cellBiome == cells[y * 3 + x].cellBiome)
                {
                    neighborCells.Add(cells[y * 3 + x]);
                    Vector3 dir = new Vector3(x - cellIndex % 3, 0, -(y - cellIndex / 3)).normalized;

                    // Debug.Log($"{cellIndex} : {dir}");

                    if(dir.x != 0)
                        resultsX.Add(dir);
                    else if(dir.z != 0)
                        resultsZ.Add(dir);
                    // results.Add(new Vector3(x - cellIndex % 3, 0, -(y - cellIndex / 3)));
                }
            }
        }

        List<Vector3> results = new List<Vector3>();
        
        if(resultsX.Count >= 2)
            for(int i = 0; i < resultsX.Count; i++)
                if(results.Contains(resultsX[i]) == false)
                    results.Add(resultsX[i]);

        if(resultsZ.Count >= 2)
            for(int i = 0; i < resultsZ.Count; i++)
                if(results.Contains(resultsZ[i]) == false)
                    results.Add(resultsZ[i]);

        return results.ToArray();
    }

    public void SetToEliteStage(float time, Vector3 dir)
    {
        Transform trm = extraGroundPlanes.Dequeue();
        StartCoroutine(PushExtraGroundCoroutine(time, trm, dir));

        usedGroundPlanes.Enqueue(trm);
    }

    public void ClearEliteStage(float time)
    {
        while (usedGroundPlanes.Count > 0)
        {
            Transform trm = usedGroundPlanes.Dequeue();
            ResetExtraGrounds(time, trm);

            extraGroundPlanes.Enqueue(trm);
            
            cube.CubeConfiner.SetActiveCollider(CellIndex, (trm.position - trm.parent.position).normalized, true);
            // Debug.Log($"{CellIndex}, {(trm.position - trm.parent.position).normalized}");
            // Debug.Break();
        }

        neighborCells.Clear();

    }

    private void ResetExtraGrounds(float time, Transform trm)
    {
        // Debug.Break();
        // Debug.Log(0.025f * (-trm.localPosition).normalized);
        StartCoroutine(PushExtraGroundCoroutine(time, trm, trm.TransformDirection(0.025f * (-trm.localPosition).normalized)));
    }

    private IEnumerator PushExtraGroundCoroutine(float time, Transform trm, Vector3 amount)
    {
        float timer = 0f;
        float theta = 0f;

        Vector3 startPos = trm.localPosition;
        Vector3 endPos = startPos + trm.InverseTransformDirection(amount);
        // Debug.Break();
        // Debug.Log(amount);
        // Debug.Log(endPos);

        while (theta < 1f)
        {
            theta = timer / time;
            trm.localPosition = Vector3.Lerp(startPos, endPos, theta);

            timer += Time.deltaTime;
            yield return null;
        }

        trm.localPosition = endPos;
    }
}
