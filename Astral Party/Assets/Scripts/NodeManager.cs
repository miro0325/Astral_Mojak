using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NodeManager : Singleton<NodeManager>
{
    public List<Node> nodes = new();
    public Dictionary<Vector2, Node> nodePosArray = new();

    public Node centerNode;

    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private ArrowUI arrowUI;

    private List<ArrowUI> arrowList = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArrowOn(Player player,Node node)
    {
        var arrow = Instantiate(arrowUI, worldCanvas.transform);
        arrow.transform.position = node.GetNodeStepPos();
        arrow.Init(player,node);
        arrowList.Add(arrow);
    }

    public void DisableArrowUI()
    {
        for(int i = 0; i < arrowList.Count; i++)
        {
            Destroy(arrowList[i].gameObject);
        }
        arrowList.Clear();
    }

    public List<Node> GetArriveNodes(int range,Node startNode,Node prevNode)
    {
        List<Node> nodes = new();
        DFSUtil(range,prevNode,startNode,nodes);
        return nodes;
        
        void DFSUtil(int count, Node prevNode,Node curNode,List<Node> arriveNodes)
        {
            if (count == 0)
            {
                arriveNodes.Add(curNode);
                return;
            }
            foreach(var node in curNode.GetNextNodes())
            {
                if (prevNode == node) continue;
                DFSUtil(count-1, curNode,node, arriveNodes);
            }
        }   
    }

}
