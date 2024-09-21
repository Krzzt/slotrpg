using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




[CreateAssetMenu]
public class Biomes : ScriptableObject
{
    public string Name;
    public List<GameObject> EnemyTypesInBiome;
    
}
