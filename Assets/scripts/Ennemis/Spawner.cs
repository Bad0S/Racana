using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public GameObject target;

	public List<GameObject> objetASpawner;
	public List<float> ProbaSpawnObjet;


	public float spawnTimer;
	public float tempsEntreSpawns;
	private bool faitSpawn;
	public int nbObjASpawner;
	public int nbObjSpawnes;

	public Camera cam1;
	private float cameraHeight;
	private float cameraWidth;
	private float camMin;
	private float camMax;

	private Vector3 SpawnPoint;

	public GameObject criTigre;
	public GameObject tirBambou;



	// Use this for initialization
	void Start () 
	{
		cam1 = Camera.main;
		cameraHeight = 2f * cam1.orthographicSize;
		cameraWidth = cameraHeight * cam1.aspect;

	}
	
	// Update is called once per frame
	void Update () 
	{
		spawnTimer += Time.deltaTime;
		transform.position = target.GetComponent<Transform>().position;
		if (spawnTimer >= tempsEntreSpawns && nbObjSpawnes < nbObjASpawner) 
		{
			if (Random.Range (0, 2) == 0) {
				SpawnPoint.x = Random.Range (0, (cameraWidth)/2);
				SpawnPoint.y = cameraHeight/2 +2;
			} else {
				SpawnPoint.x = cameraWidth/2 +2;
				SpawnPoint.y = Random.Range (0, (cameraHeight)/2);
			}


			SpawnPoint.z = 0f;
			if (Random.Range (0, 2) == 1)
				SpawnPoint.x = SpawnPoint.x *-1f;			
			if (Random.Range (0, 2) == 1)
				SpawnPoint.y = SpawnPoint.y *-1f;

			Spawn (RandomEnemySpawn (objetASpawner, ProbaSpawnObjet));
			spawnTimer = 0f;
			nbObjSpawnes++;
		}
	}

	void Spawn(GameObject objetASpawnerFonction)
	{
		print (SpawnPoint);
		GameObject instance = Instantiate<GameObject> (objetASpawnerFonction,SpawnPoint, Quaternion.identity);
		instance.transform.position = target.GetComponent<Transform>().position + SpawnPoint;
		try{
			instance.GetComponent<TigreBehavior>(). target= target;
			instance.GetComponent<TigreBehavior>().attackRoar = criTigre;
		}catch{
			
		}

		try{
			instance.GetComponent<BambouBehaviour>(). target= target;
			instance.GetComponent<BambouBehaviour>(). herbeTir= tirBambou;
		}
		catch{

		}
		//instance.transform.SetParent (null);
	}

	GameObject RandomEnemySpawn(List<GameObject> objets, List<float> probas ){
		for (int i = 0; i < probas.Count; i++) {
			if(probas[i] == 0){
				probas[i] = 1/objets.Count;  
			}
		}
		float rand = Random.Range (0f, 1f);
		print (rand);

		int counter = 0;
		float fcounter =(float)counter ;
		GameObject enemySpawned = null;
		while ((rand < 1f )||( fcounter >= probas.Count )) {
			if(rand<probas[counter]){
				//print (counter);
				enemySpawned = objets [counter];
				print (enemySpawned);
				rand = 1;
			}
			else{
				enemySpawned = objets [counter+1];
				rand = 1;
			}
			print (enemySpawned);
			counter++;
			fcounter =(float)counter ;
		}

		return enemySpawned;

	}
}
