using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public bool completed = false;
    public Transform playerSpawnPoint;
    public AudioClip[] bgMusics;
    public AudioSource background;

    public NavMeshSurface surface;

    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        //surface.BuildNavMesh();
        int songToPlay = Random.Range(0, bgMusics.Length);
        background.clip = bgMusics[songToPlay];
        background.Play();
    }
}
