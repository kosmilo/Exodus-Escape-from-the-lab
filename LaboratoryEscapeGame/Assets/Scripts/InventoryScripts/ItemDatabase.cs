using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds information of all items in the game
// Item id 3 does not exist?
public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id) // find and return an item from the created list by id
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName) // find and return an item from the created list by title
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase() // Creates a list of all items in the game
    {
        items = new List<Item>()
        {
                new Item(0, "Rusty key", "An old, rusty key. Used to unlock the surgery room door.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }, false, false),
                new Item(1, "Red vial", "A vial of strange liquid. Seems to have a slowing effect.",
                new Dictionary<string, int>
                {
                    {"Slow", 10 },
                    {"SlowTime", 5 },
                    {"Durability", 1 }
                }, true, false),
                new Item(2, "Murky vial", "A vial of strange liquid. Seems to have a slightly slowing effect.",
                new Dictionary<string, int>
                {
                    {"Slow", 3 },
                    {"SlowTime", 5 },
                    {"Durability", 1 }
                }, true, false),
                new Item(3, "Purple vial", "A vial of strange liquid. Seems to cause brief incapacitation.",
                new Dictionary<string, int>
                {
                    {"Stun", 5 }, //seconds
                    {"Durability", 1 }
                }, true, false),
                new Item(4, "Translucent vial", "A vial of strange liquid. Seems to cause incapacitation.",
                new Dictionary<string, int>
                {
                    {"Stun", 15 },
                    {"Durability", 1 }
                }, true, false),
                new Item(5, "First aid kit", "A first aid kit commonly found in medical facilities.",
                new Dictionary<string, int>
                {
                    {"Heal", 50 },
                    {"Durability", 1 },
                    {"Usage time", 5 } //seconds
                }, false, true),
                new Item(6, "Bandage", "A roll of elastic bandage commonly found in medical facilities.",
                new Dictionary<string, int>
                {
                    {"Heal", 15 },
                    {"Durability", 1 },
                    {"Usage time", 3 }
                }, false, true),
                new Item(7, "Iron key", "A spotless iron key. Used to unlock the first guard office.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }, false, false),
                new Item(8, "Stained iron key", "An iron key stained by something. Used to unlock the second guard office.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }, false, false),
                new Item(9, "Video cassette", "A retro video cassette. Requires a VCR to play.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }, false, false),
                new Item(10, "Level 3 keycard", "A keycard with access to the cell and guard wing.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }, false, false),
                new Item(11, "Machine parts", "A bundle of cables and machine parts. Might be useful in fixing the elevator.",
                new Dictionary<string, int>
                {
                    {"Durability", 99 }
                }, false, false)
        };
    }
}


