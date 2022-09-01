using UnityEngine;

public class ApplyForce : MonoBehaviour 
{
    [SerializeField] private float _forceRange;
    private Camera _cam;
    private Rigidbody _rb; 
    private Vector3 _posOnScreen;
    private CubeSpawner _spawner;
    private float _screenOffset = 75f;
    [HideInInspector] public bool IsForceApplied;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _spawner = FindObjectOfType<CubeSpawner>();
    }

    private void Update()
    {
        UpdatePositionOnScreen();
        ApplyForceOnce();
        DestroyIfOffScreen();
        QuitApplication();
    }

    // Here I'm getting position of cube on screen
    private void UpdatePositionOnScreen() => _posOnScreen = _cam.WorldToScreenPoint(transform.position);

    // If you press space, then force would be applied in random direction and only once
    private void ApplyForceOnce()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsForceApplied)
        {
            _rb.velocity = new Vector3(Random.Range(-_forceRange, _forceRange), Random.Range(-_forceRange, _forceRange), 0f);
            IsForceApplied = true;
        }
    }

    // If cube is off screen, it must be destroyed and removed from list
    private void DestroyIfOffScreen()
    {
        if (!IsOnScreen())
        {
            Destroy(gameObject);
            _spawner.Cubes.Remove(gameObject);
        }
    }

    // Just checking, if cube's position is on screen or not
    private bool IsOnScreen() 
    {
        return _posOnScreen.x > -_screenOffset && _posOnScreen.x < Screen.width + _screenOffset && _posOnScreen.y > -_screenOffset && _posOnScreen.y < Screen.height + _screenOffset;  
    }

    // Just quiting application
    private void QuitApplication() {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }
}
