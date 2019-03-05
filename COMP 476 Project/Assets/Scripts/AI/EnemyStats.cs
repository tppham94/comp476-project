using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float movement_speed = 1.0f;
    public float look_range = 40.0f;
    public float look_sphere_cast_radius = 1.0f;
    public float maximum_velocity = 10.0f;

    public float attack_range = 1.0f;
    public float attack_rate = 1.0f;
    public float attack_force = 15.0f;
    public float attack_damage = 50.0f;

    public float search_duration = 4.0f;
    public float searching_turn_speed = 120.0f;
}

