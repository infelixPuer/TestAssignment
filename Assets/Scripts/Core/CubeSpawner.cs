using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour 
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private SettingsSO _settings;
    
    private Vector2[] _coordinates;
    private float _circleLenght;
    private float _distanceOnCircle;
    private float _radInitial;

    public List<GameObject> Cubes = new List<GameObject>();

    private void Awake() 
    {
        _coordinates = new Vector2[_settings.CubeNumber];
        CalculateCircleLength();
        CalculateDistanceBetweenCubesOnCircle();
    }

    private void Start() 
    {
        CalculateCoordinatesForCubes();
        SpawnCubes();
    }    

    // Here I'm just calculating circle lenght from formule L = 2 * R * PI
    private void CalculateCircleLength() => _circleLenght = 2 * _settings.CircleRadius * Mathf.PI; 

    // Calculating how far away are cubes from each other on circle
    private void CalculateDistanceBetweenCubesOnCircle() => _distanceOnCircle = _circleLenght / _settings.CubeNumber;

    private void CalculateCoordinatesForCubes() 
    {    
        // Firstly, I'm getting initial angle in radians between start point and first cube
        _radInitial = _distanceOnCircle / _settings.CircleRadius;
        float rad = _radInitial;

        // Secondly, from cosinus property I'm getting the x and y coordinates for all cubes
        for (int i = 0; i < _settings.CubeNumber; i++)
        {
            _coordinates[i].x = Mathf.Cos((Mathf.PI / 2) - rad) * _settings.CircleRadius;
            _coordinates[i].y = Mathf.Cos(rad) * _settings.CircleRadius;

            rad += _radInitial;
        }
    }

    private void SpawnCubes() 
    {
        float rad = _radInitial;

        // Here I'm spawing cubes, adding them to Cubes list for future reference and with the help of radians I'm giving them needed rotation
        for (int i = 0; i < _settings.CubeNumber; i++)
        {
           Cubes.Add(Instantiate(_cubePrefab, (Vector3)_coordinates[i], Quaternion.Euler(0f, 0f, -(rad * 180) / Mathf.PI), transform));
            rad += _radInitial;
        }
    }
}
