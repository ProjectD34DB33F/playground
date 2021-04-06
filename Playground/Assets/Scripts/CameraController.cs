using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float pitch = 1f;

    private List<IsObstacle> currentObstacles;
    private List<IsObstacle> alreadyTransparent;
    new Transform camera;

    private void Awake()
    {
        currentObstacles = new List<IsObstacle>();
        alreadyTransparent = new List<IsObstacle>();

        camera = this.gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetObstacles();
        MakeObstaclesOpaque();
        MakeObstaclesTransparent();
    }

    private void LateUpdate()
    {
        transform.position = player.position - offset;
        transform.LookAt(player.position + Vector3.up * pitch);
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
            if (hit.collider.gameObject.TryGetComponent(out IsObstacle obstacle))
                if (!currentObstacles.Contains(obstacle))
                    currentObstacles.Add(obstacle);
        }
        foreach (var hit in hitsBackward)
        {
            if (hit.collider.gameObject.TryGetComponent(out IsObstacle obstacle))
                if (!currentObstacles.Contains(obstacle))
                    currentObstacles.Add(obstacle);
        }
    }

    private void MakeObstaclesTransparent()
    {
        for (int i = 0; i < currentObstacles.Count; i++)
        {
            IsObstacle obstacle = currentObstacles[i];

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
            IsObstacle notObstacle = alreadyTransparent[i];

            if (!currentObstacles.Contains(notObstacle))
            {
                notObstacle.TurnOpaque();
                alreadyTransparent.Remove(notObstacle);
            }
        }
    }
}