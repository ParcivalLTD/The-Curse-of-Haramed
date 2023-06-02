using UnityEngine;

public class catPanel : IMonsterPanel
{
    public GameObject catPrefab;
    public bool hasBeenUnlocked = false;

    protected override void Start()
    {
        base.Start();
        SetImage(catPrefab);
        SetMonsterName(catPrefab);
        SetInfoText(catPrefab);
    }
}