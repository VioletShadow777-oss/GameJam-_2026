using System.Collections;
using UnityEngine;
using Lean.Pool;

public class PoliceExplosionDespawn : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ExplosionDespawnCoroutine());
    }

    // Despawning Coroutine
    IEnumerator ExplosionDespawnCoroutine()
    {
        yield return new WaitForSeconds(2);
        LeanPool.Despawn(transform);
    }
}
