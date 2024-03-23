using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Player : MonoBehaviour
{
    public bool IsMoving => isMoving;

    private Node currentNode,nextNode,prevNode,startNode;

    [Header("Render")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform spritePos;

    [Header("Stats"), Space(5)]
    [SerializeField] private int atk;
    [SerializeField] private int def;
    [SerializeField] private int hp;

    [Header("State"), Space(5)]
    [SerializeField] private bool isMyTurn = true;
    [SerializeField] private bool isRollDice = true;
    [SerializeField] private bool isSelectDir = false;
    private bool isMoving = false;

    private int moveCount;
    private int diceNumber;

    private List<Node> arrivableNodes = new();
    
    // Start is called before the first frame update
    void Start()
    {
        prevNode = NodeManager.Instance.centerNode;
        currentNode = NodeManager.Instance.centerNode;
        Teleport(prevNode);
    }

    // Update is called once per frame
    void Update()
    {
        Turn();    
        SetRotation();
    }

    private void Turn()
    {
        if (isMyTurn)
        {
            if(Input.GetKeyDown(KeyCode.Space) && !isRollDice)
            {
                RollDice();
                Debug.Log(moveCount);
                StartCoroutine(Turning());
            }

        }
    }

    private IEnumerator Turning()
    {
        for (int i = 0; i < moveCount; i++)
        {
            if(currentNode.IsSpecial)
            {
                currentNode.Effect();
            }
            isMoving = true;
            var nextNodes = currentNode.GetNextNodes().Where(x => x != prevNode);
            Debug.Log(nextNodes.Count());
            if (nextNodes.Count() > 1)
            {
                SelectDirUIOn();
                isSelectDir = true;
            }
            else
            {
                nextNode = nextNodes.ElementAt(0);
                Move();
            }

            yield return new WaitUntil(() => !isMoving);
        }
        currentNode.Effect();
        EffectLib.Instance.StopFX("ArrivePanelFX");
        isRollDice = false;
    }

    private void SelectDirUIOn()
    {
        var nextNodes = currentNode.GetNextNodes().Where(x => x != prevNode);
        foreach(var node in nextNodes)
        {
            NodeManager.Instance.ArrowOn(this,node);
        }
    }

    public void SelectedDir(Node node)
    {
        if(isSelectDir)
        {
            nextNode = node;
            isSelectDir = false;
            Move();
        }
    }

    private void Move()
    {
        currentNode.MoveNode(prevNode);
        transform.DOMove(nextNode.GetNodeStepPos(), 0.5f).OnComplete(() => 
            { isMoving = false; prevNode = currentNode; currentNode = nextNode; }
        );
    }

    private void RollDice()
    {
        moveCount = Random.Range(1, 7);
        diceNumber = moveCount;
        startNode = currentNode;
        arrivableNodes.Clear();
        arrivableNodes = NodeManager.Instance.GetArriveNodes(diceNumber,startNode,prevNode);
        ShowArrivalbeNode();
        isRollDice = true;
    }

    private void ShowArrivalbeNode()
    {
        foreach(var node in arrivableNodes)
        {
            EffectLib.Instance.PlayFX("ArrivePanelFX", 1, node.GetNodeStepPos(), true);
        }
    }

    private void SetRotation()
    {
        spritePos.localRotation = GameManager.Instance.Cam.transform.rotation;
        //var dir = GameManager.Instance.Cam.transform.position - transform.position;
        //dir = dir.normalized;
        //var deg = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        //Quaternion rot = Quaternion.Euler(spritePos.eulerAngles.x, -deg, spritePos.eulerAngles.z);
        //spritePos.localRotation = rot;
        //var fixPos = Vector3.one;
        //fixPos.y = 0;
        //spritePos.localPosition = fixPos;

    }

    private void Teleport(Node node)
    {
        transform.position = node.GetNodeStepPos();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isRollDice && startNode != null)
        {
            if(arrivableNodes != null && arrivableNodes.Count > 0)
            {
                foreach (Node node in arrivableNodes)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(node.GetNodeStepPos(), 1);
                }
            }
            
        }
        if (prevNode != null && currentNode != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(prevNode.GetNodeStepPos(), 0.5f);
            Gizmos.DrawLine(prevNode.GetNodeStepPos(), currentNode.GetNodeStepPos());
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(currentNode.GetNodeStepPos(), 1);
            if (nextNode != null)
            {
                Gizmos.DrawLine(currentNode.GetNodeStepPos(), nextNode.GetNodeStepPos());
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(nextNode.GetNodeStepPos(), 0.5f);
            }
        }
    }
#endif
}
