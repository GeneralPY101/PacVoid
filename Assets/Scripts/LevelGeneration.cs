using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        StartCoroutine(GenerateLevel());
    }
    IEnumerator GenerateLevel()
    {
        yield return new WaitForSeconds(1f);
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, Quaternion.identity);
    }
}
