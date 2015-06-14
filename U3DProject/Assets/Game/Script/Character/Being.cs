using UnityEngine;
using System.Collections;

public class Being : MonoBehaviour {

    public string displayName = string.Empty;
    protected NameCard _nameCard;
    protected BeingStat _beingStat;

    public Animator animator;

    public float rotateSpeed = 3.0f;
    public Being target;
    public Being()
    {
    }
    public virtual void Start()
    {
        _nameCard = gameObject.AddComponent<NameCard>();
        _beingStat = gameObject.AddComponent<BeingStat>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            animator = gameObject.AddComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public T TryGetComponent<T>() where T:Component
    {
        T com = gameObject.GetComponent<T>();
        if (com != null)
            GameObject.Destroy(com);
        com = gameObject.AddComponent<T>();
        return com;
    }
    public void Unselect()
    {
        var select = GetComponent<SelectFlag>();
        if (select != null)
            GameObject.Destroy(select);
    }
    public void Select()
    {
        TryGetComponent<SelectFlag>();
    }
}
