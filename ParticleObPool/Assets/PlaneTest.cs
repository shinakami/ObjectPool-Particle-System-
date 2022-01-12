using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTest : MonoBehaviour
{
    public ParticleObjPool ParticleObjPool = ParticleObjPool.Instance;
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameObject obj =ParticleObjPool.SpawnFromPool("Star", transform.position, transform.rotation);
            StartCoroutine(ParticleObjPool.RecycleObject("Star", obj));
        }
    }

}
