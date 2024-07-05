using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField] private int minScraps = 10;
    [SerializeField] private int maxScraps = 20;
    private int scraps;

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
            Destroy(player);
            Debug.Log(transform.GetChild(0).name);
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
