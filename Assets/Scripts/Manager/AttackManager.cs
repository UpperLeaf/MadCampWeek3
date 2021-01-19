using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private GameObject xAttack;
    [SerializeField]
    private GameObject aAttack;
    [SerializeField]
    private GameObject sAttack;
    [SerializeField]
    private GameObject dAttack;

    private PlayerState _playerState;

    [SerializeField]
    private GameObject attackPos;

    [SerializeField]
    private GameObject distanceAttackPos;

    [SerializeField]
    private GameObject slashAttack;

    [SerializeField]
    private GameObject fireballAttack;

    [SerializeField]
    private GameObject darknessAttack;

    [SerializeField]
    private GameObject swordAttack;

    private Player _player;
    
    private void Start()
    {
        AttachSlashAttack();        
        _player = GetComponent<Player>();
        _playerState = GetComponent<PlayerState>();
    }

    public bool isAttackAble(KeyCode key)
    {
        GameObject attack = findAttackByKeyCode(key);
        if (attack == null)
            return false;
        bool isAttackable = attack.GetComponent<AbstractAttack>().IsAttackable();

        return isAttackable;
    }

    public AbstractAttack.AttackType getAttackType(KeyCode key)
    {
        return findAttackByKeyCode(key).GetComponent<AbstractAttack>().attackType;
    }

    private GameObject findAttackByKeyCode(KeyCode key)
    {
        GameObject attack = null;
        switch (key)
        {
            case KeyCode.X:
                attack = xAttack;
                break;
            case KeyCode.A:
                attack = aAttack;
                break;
            case KeyCode.S:
                attack = sAttack;
                break;
            case KeyCode.D:
                attack = dAttack;
                break;
        }
        return attack;
    }   

    public void AttackByInput(KeyCode key)
    {
        GameObject attack = findAttackByKeyCode(key);

        if (attack == null)
            return;
      
        AbstractAttack.AttackType attackType = attack.GetComponent<AbstractAttack>().attackType;
        AbstractAttack.DistanceType distanceType = attack.GetComponent<AbstractAttack>().distanceType;

        int damage;
        Transform distance;
        
        if (attackType.Equals(AbstractAttack.AttackType.ATTACK))
            damage = _player.attackDamage;
        else
            damage = _player.magicDamage;


        if (distanceType.Equals(AbstractAttack.DistanceType.NEAR))
            distance = attackPos.transform;
        else
            distance = distanceAttackPos.transform;
        attack.GetComponent<AbstractAttack>().SetAttackable(false);
        attack.GetComponent<AbstractAttack>().Attack(damage, distance, _playerState);
    }

    public void AttachFireballAttack()
    {
        if (aAttack != null)
            Destroy(aAttack);
        aAttack = Instantiate(fireballAttack, gameObject.transform, true);
    }

    public void AttachSlashAttack()
    {
        if (xAttack != null)
            Destroy(xAttack);
        xAttack = Instantiate(slashAttack, gameObject.transform, true);
    }

    public void AttachDarknessAttack()
    {
        if (sAttack != null)
            Destroy(sAttack);
        sAttack = Instantiate(darknessAttack, gameObject.transform, true);
    }

    public void AttachSwordAttack()
    {
        if (dAttack != null)
            Destroy(dAttack);
        dAttack = Instantiate(swordAttack, gameObject.transform, true);
    }
}
