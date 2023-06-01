using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCell : MonoBehaviour
{
    [SerializeField] BiomeFlags cellBiome;
    public BiomeFlags CellBiome => cellBiome;

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
        List<Vector3> results = new List<Vector3>();

        for (int y = cellIndex / 3 - 1; y <= cellIndex / 3 + 1; y++)
        {
            if (y < 0 || y > 2)
                continue;

            for (int x = cellIndex % 3 - 1; x <= cellIndex % 3 + 1; x++)
            {
                if (x < 0 || x > 2)
                    continue;

                if(y * 3 + x == cellIndex)
                    continue;

                if(new Vector2Int(y - cellIndex / 3, x - cellIndex % 3).magnitude > 1)
                    continue;

                if (cellBiome == cells[y * 3 + x].cellBiome)
                    results.Add(new Vector3(x - cellIndex % 3, 0, -(y - cellIndex / 3)));
            }
        }

//        Debug.Log(gameObject.name + " " + results.Count);
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
            
            cube.CubeConfiner.SetActiveCollider(CellIndex, trm.TransformDirection(trm.localPosition.normalized), true);
            //Debug.Log($"{CellIndex}, {trm.TransformDirection(trm.localPosition.normalized)}");
        }

    }

    private void ResetExtraGrounds(float time, Transform trm)
    {
        StartCoroutine(PushExtraGroundCoroutine(time, trm, -trm.localPosition));
    }

    private IEnumerator PushExtraGroundCoroutine(float time, Transform trm, Vector3 amount)
    {
        float timer = 0f;
        float theta = 0f;

        Vector3 startPos = trm.localPosition;
        Vector3 endPos = startPos + trm.InverseTransformDirection(amount);

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
