using UnityEngine;

public class SphereCollision : MonoBehaviour 
{
    [SerializeField] private CubeSpawner _spawner;
    private ApplyForce _applyForce;

    private void Awake() 
    {
        _applyForce = FindObjectOfType<ApplyForce>();
        _spawner = FindObjectOfType<CubeSpawner>();
    }

    private void Update() 
    {
        // Only when the force is applied calculations of collisions must start
        if (_applyForce.IsForceApplied) 
        {
            CalculateCollisions();
        }
    }

    private void CalculateCollisions() 
    {
        // Here I'm making a check for every cube, if there is a collision between it and other cube from the list
        for (int i = 0; i < _spawner.Cubes.Count; i++) 
        {
            for (int j = 0; j < _spawner.Cubes.Count; j++)
            {
                // If it checks for collision with it's self, then it must be skipped
                if (i == j) { continue; }
                
                // If two cubes are intersecting with each other like if they were spheres, then they must be destroyed and removed from the list
                if (IsIntersecting(_spawner.Cubes[i], _spawner.Cubes[j])) 
                {
                    Destroy(_spawner.Cubes[i]);
                    Destroy(_spawner.Cubes[j]);
                    _spawner.Cubes.RemoveAt(i);
                    _spawner.Cubes.RemoveAt(j - 1);
                    return;
                }
            }
        }
    }

    // Here is a simple check, if two cubes are intersecting each other like if they were spheres and sum of their radiuses equaled 1.25
    private bool IsIntersecting(GameObject obj1, GameObject obj2) => Vector3.Distance(obj1.transform.position, obj2.transform.position) < 1.25f;
}
