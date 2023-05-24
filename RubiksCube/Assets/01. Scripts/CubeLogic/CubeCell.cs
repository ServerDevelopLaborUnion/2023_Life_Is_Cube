using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCell : MonoBehaviour
{
    [SerializeField] BiomeFlags cellBiome;
    public BiomeFlags CellBiome => cellBiome;

    private Queue<Transform> extraGroundPlanes = null;
    private Queue<Transform> usedGroundPlanes = null;
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

    private bool CheckNeighborCell(out Vector3 direction)
    {
        //이웃한 셀이 있다면 바닥 늘리기
        List<CubeCell> cells = cube.ActivatedCells;

        for (int i = 0; i < cells.Count; i++)
        {

        }

        direction = Vector3.zero;

        return true;
    }

    public void SetToEliteStage(float time, Vector3 dir)
    {
        Transform trm = extraGroundPlanes.Dequeue();
        StartCoroutine(PushExtraGroundCoroutine(time, trm, dir));

        usedGroundPlanes.Enqueue(trm);
    }

    public void ClearEliteStage(float time)
    {
        while(usedGroundPlanes.Count > 0)
        {
            Transform trm = usedGroundPlanes.Dequeue();
            ResetExtraGrounds(time, trm);

            extraGroundPlanes.Enqueue(trm);
        }
    }

    private void ResetExtraGrounds(float time, Transform trm)
    {
        StartCoroutine(PushExtraGroundCoroutine(time, trm, -trm.position));
    }

    private IEnumerator PushExtraGroundCoroutine(float time, Transform trm, Vector3 amount)
    {
        float timer = 0f;
        float theta = 0f;

        Vector3 startPos = trm.position;
        Vector3 endPos = startPos + amount;

        while (theta < 1f)
        {
            theta = timer / time;
            trm.position = Vector3.Lerp(startPos, endPos, theta);

            timer += Time.deltaTime;
            yield return null;
        }

        trm.position = endPos;
    }
}
