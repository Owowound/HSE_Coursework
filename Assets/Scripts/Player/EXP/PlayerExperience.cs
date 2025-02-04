using UnityEngine;

public class PlayerExperience : ScriptableObject
{
    private int requiredNumberofEXP = 100;

    private int currentEXP = 0;

    public void AddEXP(int EXP)
    {
        currentEXP += EXP;

        //ChangeLevel();
        // TODO как только будет готова логика перемещения между уровнями добавить окно прокачки после каждого уровня
    }
}
