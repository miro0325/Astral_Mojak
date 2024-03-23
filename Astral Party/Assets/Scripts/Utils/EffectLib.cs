using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectInfo
{
    public string id;
    public ParticleSystem effect;
}

public class EffectLib : Singleton<EffectLib>
{
    [SerializeField] private List<EffectInfo> registerEffects = new();
    
    private Dictionary<string, ParticleSystem> effects = new();
    private Dictionary<string, List<ParticleSystem>> loopingEffects = new();

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach(EffectInfo info in registerEffects)
        {
            effects.Add(info.id, info.effect);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ParticleSystem GetEffect(string id)
    {
        return effects[id];
    }

    public void PlayFX(string id, float duration,Vector3 pos,bool isLoop = false,bool isDestroy = false)
    {
        var fx = Instantiate(GetEffect(id),pos,Quaternion.identity);
        var main = fx.main;
        main.loop = (isLoop) ? true : false;
        
        if(isDestroy)
        {
            main.stopAction = ParticleSystemStopAction.Destroy;
        }
        if (isLoop)
        {
            if (loopingEffects.ContainsKey(id))
            {
                loopingEffects[id].Add(fx);
            } else
            {
                loopingEffects.Add(id, new List<ParticleSystem> { fx });
            }
        }
        fx.Play();
    }   

    public void StopFX(string id)
    {
        if(loopingEffects.ContainsKey(id))
        {
            for(int i = 0; i < loopingEffects[id].Count; i++)
            {
                Destroy(loopingEffects[id][i].gameObject);
            }
            loopingEffects[id].Clear();
        }
    }
}
