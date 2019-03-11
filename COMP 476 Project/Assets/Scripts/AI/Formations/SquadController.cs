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
    public Formation []available_formations;
    public Transform squad_target;
    [SerializeField] private GameObject[] markers;
    [SerializeField]private EnemyStateController leader;
    private List<EnemyStateController> units;
    [SerializeField] private EnemyStats leader_stat, goon_stats;
    void Start()
    {
        leader.target = squad_target;
        leader.enemy_stats = leader_stat;
        if (available_formations.Length > 1) current_formation = available_formations[available_formations.Length - 1];
        Debug.Log(available_formations.Length);
        RefreshCurrentUnits();
        UpdateUnitTargets();
    }

 void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //RefreshCurrentUnits();
            //UpdateUnitTargets();
            UpdateFormation();
        }
    }

    //Picks next formation of a unit dies. The idea is to call this before destroying an enemy
    public void UpdateFormation()
    {
        RefreshCurrentUnits();
        current_formation = available_formations[units.Count];
        if (units.Count == 0 && leader == null) Destroy(gameObject); 
        UpdateUnitTargets();
    }
    void UpdateUnitTargets()
    {
      GameObject  empty = new GameObject();
        if (markers != null)
        {
            for(int i = 0; i < markers.Length; i++)
            {
                Destroy(markers[i]);
            }
        }
        markers = new GameObject[current_formation.offset_from_lead.Length];
        for(int i = 0; i < markers.Length; i++)
        {
          //  markers[i] = current_formation.GenerateMarker(empty, (leader.transform.rotation *leader.transform.position) + current_formation.offset_from_lead[i], Quaternion.identity, leader.transform);
            markers[i] = current_formation.GenerateMarker(empty, (leader.transform.rotation  * current_formation.offset_from_lead[i])+leader.transform.position, Quaternion.identity, leader.transform);
            units[i].target = markers[i].transform;
        }
        Destroy(empty);
    }

    
    void RefreshCurrentUnits()
    {
        if (leader == null && units!=null && units.Count > 0)
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
        }

    }
}
