using System.Collections.Generic;
using UnityEngine;

public class SludgeManager : MonoBehaviour
{
    public GameObject sludgePrefab;

    List<GameObject> activeSludge = new List<GameObject>();

    Queue<GameObject> inactiveSludge = new Queue<GameObject>();
    public void spawnSludge() {
        if (inactiveSludge.Count == 0) {
            GameObject newSludge = Instantiate(sludgePrefab);
            newSludge.transform.position = gameObject.transform.position;
            activeSludge.Add(newSludge);
        } else {
            GameObject newSludge = inactiveSludge.Dequeue();
            newSludge.transform.position = gameObject.transform.position;
            newSludge.SetActive(true);
            activeSludge.Add(newSludge);
        }
    }

    public void updateSludge(GameObject changed) {
        changed.SetActive(false);
        inactiveSludge.Enqueue(changed);
        activeSludge.Remove(changed);
    }
}
