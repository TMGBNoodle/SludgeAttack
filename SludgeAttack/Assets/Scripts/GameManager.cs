using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set;}

    public GameObject sludgeParent;

    public SludgeManager[] sludgeSpawners;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        sludgeSpawners = sludgeParent.transform.GetComponentsInChildren<SludgeManager>();
        InvokeRepeating("spawnSludge", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnSludge() {
        foreach (SludgeManager sludge in sludgeSpawners) {
            sludge.spawnSludge();
        }
    }
}
