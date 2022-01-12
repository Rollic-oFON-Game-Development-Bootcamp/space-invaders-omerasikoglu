using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// gerek olmayabilir buna
/// </summary>
public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }
  
    private void Awake()
    {
        Instance = this;
       
    }


}
