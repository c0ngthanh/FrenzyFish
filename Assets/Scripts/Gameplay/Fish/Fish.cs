using UnityEngine;
using System;

public class Fish : MonoBehaviour
{
    [SerializeField] private int size = 1;
    [SerializeField] private float damage = 0;
    [SerializeField] private float exp = 0;
    [SerializeField] protected float swimSpeed = 3f;
    [SerializeField] protected float sprintSpeed = 20f;
    [SerializeField] private FishSpawner fishSpawner;
    [SerializeField] private ParticleSystem bloodSplatterEffect;


    private float score = 0;
    private Animator fishAnimator;


    void Start()
    {
        fishAnimator = GetComponent<Animator>();
    }

    public void SetIsSwimming(bool value)
    {
        fishAnimator?.SetBool("isSwimming", value);
    }
    public void SetAnimationSpeed(float playSpeed = 1f)
    {
        fishAnimator.speed = playSpeed;
    }

    public void Eat(Fish otherFish)
    {
        if (!this.GetComponentInParent<Player>()) return;

        Player player = this.GetComponentInParent<Player>();
        player.PlayBiteSound();
        if (this.size > otherFish.size)
        // Player attack other fish
        {
            Instantiate(bloodSplatterEffect, this.transform.position, Quaternion.identity);
            // Disable fish and then respawn them
            fishSpawner.DeactivateFish(otherFish);

            // Take exp from otherFish
            player.exp += otherFish.exp;
            // Take health from other fish
            player.setHealth(otherFish.exp * 4);
            player.GetExpUIController().OnPlayerEating(otherFish.exp);
            this.score += 10 + otherFish.size * 10;
            if (player.exp >= player.maxExp)

            {
                // if exp > Max exp in this level, Levelup this fish
                player.LevelUp();
            }
        }

        // Player get attacked by other fish
        else
        {
            // need some delay
            player.setHealth(-otherFish.damage);
            Debug.Log("Player get attacked " + player.getCurrentHealth() + "HP");

            // Endgame(this);
        }

    }
    public void TakePresent()
    {
        this.swimSpeed *= 2;
    }
    private void Endgame(Fish otherFish)
    {
        Time.timeScale = 0;
        otherFish.GetComponentInParent<Player>().GetEndGameCamera().OpenEndGameMenu();
        otherFish.GetComponentInParent<Player>().gameObject.SetActive(false);
    }
    public void LevelUp()
    {
        // obsolete
        return;
        // this.exp = this.exp - this.maxExp;
        // this.level += 1;
        // this.maxExp = this.level * 2;
        // onLevelUp?.Invoke(this, EventArgs.Empty);
    }
    public int GetSize()
    {
        return this.size;
    }
    public void SetSize(int size)
    {
        this.size = size;
    }
    public float GetExp()
    {
        return this.exp;
    }
    public float GetMaxExp()
    {
        // obsolete
        return 0f;
    }
    public float GetScore()
    {
        return this.score;
    }
}
