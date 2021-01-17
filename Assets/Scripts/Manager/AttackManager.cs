using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private GameObject xAttack;
    [SerializeField]
    private GameObject aAttack;

    private PlayerState _playerState;

    [SerializeField]
    private GameObject attackPos;

    [SerializeField]
    private GameObject slashAttack;

    [SerializeField]
    private GameObject fireballAttack;

    private Player _player;

    void Awake()
    {
        xAttack = new GameObject("AttackByKeyCodeX");
        aAttack = new GameObject("AttackByKeyCodeA");
    }
    private void Start()
    {
        AttachSlashAttack(ref xAttack);
        AttachFireballAttack(ref aAttack);

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

    public void AttackByInputX()
    {
        xAttack.GetComponent<AbstractAttack>().Attack(_player.attackDamage, attackPos.transform, _playerState);
    }


    public void AttackByInputA()
    {
        aAttack.GetComponent<AbstractAttack>().Attack(_player.magicDamage, attackPos.transform, _playerState);
    }


    private void AttachFireballAttack(ref GameObject attach)
    {
        attach = Instantiate(fireballAttack, gameObject.transform, true);
    }

    private void AttachSlashAttack(ref GameObject attach)
    {
        attach = Instantiate(slashAttack, gameObject.transform, true);
    }
}
