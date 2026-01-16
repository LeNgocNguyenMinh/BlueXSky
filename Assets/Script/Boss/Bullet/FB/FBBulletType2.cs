using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class FBBulletType2 : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private GameObject destroyPartical;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private TrailRenderer trailRenderer;
    [SerializeField]private Material hitBackMaterial;
    [SerializeField]private Material hitBackBreakMaterial;
    private Vector2 currentTarget;
    private float speed;
    private int currentIndex;
    private List<float> listX;
    private bool isChange;
    private float damage;
    private bool isHitBackBullet;

    public void SetValue(float speed, float damage, int currentIndex, bool isHitBackBullet)
    {
        this.isHitBackBullet = isHitBackBullet;
        this.damage = damage;
        this.speed = speed;
        listX = BulletSpawnPoint.Instance.GetList4Point();
        this.currentIndex = currentIndex;
        isChange = false;
        currentTarget = new Vector2(listX[currentIndex], 0);
        if(isHitBackBullet)
        {
            spriteRenderer.material = hitBackMaterial;
            trailRenderer.material = hitBackMaterial;
        }
    }
    void FixedUpdate()
    {
        if(Vector3.Distance(rb.position, currentTarget) < 0.01f )
        {
            if(!isChange)
            {
                isChange = true;
                NextTarget(); 
                return;
            }
            else
            {
                ParticalPlay();
                Destroy(gameObject);
                return;
            }          
        }
        rb.MovePosition(
            Vector2.MoveTowards(rb.position, currentTarget, speed * Time.fixedDeltaTime)
        );
    }
    private void NextTarget()
    {
        int dir = Random.value < 0.5f ? -1 : 1;   // trái hoặc phải
        if((currentIndex + dir)<0 || (currentIndex + dir)>=listX.Count)
        {
            dir *= -1;
        }
        currentIndex += dir;
        currentTarget = new Vector2(listX[currentIndex], -Camera.main.orthographicSize);
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
