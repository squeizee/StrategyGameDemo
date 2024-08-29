using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ContentTest : MonoBehaviour
{
    [SerializeField] private List<GameObject> pool;

    public float lastCheckedPositionY = 0f;
    
    private const float threshold = 200f;
    
    
    
    public ScrollRect scrollRect;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        
    }

    private void Update()
    {
        if (scrollRect.content.localPosition.y > lastCheckedPositionY + threshold)
        {
            MoveToBottom();
            lastCheckedPositionY += threshold;
        }
        else if (scrollRect.content.localPosition.y < lastCheckedPositionY - threshold)
        {
            MoveToTop();
            lastCheckedPositionY -= threshold;
        }
    }
    
    private void MoveToBottom()
    {
        var firstTwo = pool.GetRange(0,2);
        pool.RemoveRange(0,2);
        
        var lastYValue = pool[^1].transform.localPosition.y;
        
        firstTwo[0].transform.localPosition = new Vector3(firstTwo[0].transform.localPosition.x, lastYValue - 200f, firstTwo[0].transform.localPosition.z);
        firstTwo[1].transform.localPosition = new Vector3(firstTwo[1].transform.localPosition.x, lastYValue - 200f, firstTwo[1].transform.localPosition.z); 
        
        pool.AddRange(firstTwo);
        
    }
    
    private void MoveToTop()
    {
        var lastTwo = pool.GetRange(pool.Count - 2, 2);
        pool.RemoveRange(pool.Count - 2, 2);
        
        var firstYValue = pool[0].transform.localPosition.y;
        lastTwo[0].transform.localPosition = new Vector3(lastTwo[0].transform.localPosition.x, firstYValue + 200f, lastTwo[0].transform.localPosition.z);
        lastTwo[1].transform.localPosition = new Vector3(lastTwo[1].transform.localPosition.x, firstYValue + 200f, lastTwo[1].transform.localPosition.z); 
        
        pool.InsertRange(0, lastTwo);
    }
}
