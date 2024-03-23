using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    private Node node;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        transform.rotation = GameManager.Instance.Cam.transform.rotation;
    }

    public void Select()
    {
        player.SelectedDir(node);
        NodeManager.Instance.DisableArrowUI();
    }

    public void Init(Player player,Node node)
    {
        this.node = node;
        this.player = player;  
    }
}
