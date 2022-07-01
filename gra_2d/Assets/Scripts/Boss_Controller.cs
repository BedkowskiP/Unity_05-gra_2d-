using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss_Controller : MonoBehaviour
{
    private float range;
    private float minDistance = 12f;
    
    public Transform target;
    private float nextWaypointDistance = 3f;
    private static Path path;
    private int currentWaypoint = 0;
    bool reachedEndOfpath = false;

    public GameObject spriter;
    public bool isRotating = false, facingRight = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        this.seeker = GetComponent<Seeker>();
        this.rb = GetComponent<Rigidbody2D>();
        this.InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void Update()
    {
        this.range = Vector3.Distance(this.transform.position, target.position);
        if (this.range < this.minDistance)
        {
            if (path == null) return;

            
            if(this.currentWaypoint >= path.vectorPath.Count)
            {
                this.reachedEndOfpath = true;
                return;
            } else {
                this.reachedEndOfpath = false;
            }

            Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * (Enemy.movement_speed);

            if(target.position.x > this.transform.position.x && facingRight) if(!isRotating) Flip();
            if(target.position.x < this.transform.position.x && !facingRight) if(!isRotating) Flip();
            if (force.x > 0.1f || force.x < 0.1f) Boss.anim.SetBool("Run", true); else Boss.anim.SetBool("Run", false);

            this.rb.velocity = force;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance) this.currentWaypoint++;
        }
    
    }

    void UpdatePath()
    {
        this.range = Vector3.Distance(this.transform.position, target.position);
        if (this.range < this.minDistance)
        {
            if(this.seeker.IsDone()) this.seeker.StartPath(this.transform.position, target.position, this.EndOfPath);
        }
    }

    void EndOfPath(Path p)
    {
        if (!p.error) {
            path = p;
            this.currentWaypoint = 0;
        }
    }

    public void Flip()
    {
        isRotating = true;

        StartCoroutine(Rotate180());

        facingRight = !facingRight;
    }
    IEnumerator Rotate180()
    {
        float amountRotated = 0f;
        while (amountRotated < 180f)
        {
            float frameRotation = 600 * Time.deltaTime;
            spriter.transform.Rotate(0, frameRotation, 0);
            amountRotated += frameRotation;
            yield return new WaitForEndOfFrame();
        }
        if (spriter.transform.rotation.y > 0) spriter.transform.eulerAngles = new Vector3(0, 180, 0);
        if (spriter.transform.rotation.y < 0) spriter.transform.eulerAngles = new Vector3(0, 0, 0);
        isRotating = false;
    }
}
