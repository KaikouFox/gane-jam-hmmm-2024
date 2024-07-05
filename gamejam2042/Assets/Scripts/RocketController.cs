using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private int minScraps = 10;
    [SerializeField] private int maxScraps = 20;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private GameObject explosionParticle;
    private int scraps;
    private bool fly = false;
    private float speed = 0f;
    private CanvasManager canvasManager;

    private void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    private void FixedUpdate()
    {
        if (fly)
        {
            transform.position = transform.position + new Vector3(0, Mathf.Pow(2, speed)) * Time.deltaTime;
            speed += acceleration;
            if (speed > 3.8 && scraps < maxScraps)
            {
                Instantiate(explosionParticle, transform.position, transform.rotation);
                canvasManager.SetEnding("Bad ending", Color.red);
                Destroy(gameObject);
            } else if (speed > 7 && scraps >= maxScraps)
            {
                canvasManager.SetEnding("Good ending", Color.white);
                Destroy(gameObject);
            }
        }
    }

    public void AddScraps(int amount)
    {
        scraps += amount;
        Debug.Log("scraps in rocket: " + scraps);
    }

    public void LaunchRocket(GameObject player)
    {
        Debug.Log("try to launch");
        if (scraps >= minScraps)
        {
            Transform camera = player.transform.GetChild(0);
            camera.SetParent(null);
            camera.position = new Vector3(0, 5, -15);
            Destroy(player);
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            fly = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().triggerEnter(gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().triggerExit(gameObject.GetComponent<Collider2D>());
        }
    }
}
