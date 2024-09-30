using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public readonly struct InteractEvent : IEvent
{
    public readonly int Animation;
    public readonly int Delay;

    public InteractEvent(string interactType)
    {
        Animation = InteractingType.InteractingValues[interactType][0];
        Delay = InteractingType.InteractingValues[interactType][1];
    }
}
public readonly struct ChangeItemEvent : IEvent
{
    public readonly Item Item;
    public readonly int Count;
    public ChangeItemEvent(Item item, int count)
    {
        Item = item;
        Count = count;
    }
}
public readonly struct ChangeMoneyEvent : IEvent
{
    public readonly int Price;
    public ChangeMoneyEvent(int price)
    {
        Price = price;
    }
}
public readonly struct ChangeIndicatorsHungryEvent : IEvent
{
    public readonly int Food;
    public ChangeIndicatorsHungryEvent(int food)
    {
        Food = food;
    }
}
public readonly struct ChangeIndicatorsFluitEvent : IEvent
{   
    public readonly int Fluit;
    public ChangeIndicatorsFluitEvent(int fluit)
    {
        Fluit = fluit;
    }
}
public readonly struct ChangeExperienceEvent : IEvent
{
    public readonly int Experience;
    public ChangeExperienceEvent(int experience)
    {
        Experience = experience;
    }
}
public readonly struct UpLevelEvent : IEvent
{

}
public readonly struct TeleportEvent : IEvent
{

}
public readonly struct ChangeSlotEvent : IEvent
{
    public readonly int Id;
    public ChangeSlotEvent(int id)
    {
        Id = id;
    }
}
public readonly struct InteractMenuEvent : IEvent
{
    public readonly bool InteractMenu;
    public InteractMenuEvent(bool interactMenu)
    {
        InteractMenu = interactMenu;
    }
}
public readonly struct PlayerInZoneEvent : IEvent
{
    public readonly bool InZone;
    public PlayerInZoneEvent(bool inZone)
    {
        InZone = inZone;
    }
}
public readonly struct PlayerInShopZoneInteractEvent : IEvent
{
    public readonly bool InZoneInteract;
    public PlayerInShopZoneInteractEvent(bool inZone)
    {
        InZoneInteract = inZone;
    }
}