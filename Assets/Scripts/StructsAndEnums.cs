using UnityEngine;
using System.Collections;

namespace StructsAndEnums
{
	[System.Serializable]
	public struct ScoreMultiplier
	{
		public int EnemiesKilled;
		public float MultiplyBy;
		public float MinimumSpawnDelayModifier;
		public float MaximumSpawnDelayModifier;
	}
	
	[System.Serializable]
	public struct Highscore
	{
		public float Score;
		public float GameTime;
		public string Name;
		public string GameMode;
		public int EnemiesKilled;
		public int Multiplier;
	}

	[System.Serializable]
	public struct SpawnerSettings
	{
		public bool IsEnabled;
		public float SpawningRateMinimum;
		public float SpawningRateMaximum;
		public bool CanBeDisabled;
		public float DisableTime;
		public EnemyStruct EnemyToSpawn;
		public GameObject DisableParticle;
		public GameObject SpawnParticle;
		public GameObject PreSpawnParticle;
		public float PreSpawnWarningTime;
		public float SpawnDelayInSeconds;
		public float MinimumSpeed;
		public float MaximumSpeed;
		public float SpawnDelayModifierMin;
		public float SpawnDelayModifierMax;
		public float BaseRateMin;
		public float BaseRateMax;
		public bool IsSpecialSpawner;
	}

	[System.Serializable]
	public struct EnemyStruct
	{
		public GameObject Enemy;
		public string OrName;
		public float PercentageChanceToSpawn;
	}

	[System.Serializable]
	public struct Powerup
	{
		public string Name;
		public bool Collectible;
		public PowerupTypes Type;
		public bool TiedToSpawners;
		public float ColliderSize;
		public bool IsInvisible;
		public GameObject PrefabToSpawn;
		public GameObject ParticleToSpawnOnCollect;
		public float DelayBetweenSpawnsInSeconds;
	}

	[System.Serializable]
	public enum PowerupTypes
	{
		Multiplier,
		Ammo,
		Score,
		MaxTime,
		Health,
		Enemies
	}

	[System.Serializable]
	public struct SoftResetSettings
	{
		public float CooldownRate;
		public bool DoesMultiplierReset;
		public bool DoesTriggerCooldown;
		public bool DoesChangeNumberOfSpawners;
		public int NumberOfSpawnersToChange;
	}

	[System.Serializable]
	public struct ScoringSettings
	{
		public bool PerKillScoring;
		public bool PerTimeElapsedScoring;
		public bool MultipliersEnabled;
		public bool MultipliersTiedToEnemies;
		public bool MultiplersAffectingSpawnRates;
	}
}
