using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Collectible Gem;
    public List<Transform> GemPosition = new List<Transform>(); //we used a List here and not an array since Lists are better when dealing with a dynamic set of elements.
    public List<Collectible> GemsSpawned = new List<Collectible>();
    public static LevelManager instance;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
       else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        for (int i = 0; i < GemPosition.Count; i++)
        {
            GemsSpawned.Add(Instantiate(Gem, GemPosition[i].transform.position, GemPosition[i].transform.rotation,GemPosition[i].transform));
        }
    }

    public void RemovePosition(Collectible currentGemTransform)
    {
        for(int i=0;i<GemPosition.Count;i++)
        {
            if(currentGemTransform.Equals(GemsSpawned[i]))
            {
                GemPosition.RemoveAt(i);
                GemsSpawned.RemoveAt(i);
            }
        }
    }
}
