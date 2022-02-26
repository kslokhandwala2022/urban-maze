using Opsive.Shared.Events;
using UnityEngine;
using Opsive.UltimateCharacterController.Items;
using Opsive.UltimateCharacterController.Inventory;
using Opsive.UltimateCharacterController.Character;

public class WealthHandler : MonoBehaviour
{

    [Tooltip("The character")]
    [SerializeField] protected GameObject m_Character;

    public Item WealthPoint;
    
    private Animator animator;

    public float coeff = 0.01f;
    public int picked = 0;

    private float baseMotorX = 0;
    private float baseMotorZ = 0;

    public void Awake()
    {
        EventHandler.RegisterEvent<Item, int, bool, bool>(gameObject, "OnInventoryPickupItem", WealthPickupHandler);
        var characterLocomotion = m_Character.GetComponent<UltimateCharacterLocomotion>();

        animator = GetComponent<Animator>();
        if (characterLocomotion != null)
        {
            baseMotorX = characterLocomotion.MotorAcceleration.x;
            baseMotorZ = characterLocomotion.MotorAcceleration.z;
        }
    }

    private void WealthPickupHandler(Item item, int count, bool immediatePickup, bool forceEquip) {
        picked += count;

        float finalCoeff = (float)(1 + coeff * picked);

        var characterLocomotion = m_Character.GetComponent<UltimateCharacterLocomotion>();
        if (characterLocomotion != null)
        {
            characterLocomotion.MotorAcceleration = characterLocomotion.MotorAcceleration + new Vector3(baseMotorX * finalCoeff, 0, baseMotorZ * finalCoeff);
        }

        animator.SetBool("Picking", true);
        Invoke("RevertPicking", 1);
    }

    private void RevertPicking() {
        animator.SetBool("Picking", false);
    }

    public void OnDestroy()
    {
        EventHandler.UnregisterEvent<Item, int, bool, bool>(gameObject, "OnInventoryPickupItem", WealthPickupHandler);
    }
}
