using UnityEngine;
using System.Collections;

public class D2HeroCamera : MonoBehaviour
{
    public int xOffset = 0;
    public int yOffset = 6;
    public int zOffset = 6;

    public float timeOfSmooth = 3f;

    private float _elapsedTime = 0;

    private Camera _camera;

    private Vector3 _lastpos = Vector3.zero;

    private bool _end = false;

    Animator _animator;
    void Start()
    {
        _camera = Camera.main;
        _animator = GetComponentInChildren<Animator>();
        _camera.transform.position = new Vector3(
            _animator.bodyPosition.x + xOffset
            , _animator.bodyPosition.y + yOffset
            , _animator.bodyPosition.z + zOffset
        );
        _camera.transform.LookAt(_animator.bodyPosition);
    }
    void EndUpdate()
    {
        _end = true;
        _elapsedTime = 0;
    }
    void BeginUpdate()
    {
        _end = false;
        _elapsedTime = 0;
    }
    void Update()
    {
        if (_lastpos != _animator.bodyPosition && _end)
            BeginUpdate();

        if (!_end)
        {
            float t = _elapsedTime / timeOfSmooth;
            Vector3 vfrom = _camera.transform.position;
            Vector3 vto = _animator.bodyPosition + new Vector3(xOffset, yOffset, zOffset);

            _camera.transform.position = Vector3.Slerp(vfrom, vto, t);

            Quaternion qfrom = _camera.transform.rotation;
            Quaternion qto = Quaternion.LookRotation(_animator.bodyPosition - vto);
            _camera.transform.rotation = Quaternion.Slerp(qfrom, qto, t);

            if (_elapsedTime >= timeOfSmooth)
                EndUpdate();

            _elapsedTime += Time.deltaTime;

        }

        _lastpos = _animator.bodyPosition;
    }
}
