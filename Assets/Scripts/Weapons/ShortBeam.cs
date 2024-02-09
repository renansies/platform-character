using Capabilities;
using UnityEngine;

public class ShortBeam : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

    [SerializeField] private float timeToLive = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if (timeToLive > 0)
            timeToLive -= Time.deltaTime;
        else
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.GetComponent<Shoot>())
                Destroy(gameObject);
        }
}
