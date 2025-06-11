using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public float hp;
    public float maxHp;

    Rigidbody2D rb;

    public float speed;

    public SpriteRenderer spr;
    public Sprite[] sprites;
    public TextMesh hpText;

    Vector3 baseScale; //for popping animation
    Vector2 direction;

    bool isDestroyed;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        maxHp = hp;
        hpText.text = hp + "";

        baseScale = transform.localScale;

        direction = Player._instance.transform.position - transform.position;
        direction.Normalize();

        spr.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = direction * speed;
    }

    /// <summary>
    /// Collision events
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
        if (isDestroyed) return;

        //Colliding with ball
        if (collision.gameObject.tag == "Ball") {
            CurrencyManager._instance.AddCoins(Mathf.Min(hp, collision.gameObject.GetComponent<Ball>().damage));

            hp -= collision.gameObject.GetComponent<Ball>().damage;

            HitByBall(0.05f);
        }


    }

    /// <summary>
    /// Trigger collision events
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (isDestroyed) return;

        //Colliding with ball
        if (collision.gameObject.tag == "Ball") {
            CurrencyManager._instance.AddCoins(Mathf.Min(hp, collision.gameObject.GetComponent<Ball>().damage));

            hp -= collision.gameObject.GetComponent<Ball>().damage;

            HitByBall(0.05f);
        }
    }

    /// <summary>
    /// Destroying a block
    /// </summary>
    public void DestroyBlock(bool planet = false) {
        var g = Instantiate(GameManager._instance.destroyEffect[Random.Range(0, GameManager._instance.destroyEffect.Length)]); //Explosion effect
        g.transform.position = transform.position;

        BlockSpawner._instance.BlockDestroyed(this, planet);

        isDestroyed = true;

        Destroy(gameObject);
    }

    /// <summary>
    /// OnHit event, scaling animation, checking HP
    /// </summary>
    /// <param name="scaleDif"></param>
    public void HitByBall(float scaleDif) {
        spr.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0.6f), 1 - hp / (float)maxHp);
        hpText.text = (int)hp + "";

        if (hp <= 0) {
            DestroyBlock();
        }


        StopAllCoroutines();
        StartCoroutine(Scale(scaleDif));
    }

    IEnumerator Scale(float scaleDif) {
        float t = 0;

        while (t < 1) {
            t += Time.deltaTime * 17;
            transform.localScale = Vector3.Lerp(baseScale, baseScale - new Vector3(scaleDif, scaleDif, 0), t);

            yield return null;
        }

        StartCoroutine(ScaleBack(-scaleDif));
    }

    IEnumerator ScaleBack(float scaleDif) {
        //Vector3 baseScale = transform.localScale;
        float t = 0;

        while (t < 1) {
            t += Time.deltaTime * 17;
            transform.localScale = Vector3.Lerp(baseScale - new Vector3(scaleDif, scaleDif, 0), baseScale, t);

            yield return null;
        }
    }
}
