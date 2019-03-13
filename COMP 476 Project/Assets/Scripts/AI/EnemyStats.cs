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

    //for steering arrive
    public float slowdown_radius = 2f;
    public float arrival_radius = 4f;
    public float maximum_acceleration = 5f;
    public float time_to_target = .15f;

    //turning arrive
    public float maximum_angular_velocity = 10f;
    public float maximum_angular_acceleration = 5f;
    public float angular_slowdown = 90f;
    public float angular_arrival = 5f;

    //obstacle avoidance
    public float whisker_length = 30f; //raycast distance
}

