using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private GameObject xAttack;
    [SerializeField]
    private GameObject aAttack;
    [SerializeField]
    private GameObject sAttack;

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


    private Player _player;

    void Awake()
    {
        xAttack = new GameObject("AttackByKeyCodeX");
        aAttack = new GameObject("AttackByKeyCodeA");
        sAttack = new GameObject("AttackByKeyCodeS");
    }
    private void Start()
    {
        AttachSlashAttack(ref xAttack);
        AttachFireballAttack(ref aAttack);
        AttachDarknessAttack(ref sAttack);
        _player = GetComponent<Player>();
        _playerState = GetComponent<PlayerState>();
    }

    public bool isAttackAbleX()
    {
        bool isAttackable = xAttack.GetComponent<AbstractAttack>().IsAttackable();
        if(isAttackable)
            xAttack.GetComponent<AbstractAttack>().SetAttackable(false);
        return isAttackable;
    }

    public bool isAttackAbleA()
    {
        bool isAttackable = aAttack.GetComponent<AbstractAttack>().IsAttackable();
        if (isAttackable)
            aAttack.GetComponent<AbstractAttack>().SetAttackable(false);
        return isAttackable;
    }

    public bool isAttackAbleS()
    {
        bool isAttackable = sAttack.GetComponent<AbstractAttack>().IsAttackable();
        if (isAttackable)
            sAttack.GetComponent<AbstractAttack>().SetAttackable(false);
        return isAttackable;
    }


    public void AttackByInput(KeyCode key)
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
        }

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

        attack.GetComponent<AbstractAttack>().Attack(damage, distance, _playerState);
    }

    private void AttachFireballAttack(ref GameObject attach)
    {
        attach = Instantiate(fireballAttack, gameObject.transform, true);
    }

    private void AttachSlashAttack(ref GameObject attach)
    {
        attach = Instantiate(slashAttack, gameObject.transform, true);
    }

    private void AttachDarknessAttack(ref GameObject attach)
    {
        attach = Instantiate(darknessAttack, gameObject.transform, true);
    }
}
