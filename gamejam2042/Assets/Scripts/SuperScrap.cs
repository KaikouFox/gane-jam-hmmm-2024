using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperScrap : MonoBehaviour
{
    private int day;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        day = GameObject.FindWithTag("Player").GetComponent<PlayerController>().day;
        if (day != 1) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
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