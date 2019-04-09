using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public Formation current_formation;
    /*
     * For convention
     * The index in the formation represents the number of units that are not leaders.
     * For example, for an array a_f of size 3
     * a_f[2]: leader + 2 goons
     * a_f[1]: leader + 1 goon
     * a_f[0]: leader
     * */
    public Formation[] available_formations;
    public Transform squad_target;
    [SerializeField] private GameObject[] markers;
    [SerializeField] private EnemyStateController leader;
    private List<EnemyStateController> units;
    [SerializeField] private EnemyStats leader_stat, goon_stats;
    void Start()
    {
        leader.target = squad_target;
        leader.enemy_stats = leader_stat;
        if (available_formations.Length > 1) current_formation = available_formations[available_formations.Length - 1];

        RefreshCurrentUnits();
        UpdateUnitTargets();
    }
    float update_rate = 2f;
    float update_timer = 2f;
    void Update()
    {
        update_timer -= Time.deltaTime;
        if (update_timer <= 0)
        {
            update_timer = update_rate;
            //RefreshCurrentUnits();
            //UpdateUnitTargets();
            UpdateFormation();

        }
        Flock();

    }

    //Picks next formation of a unit dies. The idea is to call this before destroying an enemy
    public void UpdateFormation()
    {
        RefreshCurrentUnits();
        current_formation = available_formations[units.Count];
        if (units.Count == 0 && leader == null) Destroy(gameObject);
        UpdateUnitTargets();
        RefreshTarget();
       
    }

    private void RefreshTarget()
    {
        GameObject[] tgt = GameObject.FindGameObjectsWithTag("Player");

        float min = Mathf.Infinity;
        for (int i = 0; i < tgt.Length; i++)
        {
            float test = (tgt[i].transform.position - transform.position).magnitude;
            if (test <= min)
            {
                min = test;
                squad_target = tgt[i].transform;
            }
        }

    }
    void UpdateUnitTargets()
    {
        GameObject empty = new GameObject();
        if (markers != null)
        {
            for (int i = 0; i < markers.Length; i++)
            {
                Destroy(markers[i]);
            }
        }
        markers = new GameObject[current_formation.offset_from_lead.Length];
        for (int i = 0; i < markers.Length; i++)
        {
            markers[i] = current_formation.GenerateMarker(empty, (leader.transform.rotation * current_formation.offset_from_lead[i]) + leader.transform.position, Quaternion.identity, leader.transform);
            units[i].target = markers[i].transform;
        }
        if (leader != null) leader.target = squad_target;

        Destroy(empty);
    }


    void RefreshCurrentUnits()
    {
        if (leader == null && units != null && units.Count > 0)
        {
            leader = units[0];
            leader.target = squad_target;
            leader.enemy_stats = leader_stat;
        }
        units = new List<EnemyStateController>();
        EnemyStateController currentsc;

        foreach (Transform child in transform)
        {
            currentsc = child.GetComponent<EnemyStateController>();
            if (currentsc != null && currentsc != leader)
            {
                currentsc.enemy_stats = goon_stats;
                units.Add(currentsc);
            }
            currentsc.attack_target = squad_target;
        }

    }
    void Flock()
    {
       Vector3 cohesion = Flock(cohesionRadius, cohesionFactor);
        Vector3 repulsion = Flock(repulsionRadius, repulsionFactor);
        Debug.Log(cohesion+repulsion);
        transform.position += (cohesion) * Time.deltaTime;
        transform.position += (repulsion) * Time.deltaTime;
    }
    //flocking

    float repulsionRadius = 100f;
    float cohesionRadius = 125f;
    float repulsionFactor = -1f;
    float cohesionFactor = 0.5f;
    float speed = 3f;
    List<SquadController> squado = new List<SquadController>();
    public SquadController[] squids;
    public Vector3 vel;
     Vector3 Flock(float radius, float factor)
    {
         squids = FindObjectsOfType<SquadController>();
        Vector3 avg_pos = Vector3.zero;
        float count = 0;
        for (int i = 0; i < squids.Length;i++)
        {
            float dist = (squids[i].transform.position - transform.position).magnitude;
            if ( dist < radius)
            {
                count++;
                avg_pos += squids[i].transform.position;
            }
        }
        avg_pos /= count;

        vel = (avg_pos - transform.position) * factor;
        return vel;

    }
}