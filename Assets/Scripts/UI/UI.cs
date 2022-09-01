using UnityEngine;
using TMPro;

public class UI : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _cubeNumberText;
    private CubeSpawner _spawner;

    private void Awake() {
        _spawner = FindObjectOfType<CubeSpawner>();
    }

    private void Update()
    {
        UpdateCubeNumberText();
    }

    // Just updating cube number text with the help of public Cubes list
    private void UpdateCubeNumberText() => _cubeNumberText.text = "Cubes on screen: " + _spawner.Cubes.Count;
}
