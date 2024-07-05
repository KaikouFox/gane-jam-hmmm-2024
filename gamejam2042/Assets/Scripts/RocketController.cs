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
    }

    public void LaunchRocket()
    {

    }
}
