using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;

    [SerializeField]
    private GameObject boarPrefeb;
    [SerializeField]
    private GameObject canniblePrefeb;

    public Transform[] boarSpawnPoint;
    public Transform[] cannibleSpawnPoint;

    [SerializeField]
    private int canibleCount;
    [SerializeField]
    private int boarCount;

    private int initialCanibleCount;
    private int initialBoarCount;

    public float waitBeforeSpawn = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        initialCanibleCount = canibleCount;
        initialBoarCount = boarCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void SpawnEnemies()
    {
        SpawnCannibles();
        SpawnBoars();
    }

    void SpawnCannibles()
    {
        int index = 0;

        for (int i = 0; i < canibleCount; i++)
        {
            if (index >= cannibleSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(canniblePrefeb, cannibleSpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        canibleCount = 0;
    }

    void SpawnBoars()
    {
        int index = 0;

        for (int i = 0; i < boarCount; i++)
        {
            if (index >= boarSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(boarPrefeb, boarSpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        boarCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawn);
        SpawnBoars();
        SpawnCannibles();
        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool cannible)
    {
        if (cannible)
        {
            canibleCount++;
            if (canibleCount > initialCanibleCount)
            {
                canibleCount = initialCanibleCount;
            }
        }
        else
        {
            boarCount++;
            if (boarCount > initialBoarCount)
            {
                boarCount = initialBoarCount;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

}
