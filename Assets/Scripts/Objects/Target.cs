using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private Text targetCount;

    private void Update()
    {
        ShowTargetCount();
        CheckTargetsStatus();
        if (targets.Count == 0) CompleteLevel();
    }

    private void ShowTargetCount()
    {
        targetCount.text = "Targets : " + targets.Count;
    }

    private void CheckTargetsStatus()
    {
        foreach (var target in targets)
        {
            if (target == null)
            {
                targets.Remove(target);
            }
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
