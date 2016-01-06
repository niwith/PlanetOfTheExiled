using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class enemyAI : MonoBehaviour {

    public string aiType = "aos";
    public float viewDist = 50;
    public Transform target;
    public List<GameObject> playUnits;
    public List<float> playUnitsDist;
    private NavMeshAgent agent;
    public bool canAttack = false;
    public float attackDist = 10;
    public float attackDamage = 10;
    public float attackGap = 1;
    public float timeTilAttack = 0;
    public objectList objList;

	// Use this for initialization
	void Start () {
        if(objList == null)
        {
            objList = GameObject.FindGameObjectWithTag("objectLister").GetComponent<objectList>();
        }
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDist;
    }

    public void Attack()
    {
        target.gameObject.SendMessage("ApplyDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        timeTilAttack = attackGap;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiType == "aos")
        {
            if (target != null)
            {
                float distCur = (Vector3.Distance(transform.position, target.transform.position));
                if (attackDist > distCur && canAttack)
                {
                    Attack();
                }
                if (distCur <= viewDist)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    target = null;
                }
            }

            else if (target == null)
            {
                float minDist = viewDist;
                for(int i = 0; i < objList.playerUnits.Count; i++)
                {
                    if (objList.playerUnits[i] != null)
                    {
                        float curDist = Vector3.Distance(this.transform.position, objList.playerUnits[i].transform.position);
                        if (minDist > curDist)
                        {
                            minDist = curDist;
                            target = objList.playerUnits[i].transform;
                        }
                    }
                }
         
            }
            
            else
            {
                target = null;
            }
        }
        if(!canAttack)
        {
            timeTilAttack -= Time.deltaTime;
            if(timeTilAttack < 0)
            {
                canAttack = true;
            }
        }
    }
}

// CODE BIN
//      foreach(GameObject unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
//      {
//         playUnits.Add(unit);
//      }
//    for (int i = 0; i < objList.playerUnits.Count; i++)
//    {
//        playUnitsDist.Add(Vector3.Distance(objList.playerUnits[i].transform.position, transform.position));
//     }
//     for (int o = 0; o < playUnitsDist.Count; o++)
//     {
//         if (playUnitsDist.Min() == playUnitsDist[o])
//         {
//            target = playUnits[o].transform;
//         }
//     }
//     playUnits.Clear();
//     playUnitsDist.Clear();