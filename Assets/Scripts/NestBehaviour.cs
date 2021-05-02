using System.Collections;
using UnityEngine;

public class NestBehaviour : MonoBehaviour
{

    [Header("ID of this nest")]
    [Tooltip("Unique ID to identify this object. Used for tracking quest progress.")]
    [SerializeField] int uniqueID;
    [Tooltip("Used to identify from witch nest the enemy came from. Use numbers > 0, because 0 means the enemy is not nested.")]
    [SerializeField] int nestId;

    [Header("Spawn configurations")]
    [Tooltip("Total enemies contained in the nest. Nest dies when all the enemies are killed.")]
    [SerializeField] int totalEnemies;
    [Tooltip("Number of enemies that can be spawned together. When this number is reached, an enemy has to be killed in order to spawn another.")]
    [SerializeField] int maxAliveEnemies;
    [Tooltip("Time (in seconds) between spawns.")]
    [SerializeField] float spawnRate;

    [Header("Witch enemy to spawn")]
    [SerializeField] GameObject enemyToSpawn;
    [Header("Where to spawn")]
    [SerializeField] GameObject spawnPoint;
    [Header("Objects Related to the Nest")]
    [Tooltip("Particle that is instantiated when nest dies.")]
    [SerializeField] GameObject deathParticle;
    [Tooltip("Animator controller for the nest")]
    [SerializeField] Animator anim;

    int spawnedEnemies = 0;
    int killedEnemies = 0;
    bool spawning;

    public int GetNestID()
    {
        return nestId;
    }

    public void RegisterKill()
    {
        killedEnemies += 1;

        CheckTotalKills();
    }

    private void CheckTotalKills()
    {        
        if (killedEnemies >= totalEnemies)
        {
            QuestManager.inst.UpdateQuestProgress(uniqueID);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void SpawnEnemyObject()
    {
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
        newEnemy.GetComponentInChildren<EnemyBehaviour>().SetNestID(nestId);

        if(GetComponent<DetectionRangeBehaviour>() != null)
            GetComponent<DetectionRangeBehaviour>().SetEnemy(newEnemy.GetComponentInChildren<EnemyBehaviour>());

        spawning = false;
        spawnedEnemies += 1;
    }

    private void OnTriggerStay(Collider other)
    {
        //Checks if player entered spawn area
        if (other.CompareTag("Player"))
        {
            //Check if can spawn
            if (!spawning && spawnedEnemies < totalEnemies && (spawnedEnemies - killedEnemies) < maxAliveEnemies)
            {
                spawning = true;
                StartCoroutine(SpawnEnemy());                 
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        //Wait for spawn rate
        yield return new WaitForSecondsRealtime(spawnRate);

        //Start nest animation
        anim.SetTrigger("spawn");//nest animation triggers spawn

    }


}
