using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public bool IsSpecial
    {
        get
        {
            return isSpecial;
        }
    }
    
    [SerializeField] protected List<Node> nextNodes = new();
    [SerializeField] protected Node prevNode;
    [SerializeField] protected bool isSpecial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void InitNode()
    {
        if (nextNodes.Count == 0)
        {
            var hits = Physics.OverlapSphere(transform.position, 2, LayerMask.GetMask("Node"));
            foreach (var hit in hits)
            {
                if (hit.gameObject == this.gameObject) continue;
                if (hit.TryGetComponent(out Node node))
                {
                    if(!CheckDiagonalNode(node))
                        nextNodes.Add(node);
                }
            }
        }
    }

    protected bool CheckDiagonalNode(Node node)
    {
        var x = node.transform.position.x - transform.position.x;
        var z = node.transform.position.z - transform.position.z;
        if(Mathf.Abs(x) > 0 && Mathf.Abs(z) > 0)
        {
            return true;
        } else
        {
            return false;
        }

    }

    public List<Node> GetNextNodes()
    {
        return nextNodes;
    }

    public Node GetPrevNode()
    {
        return prevNode;
    }

    public Vector3 GetNodeStepPos()
    {
        return transform.position + Vector3.up * 0.3f;
    }

    public virtual void MoveNode(Node node)
    {
        prevNode = node;
    }

    public abstract void Effect();
}
