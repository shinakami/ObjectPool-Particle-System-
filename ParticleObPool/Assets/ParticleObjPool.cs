using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ParticleObjPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public Queue<GameObject> ObjectPool;

    private GameObject _objectToSpawn;
    #region Singleton 
    public static ParticleObjPool Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in Pools)
        {
            ObjectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                ObjectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.Tag, ObjectPool);
        }
    }
    /// <summary>
    /// For ParticleSystem
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IEnumerator RecycleObject(string tag, GameObject obj)
    {
        float duration = obj.GetComponent<ParticleSystem>().main.duration;
        while (duration > 0)
        {
            duration--;
            yield return new WaitForSeconds(1);
        }
        EnqueueObject(tag, obj);

    }

    private void EnqueueObject(string tag, GameObject obj)
    {
        obj.SetActive(false);

        PoolDictionary[tag].Enqueue(obj);
    }

    private void AddObject(string tag, GameObject obj)
    {
        GameObject addobj = Instantiate(obj);
        PoolDictionary[tag].Enqueue(addobj);
    }



    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag: " + tag + " doesn't excist");
            return null;
        }



        _objectToSpawn = PoolDictionary[tag].Dequeue();
        if (PoolDictionary[tag].Count < 1)
        {
            AddObject(tag, _objectToSpawn);
        }
        _objectToSpawn.SetActive(true);
        _objectToSpawn.transform.position = position;
        _objectToSpawn.transform.rotation = rotation;



        return _objectToSpawn;
    }
}
