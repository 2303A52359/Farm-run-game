
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [Header("Roll Manager Reference")]
    public RollManager rollManager;
    public ObstacleManager obstacleManager;
    public BgMovemntManager bgMovemntManager;
    public ScoreManager scoreManager;
    public PlayerMovementToggle PlayerMovement;
    public Animator animator;
    public GameObject deathUI;


    public void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void SetGroundedFalse()
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", false);
        }
    }

    public void SetGroundeTrue()
    {
        if (animator != null)
        {
            animator.SetBool("isGrounded", true);
        }
    }


    public void EndRoll()
    {
        if (rollManager != null)
        {
            rollManager.EndRoll();
        }
        else
        {
            Debug.LogWarning("RollManager reference is missing!");
        }
    }
    public void DeathFunctions()
    {
        if (obstacleManager != null)
        {
            obstacleManager.StopObstacleLoopAndMovement();
            bgMovemntManager.StopMovement();
            scoreManager.StopScoreIncrease();
            PlayerMovement.DisableMovement();
            deathUI.SetActive(true);
            Debug.Log("Death Functions Worked!!!");
        }
        else
        {
            Debug.LogWarning("Some reference is null!");
        }
    }



}
