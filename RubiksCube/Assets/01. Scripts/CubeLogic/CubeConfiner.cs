using System.Collections.Generic;
using UnityEngine;

public class CubeConfiner : MonoBehaviour
{
    private List<Dictionary<DirectionFlags, Collider>> colliderMapList;
    private Dictionary<Vector3, DirectionFlags> directionMap = new Dictionary<Vector3, DirectionFlags>()
    {
        [Vector3.right] = DirectionFlags.Right,
        [Vector3.left] = DirectionFlags.Left,
        [Vector3.forward] = DirectionFlags.Up,
        [Vector3.back] = DirectionFlags.Down
    };
    private Cube cube = null;

    private void Awake()
    {
        cube = transform.parent.GetComponent<Cube>();

        colliderMapList = new List<Dictionary<DirectionFlags, Collider>>();
        for (int i = 0; i < 9; i++)
        {
            colliderMapList.Add(new Dictionary<DirectionFlags, Collider>());
            Transform colliderContainer = transform.Find($"Colliders{i}");

            foreach (DirectionFlags dirType in typeof(DirectionFlags).GetEnumValues())
            {
                Transform colliderTrm = colliderContainer.Find($"Collider{dirType.ToString()}");
                if (colliderTrm != null)
                    if (colliderTrm.TryGetComponent(out Collider collider))
                        colliderMapList[i].Add(dirType, collider);
            }
        }
    }

    public void SetActiveCollider(int i, Vector3 dir, bool active)
    {
        //Debug.Log($"{i}, {dir}");
        if (directionMap.ContainsKey(dir) == false)
            return;

        // Debug.Log($"{i}, {dir} : Founded");
        SetActiveCollider(i, directionMap[dir], active);
    }

    public void SetActiveCollider(int i, DirectionFlags dirType, bool active)
    {
        if (colliderMapList.Count < i)
            return;

        if (colliderMapList[i].ContainsKey(dirType) == false)
            return;

        colliderMapList[i][dirType]?.gameObject.SetActive(active);
    }
}
