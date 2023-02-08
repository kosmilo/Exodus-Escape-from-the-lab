using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();

    }

    public Item GetItem(int id) // find an item by id
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName) // find an item by title
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase() // item database
    {
        items = new List<Item>()
        {
                new Item(0, "Rusty key", "An old, rusty key. Used to unlock the surgery room door.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }),
                new Item(1, "Red vial", "A vial of strange liquid. Seems to have a slowing effect but evaporates quickly.",
                new Dictionary<string, int>
                {
                    {"Slow", 10 },
                    {"Durability", 1 },
                    {"Lifespan", 10 } //seconds
                }),
                new Item(2, "Murky vial", "A vial of strange liquid. Seems to have a slightly slowing effect. It doesn't evaporate that quickly.",
                new Dictionary<string, int>
                {
                    {"Slow", 3 },
                    {"Durability", 1 },
                    {"Lifespan", 40 }
                }),
                new Item(4, "Purple vial", "A vial of strange liquid. Seems to cause brief incapacitation but evaporates quickly.",
                new Dictionary<string, int>
                {
                    {"Stun", 5 }, //seconds
                    {"Durability", 1 },
                    {"Lifespan", 10 }
                }),
                new Item(5, "Translucent vial", "A vial of strange liquid. Seems to cause incapacitation but evaporates quickly.",
                new Dictionary<string, int>
                {
                    {"Stun", 15 },
                    {"Durability", 1 },
                    {"Lifespan", 10 }
                }),
                new Item(6, "First aid kit", "A first aid kit commonly found in medical facilities.",
                new Dictionary<string, int>
                {
                    {"Heal", 50 },
                    {"Durability", 1 },
                    {"Usage time", 5 } //seconds
                }),
                new Item(7, "Bandage", "A roll of elastic bandage commonly found in medical facilities.",
                new Dictionary<string, int>
                {
                    {"Heal", 15 },
                    {"Durability", 1 },
                    {"Usage time", 3 }
                }),
                new Item(8, "Iron key", "A spotless iron key. Used to unlock the first guard office.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }),
                new Item(9, "Stained iron key", "An iron key stained by something. Used to unlock the second guard office.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }),
                new Item(10, "Video cassette", "A retro video cassette. Requires a VCR to play.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }),
                new Item(11, "Level 3 keycard", "A keycard with access to the cell and guard wing.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }),
                new Item(12, "Elevator control panel part", "A bundle of cables and machine parts. Can be used to fix the elevator.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                })
        };
    }
}

