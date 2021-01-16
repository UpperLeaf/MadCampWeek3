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

    void Awake()
    {
        xAttack = new GameObject("AttackByKeyCodeX");
        aAttack = new GameObject("AttackByKeyCodeA");
    }
    private void Start()
    {
        AttachSlashAttack(ref xAttack);
        AttachFireballAttack(ref aAttack);

        _playerState = GetComponent<PlayerState>();
    }

    public void AttackByInputX(int damage)
    {
        xAttack.GetComponent<AbstractAttack>().Attack(damage, defaultAttackPos, _playerState);
    }


    public void AttackByInputA(int damage)
    {
        aAttack.GetComponent<AbstractAttack>().Attack(damage, defaultAttackPos, _playerState);
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
