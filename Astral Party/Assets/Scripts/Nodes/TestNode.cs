using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestNode : Node
{
    public override void Effect()
    {
        Debug.Log(gameObject.transform.position);
    }

    public override void MoveNode(Node node)
    {
        base.MoveNode(node);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitNode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
