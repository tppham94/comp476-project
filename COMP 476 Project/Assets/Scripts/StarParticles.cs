using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticles : MonoBehaviour
{
    //Getting the current transformation 
    Transform currentTransform;
    //Deals with the direction,color, position, rotation etc.
    ParticleSystem.Particle[] stars;
    public int totalStars = 150;
    public float sizeOfEachStars = 1f;
    public float distanceBetweenStars = 30f;
    public float distanceFromPlayer = 5f;

    //Magnitude of a square
    float starDistanceSquareValue;
    float starDistanceFromPlayerSquareValue;
    // Start is called before the first frame update
    void Start()
    {
        starDistanceSquareValue = distanceBetweenStars * distanceBetweenStars;
        starDistanceFromPlayerSquareValue = distanceFromPlayer * distanceFromPlayer;
        currentTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //If there is no star when starting the game it will generate
        if(stars == null)
        {
            GenerateStars();
        }

        //For loop will go through each star and calculate to get the position between stars in a unitsphere
        //and another one for distance between the star and the player while setting the color and the size of it 
        for (int i = 0; i < totalStars; i++)
        {
            if((stars[i].position - currentTransform.position).sqrMagnitude > starDistanceSquareValue)
            {
                stars[i].position = Random.insideUnitSphere.normalized * distanceBetweenStars + currentTransform.position;
            }

            if((stars[i].position - currentTransform.position).sqrMagnitude <= starDistanceFromPlayerSquareValue)
            {
                float percent = (stars[i].position - currentTransform.position).sqrMagnitude / starDistanceFromPlayerSquareValue;
                stars[i].startColor = new Color(1, 1, 1, percent);
                stars[i].startSize = percent * sizeOfEachStars;
            }

            GetComponent<ParticleSystem>().SetParticles(stars, stars.Length);
        }
    }

    //Method to generate the total stars
    private void GenerateStars()
    {
        //This deals with the position,rotation,color,velocity etc
        stars = new ParticleSystem.Particle[totalStars];
        //Loop will go through each stars
        for (int i = 0; i < totalStars; i++)
        {
            //Spawns within the unitsphere in consideration of the current transform
            stars[i].position = Random.insideUnitSphere * distanceBetweenStars + currentTransform.position;
            //White color
            stars[i].startColor = new Color(1, 1, 1, 1);
            //Set the size of stars
            stars[i].startSize = sizeOfEachStars;
        }
    }
}
