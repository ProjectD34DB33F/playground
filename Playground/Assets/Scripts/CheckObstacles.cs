using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacles : MonoBehaviour
{
    [SerializeField] private List<isObstacle> currentObstacles;
    [SerializeField] private List<isObstacle> alreadyTransparent;
    [SerializeField] private Transform player;
    new Transform camera;

    private void Awake()
    {
        currentObstacles = new List<isObstacle>();
        alreadyTransparent = new List<isObstacle>();

        camera = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetObstacles();
        MakeObstaclesOpaque();
        MakeObstaclesTransparent();
    }

    private void GetObstacles()
    {
        currentObstacles.Clear();

        float maxDistance = Vector3.Magnitude(camera.position - player.position);

        Ray rayForward = new Ray(camera.position, player.position - camera.position);
        Ray rayBackward = new Ray(camera.position, camera.position - player.position);

        var hitsForward = Physics.RaycastAll(rayForward, maxDistance);
        var hitsBackward = Physics.RaycastAll(rayBackward, maxDistance);

        foreach (var hit in hitsForward)
        {
            if (hit.collider.gameObject.TryGetComponent(out isObstacle obstacle))
                if (!currentObstacles.Contains(obstacle))
                    currentObstacles.Add(obstacle);
        }
        foreach (var hit in hitsBackward)
        {
            if (hit.collider.gameObject.TryGetComponent(out isObstacle obstacle))
                if (!currentObstacles.Contains(obstacle))
                    currentObstacles.Add(obstacle);
        }
    }

    private void MakeObstaclesTransparent()
    {
        for (int i = 0; i < currentObstacles.Count; i++)
        {
            isObstacle obstacle = currentObstacles[i];

            if (!alreadyTransparent.Contains(obstacle))
            {
                obstacle.TurnInvisible();
                alreadyTransparent.Add(obstacle);
            }

        }
    }

    private void MakeObstaclesOpaque()
    {
        for (int i = 0; i < alreadyTransparent.Count; i++)
        {
            isObstacle notObstacle = alreadyTransparent[i];

            if (!currentObstacles.Contains(notObstacle))
            {
                notObstacle.TurnOpaque();
                alreadyTransparent.Remove(notObstacle);
            }
        }
    }
}