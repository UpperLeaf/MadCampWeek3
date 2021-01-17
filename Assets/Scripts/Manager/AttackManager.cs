using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    private GameObject xAttack;
    [SerializeField]
    private GameObject aAttack;


    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private Transform defaultAttackPos;

    private PlayerState _playerState;

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
        xAttack.GetComponent<AbstractAttack>().Attack(_player.attackDamage, defaultAttackPos, _playerState);
    }


    public void AttackByInputA()
    {
        aAttack.GetComponent<AbstractAttack>().Attack(_player.magicDamage, defaultAttackPos, _playerState);
    }


    private void AttachFireballAttack(ref GameObject attach)
    {
        Destroy(attach);
        
        GameObject fireballObject = new GameObject("FireballAttack");
        fireballObject.transform.SetParent(PlayerManager.Instance.getPlayer().transform);

        FireballAttack fireballAttack = fireballObject.AddComponent<FireballAttack>();
        fireballAttack.SetFireball(fireball);

        attach = fireballObject;
    }

    private void AttachSlashAttack(ref GameObject attach)
    {
        Destroy(attach);
        GameObject slash = new GameObject("SlashAttack");
        slash.transform.SetParent(PlayerManager.Instance.getPlayer().transform);
        slash.AddComponent<SlashAttack>();

        attach = slash;
    }
}
