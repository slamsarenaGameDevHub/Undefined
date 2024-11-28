using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] List<NPC> m_Npcs = new List<NPC>();
    [SerializeField] List<Terrorist> m_Terrorist = new List<Terrorist>();
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] Transform npcCounter;

    float m_countDown = 3;
    int civilianDeterminer = 0;
    int terroristDeterminer = 0;
    int spawnPosIndex = 0;
    [SerializeField] int maxSpawnAmount = 40;
    int currentNpcCount
    {
        get
        {
            return npcCounter.childCount - 1;
        }
    }

    [SerializeField] float _delay = 4;

    
    private void Update()
    {
        SpawnRandom();
    }
    void SpawnRandom()
    {
        int decideNpcToSpawn = Random.Range(0, 2);
        civilianDeterminer = Random.Range(0, m_Npcs.Count);
        terroristDeterminer=Random.Range(0, m_Terrorist.Count);
        spawnPosIndex= Random.Range(0,spawnPoints.Count);
        
        m_countDown -= Time.deltaTime;
        if (m_countDown <= 0)
        {
            if (currentNpcCount < maxSpawnAmount)
            {
                switch (decideNpcToSpawn)
                {
                    case 0:
                        //Spawn Civilian
                       NPC npc= Instantiate(m_Npcs[civilianDeterminer], spawnPoints[spawnPosIndex].position,Quaternion.identity,npcCounter);
                        npc.Path=spawnPoints[spawnPosIndex];
                        break;
                    case 1:
                        //Spawn Terrorist
                       Terrorist terrorist = Instantiate(m_Terrorist[terroristDeterminer], spawnPoints[spawnPosIndex].position, Quaternion.identity,npcCounter);
                        terrorist.Path=spawnPoints[spawnPosIndex];
                        break;
                }
            }
            m_countDown = _delay;
        }


    }

}
