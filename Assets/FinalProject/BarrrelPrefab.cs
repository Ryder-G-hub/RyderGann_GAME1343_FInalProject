using UnityEngine;
using UnityEngine.Rendering;

public class BarrrelPrefab : MonoBehaviour
{
    public int scoreGiven = 100;
    public float lifetime = 80;

    public bool scored = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void HitPlayer(int value)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("OilBarrel"))
        {
            Destroy(gameObject);
        }

    }
}
