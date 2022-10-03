using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : Singleton<RespawnManager>
{
    public Collectable extinguisherPrefab, bucketPrefab;
    
    public void SpawnNewExtinguisher(Vector3 position, Quaternion rotation)
    {
        PositionAndRotation newPos = new PositionAndRotation(position, rotation);
        StartCoroutine(nameof(SpawnExtinguisherDelayed), newPos);
    }

    private IEnumerator SpawnExtinguisherDelayed(PositionAndRotation posAndRot)
    {
        yield return new WaitForSeconds(10f);
        Instantiate(extinguisherPrefab, posAndRot.position, posAndRot.rotation);
    }
    
    public void SpawnNewBucket(Vector3 position, Quaternion rotation)
    {
        PositionAndRotation newPos = new PositionAndRotation(position, rotation);
        StartCoroutine(nameof(SpawnBucketDelayed), newPos);
    }

    private IEnumerator SpawnBucketDelayed(PositionAndRotation posAndRot)
    {
        yield return new WaitForSeconds(10f);
        Instantiate(bucketPrefab, posAndRot.position, posAndRot.rotation);
    }
}

public struct PositionAndRotation
{
    public Vector3 position;
    public Quaternion rotation;

    public PositionAndRotation(Vector3 p, Quaternion r)
    {
        position = p;
        rotation = r;
    }

}
