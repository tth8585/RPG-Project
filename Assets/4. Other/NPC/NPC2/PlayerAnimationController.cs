
using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    public static PlayerAnimationController Instance { get; set; }

    private const string IDLE_ANIM_BOOL = "idle";
    private const string DEATH_ANIM_BOOL = "die";
    private const string WALK_ANIM_BOOL = "walk";
    private const string RUN_ANIM_BOOL = "run";
    private const string DANCE_ANIM_BOOL = "dance";
    private const string DANCE2_ANIM_BOOL = "dance2";
    private const string JUMP_ANIM_BOOL = "jump";
    private const string CAST_ANIM_BOOL = "cast";
    private const string HURT_ANIM_BOOL = "hurt";
    private const string WALKB_ANIM_BOOL = "walkB";
    private const string WALKL_ANIM_BOOL = "walkL";
    private const string WALKR_ANIM_BOOL = "walkR";
    private const string RUNB_ANIM_BOOL = "runB";
    private const string RUNL_ANIM_BOOL = "runL";
    private const string RUNR_ANIM_BOOL = "runR";

    private const string DEATH2_ANIM_BOOL = "die2";
    private const string DEATH3_ANIM_BOOL = "die3";
    private const string HURT2_ANIM_BOOL = "hurt2";
    private const string UNEQUIP_ANIM_BOOL = "unequip";

    private const string EQUIP_ANIM_BOOL = "equip";

    private const string XDIR_ANIM_FLOAT = "xDir";
    private const string ZDIR_ANIM_FLOAT = "zDir";


    private const string BUFF_ANIM_STRING = "BuffSpell";
    private const string TARGET_ANIM_STRING = "TargetSpell";
    private const string AREA_ANIM_STRING = "AreaSpell";
    private const string AURA_ANIM_STRING = "AuraSpell";

    private const string BUFFB_ANIM_STRING = "BuffSpellB";
    private const string TARGETB_ANIM_STRING = "TargetSpellB";
    private const string AREAB_ANIM_STRING = "AreaSpellB";
    private const string AURAB_ANIM_STRING = "AuraSpellB";

    private const string ATK_ANIM_STRING = "Attack";
    private const string ATKB_ANIM_STRING = "AttackB";
    //=============================
    //duration delay anim
    float equipDuration = 0.9f;
    float unequipDuration = 1.1f;
    float buffDuration = 2.267f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        //anim = GetComponent<Animator>();
    }
    public void SetXDir(float amount)
    {
        anim.SetFloat(XDIR_ANIM_FLOAT, amount);
    }
    public void SetZDir(float amount)
    {
        anim.SetFloat(ZDIR_ANIM_FLOAT, amount);
    }
    //===================
    public void AnimateSpellBuff()
    {
        if (anim.GetLayerWeight(1) == 0)
        {
            anim.Play(BUFF_ANIM_STRING);
        }
        else
        {
            anim.Play(BUFFB_ANIM_STRING);
        }
    }
    public void AnimateSpellTarget()
    {
        if (anim.GetLayerWeight(1) == 0)
        {
            anim.Play(TARGET_ANIM_STRING);
        }
        else
        {
            anim.Play(TARGETB_ANIM_STRING);
        }
    }
    public void AnimateSpellArea()
    {
        if (anim.GetLayerWeight(1) == 0)
        {
            anim.Play(AREA_ANIM_STRING);
        }
        else
        {
            anim.Play(AREAB_ANIM_STRING);
        }
    }
    public void AnimateSpellAura()
    {
        if (anim.GetLayerWeight(1) == 0)
        {
            anim.Play(AURA_ANIM_STRING);
        }
        else
        {
            anim.Play(AURAB_ANIM_STRING);
        }
    }

    public void AnimateAttack()
    {
        if (anim.GetLayerWeight(1) == 0)
        {
            anim.Play(ATK_ANIM_STRING);
        }
        else
        {
            anim.Play(ATKB_ANIM_STRING);
        }
    }

    //============================
    public void AnimateIdle()
    {
        Animate(IDLE_ANIM_BOOL);
    }
    bool isDead = false;
    public void AnimateDeath()
    {
        int randomNumber = Random.Range(0, 2 + 1);
      
        if (randomNumber == 0)
        {
            Animate(DEATH_ANIM_BOOL);
        }
        else if(randomNumber == 1)
        {
            Animate(DEATH2_ANIM_BOOL);
        }
        else
        {
            Animate(DEATH3_ANIM_BOOL);
        }
        isDead = true;
    }
    public void AnimateWalk()
    {
        Animate(WALK_ANIM_BOOL);
    }
    public void AnimateRun()
    {
        Animate(RUN_ANIM_BOOL);
    }
    public void AnimateDance()
    {
        int randomNumber = Random.Range(0, 1 + 1);
        Animate(IDLE_ANIM_BOOL);

        if (randomNumber == 0)
        {
            Animate(DANCE_ANIM_BOOL);
        }
        else
        {
            Animate(DANCE2_ANIM_BOOL);
        }
    }
    public void AnimateJump()
    {
        Animate(JUMP_ANIM_BOOL);
    }
    public bool isCasting = false;
    public void AnimateCast()
    {
        if (anim.GetLayerWeight(1) == 0)
        {
            return;
        }

        StopAllCoroutines();

        if (!isDoingSomething)
        {
            isDoingSomething = true;
            Animate(CAST_ANIM_BOOL);
            StartCoroutine(DelayAnim(buffDuration));
        }
        else
        {
            isDoingSomething = false;
            StopCoroutine(DelayAnim(buffDuration));
        }
    }
    public void AnimateHurt()
    {
        int randomNumber = Random.Range(0, 1 + 1);

        if (randomNumber == 0)
        {
            Animate(HURT_ANIM_BOOL);
        }
        else
        {
            Animate(HURT2_ANIM_BOOL);
        }

        if (anim.GetLayerWeight(1) != 1)
        {
            anim.SetLayerWeight(1, 1);
        }
    }
    public void AnimateWalkB()
    {
        Animate(WALKB_ANIM_BOOL);
    }
    public void AnimateWalkL()
    {
        Animate(WALKL_ANIM_BOOL);
    }
    public void AnimateWalkR()
    {
        Animate(WALKR_ANIM_BOOL);
    }
    public void AnimateRunB()
    {
        Animate(RUNB_ANIM_BOOL);
    }
    public bool isDoingSomething = false;
    public void AnimateEquip()
    {
        if (anim.GetLayerWeight(1) != 1)
        {
            anim.SetLayerWeight(1, 1);
        }
        isDoingSomething = true;
        Animate(EQUIP_ANIM_BOOL);
        StartCoroutine(DelayAnim(equipDuration));
    }
    public void AnimateUnequip()
    {
        if (anim.GetLayerWeight(1) != 0)
        {
            anim.SetLayerWeight(1, 0);
        }
        isDoingSomething = true;
        Animate(UNEQUIP_ANIM_BOOL);
        StartCoroutine(DelayAnim(unequipDuration));
    }
    public void AnimateRunL()
    {
        Animate(RUNL_ANIM_BOOL);
    }
    public void AnimateRunR()
    {
        Animate(RUNR_ANIM_BOOL);
    }

    //=========================
    private void Animate(string boolName)
    {
        if (isDead)
        {
            return;
        }
        DisableOtherAnim(anim, boolName);
        anim.SetBool(boolName, true);
    }
    private void DisableOtherAnim(Animator animator, string animation)
    {
        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != animation && parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }
    //================
    private IEnumerator DelayAnim(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDoingSomething = false;
    }
}
