using UnityEngine;
using System.Collections;

public class D2HeroCamera : MonoBehaviour
{
    /// <summary>
    ///俯角
    /// </summary>
    public float depression = 45;
    /// <summary>
    /// 相机与目标的距离
    /// </summary>
    public float distance = 5f;

    public readonly float minDistance = 1f;

    public float timeOfSmooth = 0.3f;

    private float _elapsedTime = 0;

    private Camera _camera;

    private Vector3 _lastpos = Vector3.zero;

    private bool _end = false;

    Animator _animator;

    Vector3 currentVelocity;

    Vector3 calCamearPos(Vector3 tarpos)
    {
        Vector3 dst = new Vector3();
        dst.x = tarpos.x;
        dst.y = tarpos.y + distance * Mathf.Sin(depression / 180f * Mathf.PI);
        dst.z = tarpos.z - distance * Mathf.Cos(depression / 180f * Mathf.PI);
        return dst;
    }

    void Start()
    {
        _camera = Camera.main;

        _animator = GetComponentInChildren<Animator>();

        _camera.transform.position = calCamearPos(_animator.rootPosition);

        _camera.transform.LookAt(_animator.rootPosition);

        currentVelocity = Vector3.zero;
    }
    void EndUpdate()
    {
        _end = true;
        _elapsedTime = 0;
        currentVelocity = Vector3.zero;
    }
    void BeginUpdate()
    {
        _end = false;
        _elapsedTime = 0;
    }
    void UpdateSetting()
    {
        bool update = false;
        {
            float delta = Input.GetAxis("Mouse ScrollWheel");
            if (delta < 0)
            {
                distance += 0.2f;
                update = true;
            }
            else if (delta > 0)
            {
                distance -= 0.2f;
                update = true;
            }
        }

        {
            float delta = Input.GetAxis("Vertical");
            if (delta != 0)
            {
                depression += delta;
                update = true;
            }
        }



        depression = Mathf.Clamp(depression, -90, 90);
        distance = Mathf.Clamp(distance, minDistance, 100);
        if (update)
            BeginUpdate();
    }
    void Update()
    {
        UpdateSetting();

        if (_lastpos != _animator.rootPosition && _end)
            BeginUpdate();

        if (!_end)
        {
            float t = _elapsedTime / timeOfSmooth;
            Vector3 vfrom = _camera.transform.position;
            Vector3 vto = calCamearPos(_animator.rootPosition);
            Vector3 dir=vto-vfrom;
            if (dir.sqrMagnitude < 0.1f)
                EndUpdate();
            else
            {
                _camera.transform.position = Vector3.SmoothDamp(vfrom, vto, ref currentVelocity, timeOfSmooth);

                Quaternion qfrom = _camera.transform.rotation;
                Quaternion qto = Quaternion.LookRotation(_animator.rootPosition - vto);
                _camera.transform.rotation = Quaternion.Slerp(qfrom, qto, t);
                _elapsedTime += Time.deltaTime;
            }
        }

        _lastpos = _animator.rootPosition;
    }
}
