using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    public BoxPlacement[] placements; // 3つの箱の配置場所
    public MovingPlatform[] platforms; // 動かす足場（複数可）

    private bool puzzleSolved = false;

    void Update()
    {
        CheckSolution();
    }

    void CheckSolution()
    {
        foreach (var place in placements)
        {
            if (place.placedBox == null || place.placedBox.name != place.correctBoxName)
            {
                Debug.Log("まだ並んでいません！");
                return;
            }
        }

        Debug.Log("パズルクリア！ 足場が動きます！");
        puzzleSolved = true;

        // 足場を動かす
        foreach (var platform in platforms)
        {
            platform.StartMoving();
        }
    }
}
