using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraSword,
        //ExtraSword2,
        Healing,
        SpeedIncrease,
    }

    public ItemType type;

    //[SerializeField] GameObject sword1;
    //[SerializeField] GameObject sword2;
    //[SerializeField] GameObject sword3;

    public GameObject sword1;
    public GameObject sword2;
    public GameObject sword3;

    public int weaponTier = 1;

    public int healthBouns = 4;
    public int walkSpeedBouns = 1;



    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraSword:
                weaponTier++;
                //player.GetComponent<BombController>().AddBomb();
                if (weaponTier == 2)
                {
                    sword1.SetActive(false);
                    sword2.SetActive(true);
                }
                if (weaponTier == 3)
                {
                    sword1.SetActive(false);
                    sword2.SetActive(false);
                    sword3.SetActive(true);
                }
                break;

            //case ItemType.ExtraSword2:
            //    if (weaponTier == 2)
            //    {
            //        sword1.SetActive(false);
            //        sword2.SetActive(false);
            //        sword3.SetActive(true);
            //        weaponTier++;
            //    }
            //    break;


            //case ItemType.ExtraSword2:
            //    sword1.SetActive(false);
            //    sword2.SetActive(false);
            //    sword3.SetActive(true);
            //    weaponTier++;
            //    break;

            case ItemType.Healing:
                player.GetComponent<Player>().currentHealth += healthBouns;
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<Player>().walkSpeed += walkSpeedBouns;
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }

}