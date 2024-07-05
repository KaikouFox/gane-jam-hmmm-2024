using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private int minScraps = 10;
    [SerializeField] private int maxScraps = 20;
    [SerializeField] private int tooManyScraps = 30;
    [SerializeField] private float acceleration = 0.1f;
    [SerializeField] private GameObject explosionParticle;
    private int scraps;
    private bool fly = false;
    private float speed = 0f;
    private CanvasManager canvasManager;
    private int day;
    private int omhoog = 1;

    private void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    private void FixedUpdate()
    {
        if (fly)
        {
            transform.position = transform.position + new Vector3(0, Mathf.Pow(2, speed) * omhoog) * Time.deltaTime;
            speed += acceleration;
            if (speed > 3.8 && scraps < maxScraps)
            {
                Instantiate(explosionParticle, transform.position, transform.rotation);
                canvasManager.SetEnding("Bad ending", Color.red, "BOOM!!!! \n You didn't put enough scrap in the rocket.");
                Destroy(gameObject);
            } else if (speed > 7 && scraps >= maxScraps && day == 1)
            {
                canvasManager.SetEnding("Speedrunner ending", Color.green, "Are you dream?");
                Destroy(gameObject);
            } else if (speed > 7 && scraps >= maxScraps)
            {
                canvasManager.SetEnding("Good ending", Color.white, "Well done!");
                Destroy(gameObject);
            } else if (speed > 3.8 && scraps >= tooManyScraps)
            {
                acceleration = -0.1f;
            } else if (speed <= 0 && scraps >= tooManyScraps)
            {
                omhoog = -1;
                speed = 0;
                acceleration = 0.1f;
            }
            if (transform.position.y <= 1.77)
            {
                Instantiate(explosionParticle, transform.position, transform.rotation);
                canvasManager.SetEnding("Heavy ending", Color.red, "BOOM!!!! \n Oopsie doopsie, you put too much scrap in the rocket. ");
                Destroy(gameObject);
            }
        }
    }

    public void AddScraps(int amount)
    {
        scraps += amount;
    }

    public void LaunchRocket(GameObject player)
    {
        day = player.GetComponent<PlayerController>().day;
        if (scraps >= minScraps)
        {
            Transform camera = player.transform.GetChild(0);
            camera.SetParent(null);
            camera.position = new Vector3(0, 5, -15);
            camera.gameObject.GetComponent<Camera>().orthographicSize = 12.5f;
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
