using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private int actionPoints = 10;
    [SerializeField] private int minDefencePoints = 2;
    [SerializeField] private int minDefencePointsIncreasePerDay = 1;

    private GameObject rocket;
    private float movementX;
    private float movementY;
    private CanvasManager canvasManager;
    private bool touchScrap = false;
    private GameObject scrap;
    private int scrapAmount = 0;
    private bool touchRocket = false;
    private bool touchTool = false;
    private int pickaxeLevel = 0;
    private bool touchSuperScrap = false;
    private RocketController rocketScript;
    public int day = 1;
    public GameObject turret;
    public Tilemap rockTilemap;
    public Tilemap hrockTilemap;
    private bool running = true;
    private int defencePoints = 0;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        canvasManager.SetCanvasState(CanvasManager.CanvasState.InGame);
        canvasManager.SetActionPoints(actionPoints);
        canvasManager.SetScrapPoints(scrapAmount);
        canvasManager.SetDefencePoints(defencePoints);
        canvasManager.SetDay(day);
        rocket = GameObject.FindWithTag("rocket");
        rocketScript = rocket.GetComponent<RocketController>();
    }

    private void Update()
    {
        if (!running) { return; }
        if (Input.GetKeyDown(KeyCode.E) && touchScrap)
        {
            ChangeScrapAmount(2);
            ChangePointAmount(-1);
            Destroy(scrap);
        }
        else if (Input.GetKeyDown(KeyCode.E) && touchSuperScrap)
        {
            ChangeScrapAmount(100);
            ChangePointAmount(100);
            Destroy(scrap);
        }
        else if (Input.GetKeyDown(KeyCode.E) && touchRocket && scrapAmount >= 1)
        {
            ChangePointAmount(-1);
            ChangeScrapAmount(-1);
            rocketScript.AddScraps(1);
        }
        if (Input.GetKeyDown(KeyCode.E) && pickaxeLevel >= 1)
        {
            Vector3Int position = Vector3Int.RoundToInt(transform.position);
            UnityEngine.Debug.Log(position);
            if (rockTilemap.HasTile(new Vector3Int(position.x+2, position.y, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x+1, position.y, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x-1, position.y, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x-2, position.y, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x, position.y+2, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x, position.y+1, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x, position.y-1, 0)) ||
                rockTilemap.HasTile(new Vector3Int(position.x, position.y-2, 0)))
            {
                rockTilemap.SetTile(new Vector3Int(position.x + 2, position.y, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x + 1, position.y, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x - 1, position.y, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x - 2, position.y, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x, position.y + 2, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x, position.y + 1, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x, position.y - 1, 0), null);
                rockTilemap.SetTile(new Vector3Int(position.x, position.y - 2, 0), null);
                ChangePointAmount(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && pickaxeLevel == 2)
        {
            Vector3Int position = Vector3Int.RoundToInt(transform.position);
            UnityEngine.Debug.Log("Checking hrock");
            if (hrockTilemap.HasTile(new Vector3Int(position.x + 2, position.y, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x + 1, position.y, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x - 1, position.y, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x - 2, position.y, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x, position.y + 2, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x, position.y + 1, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x, position.y - 1, 0)) ||
                hrockTilemap.HasTile(new Vector3Int(position.x, position.y - 2, 0))) 
            {
                UnityEngine.Debug.Log("Braek rock");
                hrockTilemap.SetTile(new Vector3Int(position.x + 2, position.y, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x + 1, position.y, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x - 1, position.y, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x - 2, position.y, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x, position.y + 2, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x, position.y + 1, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x, position.y - 1, 0), null);
                hrockTilemap.SetTile(new Vector3Int(position.x, position.y - 2, 0), null);
                ChangePointAmount(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && touchRocket)
        {
            rocketScript.LaunchRocket(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.T) && scrapAmount >= 2)
        {
            ChangeScrapAmount(-2);
            ChangePointAmount(-1);
            BuildTurret();
        }
    }

    private void FixedUpdate()
    {
        if (running)
        {
            movementX = Input.GetAxisRaw("Horizontal");
            movementY = Input.GetAxisRaw("Vertical");

            transform.position = transform.position + new Vector3(movementX * baseSpeed * Time.deltaTime, movementY * baseSpeed * Time.deltaTime, 0);
        }
    }

    public void ChangePointAmount(int newValue)
    {
        actionPoints += newValue;
        canvasManager.SetActionPoints(actionPoints);
        if (actionPoints <= 0)
        {
            canvasManager.SetAnnouncement("Ran out of Actionpoints");
            if (defencePoints < minDefencePoints)
            {
                canvasManager.SetEnding("Death ending", Color.red, "You didn't have enough defencepoints for the night.");
                transform.GetChild(0).parent = null;
                Destroy(gameObject);
            } else
            {
                minDefencePoints += minDefencePointsIncreasePerDay;
                StartCoroutine(NextDay());
            }
        }
        else
        {
            canvasManager.SetAnnouncement("");
        }
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
        else if (collision.gameObject.CompareTag("tool"))
        {
            pickaxeLevel += 1;
            ChangePointAmount(-1);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("superScrap"))
        {
            touchSuperScrap = true;
            scrap = collision.gameObject;
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
        else if (collision.gameObject.CompareTag("tool"))
        {
            touchTool = false;
        }
        else if (collision.gameObject.CompareTag("superScrap"))
        {
            touchSuperScrap = false;
        }
    }

    public void BuildTurret()
    {
        Instantiate(turret, transform.position, transform.rotation);
        defencePoints += 1;
        canvasManager.SetDefencePoints(defencePoints);
    }

    IEnumerator NextDay()
    {
        running = false;
        yield return new WaitForSecondsRealtime(3);
        transform.position = new Vector3(0.5f, -4, 0);
        running = true;
        canvasManager.SetAnnouncement("");
        ChangePointAmount(10);
        day +=1;
        canvasManager.SetDay(day);
    }
}
