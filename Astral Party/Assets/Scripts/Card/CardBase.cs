using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class CardBase : MonoBehaviour
{
    public string Name => cardName;
    public string Description => cardDescription;
    
    [SerializeField] private string cardName;
    [SerializeField,TextArea(1,10)] private string cardDescription;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected abstract void Use();

}
