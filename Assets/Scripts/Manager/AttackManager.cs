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

    public void AttackByInputX()
    {
        xAttack.GetComponent<AbstractAttack>().Attack(_player.attackDamage, attackPos.transform, _playerState);
    }


    public void AttackByInputA()
    {
        aAttack.GetComponent<AbstractAttack>().Attack(_player.magicDamage, attackPos.transform, _playerState);
    }

    public void AttackByInputS()
    {
        sAttack.GetComponent<AbstractAttack>().Attack(_player.magicDamage / 2, distanceAttackPos.transform, _playerState);
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
