using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameRequirement
{
    public RequirementType type;
    public enum RequirementType
    {
        has
    }
    List<int> targets;

    public bool check(long user)
    {
        if (type == RequirementType.has)
        {
            Contain checking;
            if (targets[0] == -1)
                checking = Global.ID[user].GetObject().Inventory;
            else checking = Global.ID[targets[0]].GetObject().Inventory;
            foreach (int id in checking.Items)
            {
                if (id == targets[1])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
