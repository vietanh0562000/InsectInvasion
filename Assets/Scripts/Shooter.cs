using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [Header("Shooting")] public GameObject ballPrefab; //Ball to instantiate
    public Transform fireTransform;
    public float fireRate;
    public float damage;
    public float ballSpeed;

    float shooterTimer = 0f;

    [HideInInspector]
    public List<GameObject> ballPool; //for object pooling -> better fps

    public float multiplier = 1;

    public GameObject laser;

    // Start is called before the first frame update
    void Start() {
        ballPool = new List<GameObject>();

    }

    // Update is called once per frame
    void Update() {
        if (GameManager._instance.isPaused) {
            if (laser != null)
                laser.SetActive(false);

            return;
        }

        if (laser != null)
            laser.SetActive(true);

        Shoot();
    }

    /// <summary>
    /// Shhoting balls, called in every frame
    /// </summary>
    void Shoot() {
        shooterTimer += Time.deltaTime;

        //If you cant shoot return
        if (shooterTimer < 1f / (fireRate * (1 + GameManager._instance.atkSpeedBonus / 100f))) return;

        shooterTimer = 0;

        //Getting ball from pool
        var g = GetBallFromPool();
        g.SetActive(true);
        g.transform.position = fireTransform.position;
        g.transform.eulerAngles = transform.eulerAngles;

        g.GetComponent<Ball>().damage = damage * multiplier;
        g.GetComponent<Ball>().baseSpeed = ballSpeed;

        float angle = transform.parent.GetComponent<ItemSlot>().rotation + Player._instance.transform.localEulerAngles.z + 90;
        g.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * ballSpeed;
    }


    /// <summary>
    /// if you have a free ball returns it, and if all balls are in usage instantiate one
    /// </summary>
    /// <returns></returns>
    GameObject GetBallFromPool() {
        foreach (GameObject g in ballPool)
            if (g.activeSelf == false)
                return g;

        var newBall = Instantiate(ballPrefab);
        ballPool.Add(newBall);

        return newBall;
    }
}
