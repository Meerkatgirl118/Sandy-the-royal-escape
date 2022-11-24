using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindMiceBoss : MonoBehaviour
{
    [SerializeField] Light mainLight;
    [SerializeField] ParticleSystem fog;
    [SerializeField] GameObject cleaningRat;
    [SerializeField] GameObject cookingRat;
    [SerializeField] GameObject enemies;
    PlayerMovement playerMovement;

    GameObject enemySpawned;
    float battleStartWait;
    bool bossBattleTriggered = false;
    bool spawnEnemies = false;

    public int enemiesDefeated = 0;
    public bool ratAttackSectionTriggered = false;

    [SerializeField] GameObject[] cameras;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
      if (ratAttackSectionTriggered && enemiesDefeated >= 10)
        {
            print("All enemies defeated");
            ratAttackSectionTriggered = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !bossBattleTriggered)
        {
            StartCoroutine(BattleStart());
        }
    }

    IEnumerator BattlePhase1RatAttack()
    {
        if (spawnEnemies)
        {
            ratAttackSectionTriggered = true;
            for (int i = 0; i < 10f; i++)
            {
                fog.gameObject.SetActive(true);
                Invoke("SpawnEnemies", 10f);
                yield return new WaitForSeconds(3f);
            }
            fog.gameObject.SetActive(false);
        }
    }

    void SpawnEnemies()
    {
        enemySpawned = Instantiate(cleaningRat, this.transform.position, Quaternion.identity);
        enemySpawned.transform.parent = enemies.transform;
        enemySpawned.GetComponent<EnemyBehaviour>().FindNeededScripts();
    }


    IEnumerator BattleStart()
    {
        mainLight.color = Color.Lerp(mainLight.color, Color.red, 20f);
        playerMovement.movementEnabled = false;
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < cameras.Length; i++)
        {
            if (i != 4)
            {
                cameras[i].SetActive(false);
                cameras[i + 1].SetActive(true);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                cameras[i].SetActive(false);
                cameras[0].SetActive(true);
                yield return new WaitForSeconds(3f);
            }
        }

        cameras[1].gameObject.SetActive(false);
        cameras[0].gameObject.SetActive(true);
        playerMovement.movementEnabled = true;
        bossBattleTriggered = true;
        spawnEnemies = true;
        StartCoroutine(BattlePhase1RatAttack());
    }

    
}
