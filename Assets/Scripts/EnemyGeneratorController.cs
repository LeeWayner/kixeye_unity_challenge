using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a prefab, a base to create oher objects of this type
// and keep its behaivor and properties 
public class EnemyGeneratorController : MonoBehaviour
{
	[SerializeField]
	private EnemyController enemyPrefab; // The actual object 
	[SerializeField]
	private float generatingInterval = 1.75f; // The velocity between the objects will be created 
	private ObjectPooling<EnemyController> enemyPool;
	private List<EnemyController> currentEnemies;
	private Vector3 scale = Vector3.one;
	private bool isGenerating = false;
	private float timer = 0;
	// Use this for initialization
	void Start()
	{
		enemyPool = new ObjectPooling<EnemyController>(20, ResetEnemy, InitEnemy);
		enemyPool.InitBuffer(5);
		currentEnemies = new List<EnemyController>(20);
	}

	private void ResetEnemy(EnemyController _enemy)
	{
		if (_enemy != null)
		{
			_enemy.gameObject.SetActive(true);
		}
	}
	private void InitEnemy(EnemyController _enemy)
	{
		if (_enemy == null)
		{
			_enemy = Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
			_enemy.gameObject.SetActive(false);
			enemyPool.Store(_enemy);
		}
	}

	public void RestoreEnemy(EnemyController _enemyController)
	{
		currentEnemies.Remove(_enemyController);
		enemyPool.Store(_enemyController);
		_enemyController.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if(isGenerating)
		{
			timer += Time.deltaTime;
			if(timer > generatingInterval)
			{
				CreateEnemy();
				timer = 0;
			}
		}
	}

	void CreateEnemy()
	{
		int shouldCreate = UnityEngine.Random.Range(0, 2);
		if (shouldCreate == 0)
		{
			return;
		}
		float randomValue;
		EnemyController enemyObject = enemyPool.New();
		enemyObject.transform.position = transform.position;
		randomValue = UnityEngine.Random.value;
		// Change its local scale in x y z format depending of the probability 
		if (randomValue > 0.5 && randomValue <= 0.7)
		{
			scale.x = 1.5f;
			scale.y = 1.5f;
		}
		else if (randomValue > 0.7 && randomValue <= 0.9)
		{
			scale.x = 1.75f;
			scale.y = 1.75f;
		}
		else if (randomValue > 0.9)
		{
			scale.x = 2f;
			scale.y = 2f;
		}
		else
		{
			scale.x = 1;
			scale.y = 1;
		}

		enemyObject.transform.localScale = scale;
		enemyObject.StartMove();
		currentEnemies.Add(enemyObject);
	}

	public void StartGenerating()
	{
		isGenerating = true;
	}

	public void CancelGenerator(bool clean = false)
	{
		isGenerating = false;

		if (clean)
		{			
			foreach (EnemyController enemy in currentEnemies)
			{
				enemyPool.Store(enemy);
				enemy.gameObject.SetActive(false);
			}
			currentEnemies.Clear();
		}
	}
}
