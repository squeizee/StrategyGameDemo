using Controllers;
using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Buildings
{
    public class Barrack : Building
    {
        [SerializeField] private Vector3 spawnPoint = new Vector3(0f, -1f, 0f);

        public Vector3 SpawnPoint => spawnPoint;
    }
}