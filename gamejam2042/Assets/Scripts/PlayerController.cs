using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private int actionPoints = 10;

    private GameObject rocket;
    private float movementX;
    private float movementY;
    private CanvasManager canvasManager;
    private bool touchScrap = false;
    private GameObject scrap;
    private int scrapAmount = 0;
    private bool touchRocket = false;
    private RocketController rocketScript;
    public GameObject turret;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        canvasManager.SetActionPoints(actionPoints);
        canvasManager.SetScrapPoints(scrapAmount);
        rocket = GameObject.FindWithTag("rocket");
        rocketScript = rocket.GetComponent<RocketController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchScrap)
        {
            ChangeScrapAmount(1);
            ChangePointAmount(-1);
            Destroy(scrap);
        }
        else if (Input.GetKeyDown(KeyCode.E) && touchRocket && scrapAmount >= 1)
        {
            ChangePointAmount(-1);
            ChangeScrapAmount(-1);
            rocketScript.AddScraps(1);
        }
        if (Input.GetKeyDown(KeyCode.Q) && touchRocket)
        {
            rocketScript.LaunchRocket(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.T) && scrapAmount >= 2)
        {
            scrapAmount -= 2;
            ChangePointAmount(-1);
            BuildTurret();
        }
    }

    private void FixedUpdate()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        transform.position = transform.position + new Vector3(movementX * baseSpeed * Time.deltaTime, movementY * baseSpeed * Time.deltaTime, 0);
    }

    public void ChangePointAmount(int newValue)
    {
        actionPoints += newValue;
        canvasManager.SetActionPoints(actionPoints);
    }

    public void ChangeScrapAmount(int newValue)
    {
        scrapAmount += newValue;
        canvasManager.SetScrapPoints(scrapAmount);
    }

    public void triggerEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("scrap"))
        {
            touchScrap = true;
            scrap = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("rocket"))
        {
            touchRocket = true;
        }
    }

    public void triggerExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("scrap"))
        {
            touchScrap = false;
        }
        else if (collision.gameObject.CompareTag("rocket"))
        {
            touchRocket = false;
        }
    }

    public void BuildTurret()
    {
        Instantiate(turret, transform.position, transform.rotation);
    }


}
