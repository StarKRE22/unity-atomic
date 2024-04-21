> [!IMPORTANT]
> For a more better Unity development experience, I recommend using the [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) asset.


Unity Atomic - Atomic Extensions for Unity
===
Created by Igor Gulkin (@StarKRE22)

What is Unity Atomic?
---
The atomic approach is an object-oriented approach for game object development, representing a game object as a composition of atomic data and functions. An atomic object provides a common interface for interacting with the properties and functions of various entities, and also has the ability to change the structure of an object at runtime.

Release Notes, see [unity-atomic/releases](https://github.com/StarKRE22/unity-atomic/releases)

Getting started
---

Create Character using Atomic Approach

```csharp

[Is("Damagable")]
public sealed class Character : AtomicEntity //derived from MonoBehaviour
{
    [Get("Health")]
    [SerializeField]
    private AtomicVariable<int> health;

    [Get("TakeDamage")]
    [SerializeField]
    private AtomicAction<int> takeDamageAction;

    [Get("DeathEvent")]
    [SerializeField]
    private AtomicEvent deathEvent;
    
    private void Awake()
    {
        //Declare damage action
        this.takeDamageAction.Compose(damage =>
        {
            this.health.Value -= damage;
        });

        //Declare death mechanics
        this.health.Subscribe(value =>
        {
            if (value <= 0) this.deathEvent.Invoke();
        });
    }
}
```

Interacting with Character
---

```csharp

IAtomicEntity character = gameObject.GetComponent<IAtomicEntity>();


//Check if damagable object 
bool isDamagable = character.Is("Damagable");

//Get health
int health = character.GetValue<int>("Health").Value;

//Set health
character.SetValue("Health", 5);

//Deal damage
character.CallAction("TakeDamage", 2);

//Subscribe on death
character.ListenEvent("DeathEvent", () => Debug.Log("I'm dead!"));
```

Contracts
---
For better work with ids and undestanding types, I recommend using the Contracts

```csharp
//File for keeping object types
public static class ObjectType
{
    public const string Damagable = nameof(Damagable);
    public const string Moveable = nameof(Moveable);
    public const string Jumpable = nameof(Jumpable);
}

//File for keeping object properties
public static class ObjectAPI
{
    //Specify expecting type
    [Contract(typeof(IAtomicVariableObservable<int>))]
    public const string Health = nameof(Health);
        
    [Contract(typeof(IAtomicAction<int>))]
    public const string TakeDamageAction = nameof(TakeDamageAction);

    [Contract(typeof(IAtomicObservable))]
    public const string DeathEvent = nameof(DeathEvent);
}
```

```csharp

//Interact with object properties by contract ids
bool isDamagable = character.Is(ObjectType.Damagable);

int health = character.GetValue<int>(ObjectAPI.Health).Value;

character.CallAction(ObjectAPI.TakeDamageAction, 2);

character.ListenEvent(ObjectAPI.DeathEvent,()=> Debug.Log("I'm dead!"));
```

Reusing of object structure
---
If you need reuse game mechanics between different objects then you can create component for target structure

```csharp
//Create common health component
[Serializable]
public sealed class HealthComponent
{
    public IAtomicVariableObservable<int> Health => this.health;
    public IAtomicAction<int> TakeDamageAction => this.takeDamageAction;
    public IAtomicObservable DeathEvent => this.deathEvent;

    [SerializeField]
    private AtomicVariable<int> health;

    [SerializeField]
    private AtomicAction<int> takeDamageAction;

    [SerializeField]
    private AtomicEvent deathEvent;

    public void Compose()
    {
        //Declare damage action
        this.takeDamageAction.Compose(damage => { this.health.Value -= damage; });

        //Declare death mechanics
        this.health.Subscribe(value =>
        {
            if (value <= 0) this.deathEvent.Invoke();
        });
    }
}
```

Reuse health logic between Character and Tower

```csharp

[Is(ObjectType.Damagable)]
public sealed class Character : AtomicEntity
{
    #region INTERFACE

    ///Health
    [Get(ObjectAPI.Health)]
    public IAtomicVariableObservable<int> Health => this.healthComponent.Health;

    [Get(ObjectAPI.TakeDamageAction)]
    public IAtomicAction<int> TakeDamageAction => this.healthComponent.TakeDamageAction;

    [Get(ObjectAPI.DeathEvent)]
    public IAtomicObservable DeathEvent => this.healthComponent.DeathEvent;

    ///Movement
    [Get(ObjectAPI.MoveAction)]
    public IAtomicAction<Vector3> MoveAction => this.moveAction;

    #endregion

    #region CORE

    [SerializeField]
    private HealthComponent healthComponent;

    [SerializeField]
    private AtomicAction<Vector3> moveAction;

    private void Awake()
    {
        this.healthComponent.Compose();
        this.moveAction.Compose(offset => this.transform.Translate(offset));
    }

    #endregion
}

[Is(ObjectType.Damagable)]
public sealed class Tower : AtomicEntity
{
    #region INTERFACE

    [Get(ObjectAPI.TakeDamageAction)]
    public IAtomicAction<int> TakeDamageAction => this.healthComponent.TakeDamageAction;

    #endregion

    #region CORE

    [SerializeField]
    private HealthComponent healthComponent;

    private void Awake()
    {
        this.healthComponent.Compose();
    }

    #endregion
}

```

