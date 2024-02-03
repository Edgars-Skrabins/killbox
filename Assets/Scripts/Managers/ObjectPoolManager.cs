using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [Tooltip("If true then this script wont be destroyed on load")]
    [SerializeField] private bool dontDestroyOnLoad; // If true then this script wont be destroyed on load

    [Serializable]
    public class ObjectPool
    {
        public string name; // The name of the pool for organization and for when calling an object from outside this script
        [Space(10)]
        [HideInInspector] public List<GameObject> pooledObjects;
        [Tooltip("The object that will be pooled")]
        public GameObject pooledObject; //The pooled object
        [Tooltip("The amount of times the object will be pooled")]
        public int pooledAmount; //The amount of objects in this pool
        [Tooltip("If willGrow is on then the list will dynamically grow when out of objects to call")]
        public bool willGrow; // If the list is dynamically going to get bigger or not
        [Tooltip("If dontDestroyOnLoad is on then the object this class is on and the pooled objects wont be destroyed when changing scenes")]
        public bool dontDestroyOnLoad;

    }

    public ObjectPool[] objectPools;

    protected override void Awake()
    {
        
        base.Awake();
        
        if (dontDestroyOnLoad)
        {
            dontDestroyOnLoad = true;
        }
    }

    private void Start()
    {
        // ----- Makes a list for each of the array list variables ----- 

        for (int i = 0; i < objectPools.Length; i++)
        {
            objectPools[i].pooledObjects = new List<GameObject>();
        }

        // ----- Instantiates objects,gives them the DontDestroyOnLoad attribute and adds them to the list -----

        for (int i = 0; i < objectPools.Length; i++)
        {
            for (int t = 0; t < objectPools[i].pooledAmount; t++)
            {

                GameObject obj = Instantiate(objectPools[i].pooledObject);

                if (objectPools[i].dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(obj);
                }

                obj.SetActive(false);
                objectPools[i].pooledObjects.Add(obj);
            }
        }
    }

    /// <summary> 
    /// Use this function when you need to get an object from one of the object pools
    /// </summary>
    /// <param name="name"> Name of the pool array indexed class that you can find in the inspector </param>
    /// <returns> Returns a GameObject that is not active and enabled in the given object pool</returns>
    /// <example> GameObject obj = PoolerScript.Pooler.GetPooledObject(); </example>
    /// 
    public GameObject GetPooledObject(string name)
    {
        // ----- Returns a GameObject -----
        ObjectPool op = Array.Find(objectPools, ObjectPools => ObjectPools.name == name);

        for (int i = 0; i < op.pooledObjects.Count; i++)
        {
            if (!op.pooledObjects[i].activeInHierarchy)
            {
                return op.pooledObjects[i];
            }
        }

        // ----- Spawns a new object if willgrow is true and if there are not enough objects in the list -----

        if (op.willGrow)
        {
            GameObject obj = Instantiate(op.pooledObject);
            op.pooledObjects.Add(obj);
            return obj;
        }

        // If willGrow is false and there isnt enough objects it returns null to not cause an error
        return null;
    }

}

