using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachable : MonoBehaviour
{
    public bool isAttached = false;
    public Player player;
    public float distance;

    private Transform oldParent;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("NoPlayerFound");
        }
    }

    private void Update()
    {
        distance = Mathf.Abs((player.gameObject.transform.position - transform.position).magnitude);   
        if (distance < 30f && !player.isSomethingAttached)
        {
            if(Input.GetKeyDown(KeyCode.E) && !isAttached)
            {
                Attach();
            } 
        }
        if(isAttached && Input.GetKeyUp(KeyCode.E))
        {
            Dettach();
        }
    }

    public void Attach()
    {
        oldParent = gameObject.transform.parent;
        gameObject.transform.parent = player.gameObject.transform;
        isAttached = true;
        player.isSomethingAttached = true;

    }
    public void Dettach()
    {
        gameObject.transform.parent = oldParent;
        isAttached = false;
        player.isSomethingAttached = false;
    }
}
