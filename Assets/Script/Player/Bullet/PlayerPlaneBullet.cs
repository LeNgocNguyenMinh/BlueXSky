using System.Collections;
using UnityEngine;

public class PlayerPlaneBullet : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float flyTime;
    [SerializeField]private ParticleSystem hitParticle;
    [SerializeField]private Transform hitPoint;
    private float count;
    private float speed;
    private float damage;
    public void SetValue(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
        count = flyTime;
    }
    void FixedUpdate()
    {
        if(count <= 0f)
        {
            gameObject.SetActive(false);
            return;
        }
        else{
            count -= Time.fixedDeltaTime;
        }
        rb.MovePosition(
            rb.position + Vector2.up * speed * Time.fixedDeltaTime
        );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossHealthControl>().BossHurt(damage);
            GameObject tmp = ObjectPooling.Instance.GetPooledObject(hitParticle.gameObject);
            if(tmp != null)
            {
                tmp.transform.position = hitPoint.position;
                tmp.SetActive(true);
                tmp.GetComponent<ParticleSystem>().Play();
            }
            gameObject.SetActive(false);
        }
    }
}
