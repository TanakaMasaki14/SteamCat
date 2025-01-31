using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    public BoxPlacement[] placements; // すべての配置ポイント

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーでチェック
        {
            CheckSolution();
        }
    }

    void CheckSolution()
    {
        foreach (var place in placements)
        {
            if (place.gameObject.GetComponent<BoxPlacement>().placedBox == null)
            {
                Debug.Log("すべての箱を配置してください！");
                return;
            }
        }

        Debug.Log("パズルクリア！");
        // クリア処理（エフェクト・シーン遷移など）
    }
}
