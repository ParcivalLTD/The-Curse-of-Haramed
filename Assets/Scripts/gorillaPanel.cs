using UnityEngine;

public class gorillaPanel : IMonsterPanel
{
    public GameObject gorillaPrefab;
    public int unlockWave = 20;
    public bool hasBeenUnlocked = false;

    protected override void Start()
    {
        base.Start();
        SetImage(gorillaPrefab);
        SetMonsterName(gorillaPrefab);
        SetInfoText(gorillaPrefab);
    }
}