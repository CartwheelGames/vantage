using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private float radius = 0f;
    public float Radius
    {
        get
        {
            return Radius;
        }
    }
}
