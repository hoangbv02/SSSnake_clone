using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public int amountToPool = 50;
    public GameObject[] prefabs;

    List<GameObject> pooledObjects = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            for (int j = 0; j < amountToPool; j++)
            {
                GameObject obj = Instantiate(prefabs[i]);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
