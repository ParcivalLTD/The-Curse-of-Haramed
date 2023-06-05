using UnityEngine;

public class magigMirtPanel : IMonsterPanel
{
    public GameObject magicPrefab;
    public int unlockWave = 40;
    public bool hasBeenUnlocked = false;

    protected override void Start()
    {
        base.Start();
        SetImage(magicPrefab);
        SetMonsterName(magicPrefab);
        SetInfoText(magicPrefab);
    }
}