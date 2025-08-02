using System;
using System.Collections.Generic;
using UnityEngine;

public class SeedParticles : MonoBehaviour
{
    public static Action<Vector3[]> onSeedCollided;
    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionAmount = ps.GetCollisionEvents(other, collisionEvents);
        
        Vector3[] collisionPosition = new Vector3[collisionAmount];

        for (int i = 0; i < collisionAmount; i++)
        {
            collisionPosition[i] = collisionEvents[i].intersection;
        }
        onSeedCollided?.Invoke(collisionPosition);

    }
}
