using UnityEngine;

public class AnimationSoundEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void attack1()
    {
        SoundManager.Instance.PlayAttack1SFX();
    }

    public void attack2()
    {
        SoundManager.Instance.PlayAttack2SFX();
    }
    public void attack3()
    {
        SoundManager.Instance.PlayAttack3SFX();
    }

    public void attackLong()
    {
        SoundManager.Instance.PlayAttack_LongFX();
    }

    public void skill()
    {
        SoundManager.Instance.PlayAttack_SkillFX();
    }

    public void explosive()
    {
        SoundManager.Instance.PlayExplosiveFX();
    }

    public void footstep()
    {
        SoundManager.Instance.PlayPlayerFootstep();
    }

    public void attackPocong()
    {
        SoundManager.Instance.PlayPocongAttack(0.3f);
    }
}

