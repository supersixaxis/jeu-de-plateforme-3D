using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Range(0.5f,50)]
    public float detectDistance =3;
    public Transform[] points;
    int destinationIndex = 0;
    NavMeshAgent agent;
    Transform player;
   // float runSpeed = 2.5f;
  

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        if(agent != null){
            agent.destination = points[destinationIndex].position;
        }
    }
    private void Update()
    {
     Walk();
     SearchPlayer();
     SetMobSize();
    }
    private void SetMobSize()
    {
        if (gameObject.tag == "scalemob"){
             if (Vector3.Distance(transform.position, player.position) <= detectDistance + 2){
                iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
        }
        }
       
    }

    public void Walk(){
           float dist = agent.remainingDistance;
        if(dist <= 0.05f){
            destinationIndex++;
            if(destinationIndex > points.Length -1)
            destinationIndex = 0;
        }
        agent.destination = points[destinationIndex].position;
    }

    public void SearchPlayer(){
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= detectDistance){
            agent.destination = player.position;
        } else {
              agent.destination = points[destinationIndex].position;
        }
    }
    private void OnDrawGizmosSelected()
    {
        // fonction qui montre la distance de detection du monstre ( sinon ce n'est pas visible sans cette fonction)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
