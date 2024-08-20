using UnityEngine;

public class Giraffe : MonoBehaviour
{
    private bool hasEatenApple = false;

    public void EatApple()
    {
        if (!hasEatenApple)
        {
            hasEatenApple = true;
            Debug.Log("Giraffe ate an apple!");
            CheckForWin();
        }
    }

    private void CheckForWin()
    {
        if (hasEatenApple)
        {
            Debug.Log("You Win! The giraffe ate the apple.");
        }
    }
}
