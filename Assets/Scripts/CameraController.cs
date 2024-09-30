using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _headPlayer;
    [SerializeField] private Transform _player;

    [SerializeField] private LayerMask _obstaclesMask;
    [SerializeField] private LayerMask _noPlayerMask;
    [SerializeField] private LayerMask _originalMask;

    [SerializeField] private float _speedX = 360f;
    [SerializeField] private float _speedY = 240f;
    [SerializeField] private float _limitY = 40f;
    [SerializeField] private float _minDistance = 1.5f;
    [SerializeField] private float _hideDistance = 2f;

    private float _maxDistance;
    private float _currentYRotation;

    private Vector3 _localPosition;

    private void Start()
    {
        _localPosition = _headPlayer.InverseTransformPoint(transform.position);

        _maxDistance = Vector3.Distance(transform.position, _headPlayer.position);

        _originalMask = _camera.cullingMask;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
         transform.position = _headPlayer.TransformPoint(_localPosition);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 newtarget = transform.position;
            newtarget.y = _player.position.y;
            _player.LookAt(newtarget);
        }

         Rotation();
         ObstaclesReaction();
         PlayerReaction();

        _localPosition = _headPlayer.InverseTransformPoint(transform.position);
    }
    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if(mouseY != 0)
        {
            float tmp = Mathf.Clamp(_currentYRotation + -mouseY * _speedY * Time.deltaTime, -_limitY, _limitY);

            if(tmp != _currentYRotation)
            {
                float rotate = tmp - _currentYRotation;
                transform.RotateAround(_headPlayer.position, transform.right,rotate);
                _currentYRotation = tmp;
            }
        }
        if (mouseX != 0)
        {
            transform.RotateAround(_headPlayer.position, transform.up, mouseX * _speedX * Time.deltaTime);
        }

        transform.LookAt(_headPlayer);  
    }
    private void ObstaclesReaction()
    {
        float distance = Vector3.Distance(transform.position, _headPlayer.position);
        RaycastHit hit;

        if (Physics.Raycast(_headPlayer.position, transform.position - _headPlayer.position, out hit, _maxDistance, _obstaclesMask))
        {
            transform.position = hit.point;
        }
        else if (distance < _maxDistance && !Physics.Raycast(transform.position, -transform.forward, .1f, _obstaclesMask))
        {
            transform.position -= transform.forward * .05f;
        }
    }
    private void PlayerReaction()
    {
        float distance = Vector3.Distance(transform.position, _headPlayer.position);

        if (distance < _hideDistance)
            _camera.cullingMask = _noPlayerMask;
        else
            _camera.cullingMask = _originalMask;
    }
}
