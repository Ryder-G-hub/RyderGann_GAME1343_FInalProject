using UnityEngine;
using UnityEngine.InputSystem;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrelPrefab;
    public float spawnInterval = 3.0f;
    public bool spawnOnStart = true;

    private float spawnTimer;
    private Collider spawnerCollider;

    void Start()
    {
        spawnerCollider = GetComponent<Collider>();
        spawnTimer = spawnInterval;

        if (spawnOnStart)
        {
            SpawnBarrel();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (spawnOnStart && spawnInterval > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <=0)
            {
                SpawnBarrel();
                spawnTimer = spawnInterval;
            }
        }

        Keyboard kb = Keyboard.current;
        if (kb != null && kb.spaceKey.wasPressedThisFrame)
        {
            SpawnBarrel();
        }
    }

    void SpawnBarrel()
    {
        if (barrelPrefab == null)
        {
            return;
        }

        GameObject barrel = Instantiate(barrelPrefab, transform.position, barrelPrefab.transform.rotation);

        Collider barrelCollider = barrel.GetComponent<Collider>();
        if (spawnerCollider != null && barrelCollider != null)
        {
            Physics.IgnoreCollision(spawnerCollider, barrelCollider);
        }
    }
}
