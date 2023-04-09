﻿using System.Collections;
using Neuro.Cursor;
using UnityEngine;

namespace Neuro.Minigames.Solvers;

[MinigameSolver(typeof(SortMinigame))]
public class SortSamplesSolver : TaskMinigameSolver<SortMinigame>
{
    protected override IEnumerator CompleteMinigame(SortMinigame minigame, NormalPlayerTask task)
    {
        bool secondPlant = false, secondAnimal = false, secondMineral = false;
        Vector3 left = new(-0.5f, 0f);
        Vector3 right = new(0.5f, 0f);
        foreach (SortGameObject obj in minigame.Objects)
        {
            yield return InGameCursor.Instance.CoMoveTo(obj);
            InGameCursor.Instance.StartHoldingLMB(obj);
            switch (obj.MyType)
            {
                case SortGameObject.ObjType.Plant:
                    yield return InGameCursor.Instance.CoMoveTo(minigame.PlantBox.transform.position + (secondPlant ? right : left));
                    secondPlant = true;
                    break;
                case SortGameObject.ObjType.Animal:
                    yield return InGameCursor.Instance.CoMoveTo(minigame.AnimalBox.transform.position + (secondAnimal ? right : left));
                    secondAnimal = true;
                    break;
                case SortGameObject.ObjType.Mineral:
                    yield return InGameCursor.Instance.CoMoveTo(minigame.MineralBox.transform.position + (secondMineral ? right : left));
                    secondMineral = true;
                    break;
            }
            InGameCursor.Instance.StopHoldingLMB();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
