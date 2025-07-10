using UnityEngine;

public class GameEntity : MonoBehaviour 
{
    // Common properties for all game entities
    public float health;
    public float healthRegen;
    public float mana;
    public float manaRegen;
    
    public int armor;
    public int armorUpgradeLevel;
    public float armorType;
    public Vector3 position;
    public bool isDead = false;
    public bool isMoving = false;
    public int unitFaction = 0;
    public float walkSpeed;
    public float attackSpeed;
    public float weaponDamage;
    public float weaponUpgradeLevel;

    public string entityName;
    public bool isCriticalEntity = false;

    public int casterTraining;


    public bool isResource;
    public GameResource resourceType;
    public float resourceAmount;

    // Size settings for placement and collision
    public SizeCategory sizeCategory = SizeCategory.Small;
    public float sizeRadius;
}
public enum GameResource
{
    Gold,
    Lumber,
    Steel,
}
