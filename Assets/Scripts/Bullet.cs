using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (_target != null && _target.CompareTag("Enemy"))
        {
            EnemyController enemy = _target.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Debug.Log("Damage Called");
                enemy.TakeDamage(damage);
                
            }
        }

        Destroy(gameObject);
    }

}
