﻿
using UnityEngine;
using System.Collections.Generic;

public static class ParticlePool
{
    const int DEFAULT_POOL_SIZE = 3;

    /// <summary>
    /// The Pool class represents the pool for a particular prefab.
    /// </summary>
    class Pool
    {
        Transform m_sRoot = null;

        //list prefab ready
        List<ParticleSystem> inactive;

        // The prefab that we are pooling
        ParticleSystem prefab;

        int index;

        // Constructor
        public Pool(ParticleSystem prefab, int initialQty, Transform parent)
        {
            m_sRoot = parent;
            this.prefab = prefab;
            inactive = new List<ParticleSystem>(initialQty);

            for (int i = 0; i < initialQty; i++)
            {
                ParticleSystem particle = (ParticleSystem)GameObject.Instantiate(prefab, m_sRoot);
                particle.Stop();
                inactive.Add(particle);
            }
        }

        public int Count {
            get { return inactive.Count;}
        }

        // Spawn an object from our pool
        public void Play(Vector3 pos, Quaternion rot)
        {
            index = index + 1 < inactive.Count ? index + 1 : 0;
            ParticleSystem obj = inactive[index];

            obj.transform.SetPositionAndRotation( pos, rot);
            obj.Play();
        }

        public void Release() {
            while(inactive.Count > 0) {
                GameObject.DestroyImmediate(inactive[0]);
                inactive.RemoveAt(0);
            }
            inactive.Clear();
        }
    }

    //--------------------------------------------------------------------------------------------------

    // All of our pools
    static Dictionary<int, Pool> pools = new Dictionary<int, Pool>();

    /// <summary>
    /// Init our dictionary.
    /// </summary>
    static void Init(ParticleSystem prefab = null, int qty = DEFAULT_POOL_SIZE, Transform parent = null)
    {
        if (prefab != null && !pools.ContainsKey(prefab.GetInstanceID()))
        {
            pools[prefab.GetInstanceID()] = new Pool(prefab, qty, parent);
        }
    }

    static public void Preload(ParticleSystem prefab, int qty = 1, Transform parent = null)
    {
        Init(prefab, qty, parent);
    }

    static public void Play(ParticleSystem prefab, Vector3 pos, Quaternion rot)
    {
        pools[prefab.GetInstanceID()].Play(pos, rot);
    }

    static public void Release(ParticleSystem prefab)
    {
        if (pools.ContainsKey(prefab.GetInstanceID()))
        {
            pools[prefab.GetInstanceID()].Release();
            pools.Remove(prefab.GetInstanceID());
        }
        else
        {
            GameObject.DestroyImmediate(prefab);
        }
    }
}

[System.Serializable]
public class ParticleAmount
{
    public ParticleSystem prefab;
    public int amount;
    public Transform root;
}