﻿using System.Collections;
using Neuro.Cursor;
using UnityEngine;

namespace Neuro.Minigames.Solvers;

[MinigameSolver(typeof(MonitorOxyMinigame))]
public class MonitorTreeSolver : TaskMinigameSolver<MonitorOxyMinigame>
{
    protected override IEnumerator CompleteMinigame(MonitorOxyMinigame minigame, NormalPlayerTask task)
    {
        for (int i = 0; i < minigame.Sliders.Count; i++)
        {
            yield return InGameCursor.Instance.CoMoveTo(minigame.Sliders[i], 0.5f);
            InGameCursor.Instance.StartHoldingLMB(minigame.Sliders[i]);
            // gotta deal with the crappy hitbox positioning again
            Vector2 direction = minigame.Sliders[i].transform.localPosition.y > minigame.Targets[i].transform.localPosition.y ? new Vector2(0f, -0.1f) : new Vector2(0f, 0.1f);
            while (Mathf.Abs(minigame.Sliders[i].transform.localPosition.y - minigame.Targets[i].transform.localPosition.y) > 0.1f)
            {
                InGameCursor.Instance.SnapTo(InGameCursor.Instance.Position + direction);
                yield return null;
            }
            InGameCursor.Instance.StopHoldingLMB();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
