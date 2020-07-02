using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    public static float GetDifficultyRate(float difficultySeconds = 60f) {
        return Mathf.Clamp(Time.timeSinceLevelLoad / difficultySeconds, 0 , 1);
    }

}
