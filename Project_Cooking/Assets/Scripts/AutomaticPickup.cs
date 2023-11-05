using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticPickup : MonoBehaviour
{
    private Transform playerTransform;
   private float speed = 3f;
    private ProgressBar progressBar;
    private void Awake()
    {
        //look for player!
        playerTransform = FindObjectOfType<InteractionDetector>().gameObject.transform;
        if(!playerTransform)
        {
            Debug.Log("couldnt find a player on scene");
        }
        progressBar = FindObjectOfType<ProgressBar>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, playerTransform.position);
        if (dist > 1f)
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * (speed / 2));
        else if (dist < 1f && dist > 0.125f)
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * (speed));
        else
        {
            if (progressBar)
                progressBar.Increase(3);
            Destroy(this.gameObject);

        }
    }
}
