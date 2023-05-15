using UnityEngine;

public class platapusPanel : IMonsterPanel
{
    public GameObject platapusPrefab;
    public int unlockWave = 10;
    public bool hasBeenUnlocked = false;

    protected override void Start()
    {
        base.Start();
        SetImage(platapusPrefab);
        SetMonsterName(platapusPrefab);
        SetInfoText(platapusPrefab);
    }
}