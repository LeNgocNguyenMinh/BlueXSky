using UnityEngine;

public class SBBulletType1 : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private GameObject destroyPartical;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private TrailRenderer trailRenderer;
    [SerializeField]private Material hitBackMaterial;
    [SerializeField]private Material hitBackBreakMaterial;
    private Vector2 target;
    private float speed;
    private float damage;
    private bool isHitBackBullet;

    public void SetValue(float posX, float speed, float damage,bool isHitBackBullet)
    {
        this.isHitBackBullet = isHitBackBullet;
        this.damage = damage;
        this.speed = speed;
        this.isHitBackBullet = isHitBackBullet;
        target = new Vector2(posX, -Camera.main.orthographicSize);
        if(isHitBackBullet)
        {
            spriteRenderer.material = hitBackMaterial;
            trailRenderer.material = hitBackMaterial;
        }
    }
    void FixedUpdate()
    {
        if(Vector3.Distance(rb.position, target) < 0.01f)
        {
            rb.linearVelocity = Vector2.zero;
            ParticalPlay();
            Destroy(gameObject);
            return;
        }
        rb.MovePosition(
            Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime)
        );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerPlane"))
        {
            if(isHitBackBullet)
            {
                Debug.Log("Hit back bullet collided with player");
                PlayerPlaneEnergyCharge.Instance.AddEnergy();
            }
            else
            {
                PlayerHealthControl.Instance.PlayerHurt(damage);
            }
            rb.linearVelocity = Vector2.zero;
            ParticalPlay();
            Destroy(gameObject);
        }
    }
    private void ParticalPlay()
    {
        GameObject tmp = Instantiate(destroyPartical, transform.position, Quaternion.identity);
        if(isHitBackBullet)
        {
            tmp.GetComponent<ParticleSystemRenderer>().material = hitBackBreakMaterial;
        }
        tmp.GetComponent<ParticleSystem>().Play();
    }
}
