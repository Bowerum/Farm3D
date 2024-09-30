using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour, IEventReceiver<PlayerInZoneEvent>
{
    [SerializeField] private EventBus _eventBus;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _soilMask;
    [SerializeField] private LayerMask _waterSoilMask;
    [SerializeField] private LayerMask _plantMask;

    [SerializeField] private List<GameObject> _plants;

    [SerializeField] private GameObject _hand;

    private bool _canInteract = true;

    private PlayerInputActions _inputActions;
    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<PlayerInZoneEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<PlayerInZoneEvent>);
    }
    private void Start()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(_canInteract)
        {
            Item activeItem = PlayerInventory.ItemsInventory[Inventory.ActiveSlot].Item;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit plant, 1, _plantMask))
            {
                if (plant.collider.gameObject.GetComponent<GrowPlant>().IsGrown)
                    StartCoroutine(PullPlant(plant.collider.gameObject));
            }
            else if (activeItem.ItemType == ItemType.Instrument && activeItem.Id == 1)
            {
                Dig();
            }
            else if (activeItem.ItemType == ItemType.Seed)
            {
                Plant(activeItem);
            }
            else if (activeItem.ItemType == ItemType.Instrument && activeItem.Id == 2)
                Watering();
        }
    }
    private void Dig()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, _groundMask))
        {
            AudioManager.AidioManager.Play("dig");

            GameObject ground;

            ground = hit.collider.gameObject;

            StartCoroutine(DigCoroutine(ground));
        }
    }
    private void Watering()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, _soilMask))
        {
            AudioManager.AidioManager.Play("watering");

            GameObject soil;

            soil = hit.collider.gameObject;

            StartCoroutine(WateringCoroutine(soil));
        }
    }
    private void Plant(Item seedItem)
    {
        GameObject soil;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, _waterSoilMask))
        {
            AudioManager.AidioManager.Play("plant");
            
            soil = hit.collider.gameObject;

            if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit plantD, 1, _plantMask))
                StartCoroutine(PlantingCoroutine(soil, seedItem));   
        }
    }
    private IEnumerator DigCoroutine(GameObject ground)
    {
        GameObject handItem =  Instantiate(Resources.Load<GameObject>("Prefabs/Items/shovel"), _hand.transform);

        _canInteract = false;

        _eventBus.Raise(new InteractEvent("dig"));

        yield return new WaitForSeconds(InteractingType.InteractingValues["dig"][1]);

        Instantiate(Resources.Load<GameObject>("Prefabs/Blocks/soil"), ground.transform.position, Quaternion.identity);
        Destroy(ground);
        Destroy(handItem);

        _canInteract = true;
    }
    private IEnumerator WateringCoroutine(GameObject soil)
    {
        GameObject handItem = Instantiate(Resources.Load<GameObject>("Prefabs/Items/wateringCan"), _hand.transform);

        _canInteract = false;

        _eventBus.Raise(new InteractEvent("watering"));

        yield return new WaitForSeconds(InteractingType.InteractingValues["watering"][1]);

        Instantiate(Resources.Load<GameObject>("Prefabs/Blocks/waterSoil"), soil.transform.position, Quaternion.identity);

        Destroy(soil);
        Destroy(handItem);
        _canInteract = true;
    }
    private IEnumerator PlantingCoroutine(GameObject soil, Item seedItem)
    {
        _canInteract = false;

        _eventBus.Raise(new InteractEvent("plant"));

        GameObject plantStorage = Resources.Load<GameObject>("Prefabs/Plants/PlantStorage");

        GrowPlant plant = plantStorage.GetComponent<GrowPlant>();

        plant.Plant = AllPlants.Plants.First(p => p.SeedId == seedItem.Id);

        yield return new WaitForSeconds(InteractingType.InteractingValues["plant"][1]);

        _eventBus.Raise(new ChangeItemEvent(seedItem, -1));

        Instantiate(plantStorage, soil.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        _canInteract = true;
    }
    private IEnumerator PullPlant(GameObject plant)
    {
        _canInteract = false;

        _eventBus.Raise(new InteractEvent("pull"));

        yield return new WaitForSeconds(InteractingType.InteractingValues["pull"][1]);

        AudioManager.AidioManager.Play("pull");

        Item productItem = AllItems.Items.FirstOrDefault(i => i.ItemType == ItemType.Food && i.Id == plant.GetComponent<GrowPlant>().Plant.ProductId);

        _eventBus.Raise(new ChangeItemEvent(productItem, 1));

        _eventBus.Raise(new ChangeExperienceEvent(plant.GetComponent<GrowPlant>().Plant.Experience));

        Destroy(plant);

        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1, _waterSoilMask);

        GameObject waterSoil = hit.collider.gameObject;

        Instantiate(Resources.Load<GameObject>("Prefabs/Blocks/Grounds/ground"), waterSoil.transform.position, Quaternion.identity);

        Destroy(waterSoil);

        _canInteract = true;
    }
    public void OnEvent(PlayerInZoneEvent @event)
    {
        _canInteract = !@event.InZone;
    }
}