using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PoolPreparer : MonoBehaviour
{
    [SerializeField]
    PooledMonobehaviour[] prefabs;

    [SerializeField]
    private int initialPoolSize = 100;

    private void Awake()
    {
        foreach (var prefab in prefabs)
        {
            if (prefab == null)
            {
                Debug.LogError("Null prefab in PoolPreparer");
            }
            else
            {
                PooledMonobehaviour poolablePrefab = prefab.GetComponent<PooledMonobehaviour>();
                if (poolablePrefab == null)
                {
                    Debug.LogError("Prefab does not contain an IPoolable and can't be pooled");
                }
                else
                {
                    Pool.GetPool(poolablePrefab).GrowPool();
                }
            }
        }
    }

    private void OnValidate()
    {
        List<GameObject> prefabsToRemove = new List<GameObject>();
        foreach (var prefab in prefabs.Where(t => t != null))
        {
            if (PrefabUtility.GetPrefabType(prefab) != PrefabType.Prefab)
            {
                Debug.LogError(string.Format("{0} is not a prefab.  It has been removed.", prefab.gameObject.name));
                prefabsToRemove.Add(prefab.gameObject);
            }

            PooledMonobehaviour poolablePrefab = prefab.GetComponent<PooledMonobehaviour>();
            if (poolablePrefab == null)
            {
                Debug.LogError("Prefab does not contain an IPoolable and can't be pooled.  It has been removed.");
                prefabsToRemove.Add(prefab.gameObject);
            }
        }

        prefabs = prefabs
            .Where(t => t != null && prefabsToRemove.Contains(t.gameObject) == false)
            .ToArray();
    }
}