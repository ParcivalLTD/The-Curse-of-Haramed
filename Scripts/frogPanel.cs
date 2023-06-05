using UnityEngine;

public class frogPanel : IMonsterPanel
{
    public GameObject frogPrefab;
    public int unlockWave = 30;
    public bool hasBeenUnlocked = false;

    protected override void Start()
    {
        base.Start();
        SetImage(frogPrefab);
        SetMonsterName(frogPrefab);
        SetInfoText(frogPrefab);
    }
}