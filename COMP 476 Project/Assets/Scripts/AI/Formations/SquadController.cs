using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SquadController : MonoBehaviour
{
    public Formation current_formation;
    public Formation v_formation;
    public Transform squad_target;
    private GameObject[] markers;
    [SerializeField]private EnemyStateController leader;
    private List<EnemyStateController> units;
    [SerializeField] private EnemyStats leader_stat, goon_stats;
    void Start()
    {
        leader.target = squad_target;
        leader.enemy_stats = leader_stat;
        RefreshCurrentUnits();
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
        for(int i = 0; i < current_formation.offset_from_lead.Length; i++)
        {
            markers[i] = current_formation.GenerateMarker(empty, i, Quaternion.identity, leader.transform);
            units[i].target = markers[i].transform;
        }
    }

    void RefreshCurrentUnits()
    {
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
