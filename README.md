![Official](https://img.shields.io/badge/official-%20)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/unity-atomic?color=orange)](https://github.com/starkre22/unity-atomic/releases)
[![GitHub](https://img.shields.io/github/license/starkre22/unity-atomic?color=red)](https://github.com/StarKRE22/unity-atomic/blob/master/LICENSE)

> [!IMPORTANT]
> For a more better Unity development experience, I recommend using the [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) asset.

Unity Atomic - Atomic Extensions for Unity
===
Created by Igor Gulkin (@StarKRE22)

What is Unity Atomic?
---
The atomic approach is an object-oriented approach for game object development in Unity Engine, representing a game object as a composition of atomic data and functions. The atomic object provides a common interface for interacting with it's properties and functions of various entities, and also has the ability to change the structure of an object at runtime.

Release Notes, see [unity-atomic/releases](https://github.com/StarKRE22/unity-atomic/releases)

## Table of Contents
- [Installation](#installation)
- [Quick Start](#quick-start)
    - [Create a Character](#create-a-character)
    - [Interact with Character](#interact-with-character)
- [Data Structures](#work-with-elements)
    - [AtomicValue]
    - [AtomicVariable] 
- [Work with Objects](#work-with-objects)
    - [Atomic Entity](#atomic-entity)
       - [Add properties at runtime](#add-properties-at-runtime)

    - [Atomic Object]()
      - [Add mechanics at runtime](#add-mechanics-at-runtime)

- [Contracts](#contracts)


- [Good Practices](#good-practices)
    - [Reusing of object structure](#reusing-of-object-structure)
    - [Division into sections](#division-into-sections)
    - [Reflection Baking](#reflection-baking)

Installation
---

**There are 3 ways of installation:**

_1. Download code from repository_

_2. Download last [Atomic.unitypackage](https://github.com/StarKRE22/unity-atomic/releases/download/ver-2.0/Atomic.unitypackage) from [release notes](https://github.com/StarKRE22/unity-atomic/releases)_ 

_3. Install via Unity Package Manager_

```
"com.starkre.unity-atomic": "https://github.com/StarKRE22/unity-atomic.git" 
```

Quick Start
---

**Create a Character**

For example, let's create Character class with health mechanics

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

Interact with Character
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


Data Structures
---
There are several different atomic structures in the library that may be required to develop game objects







Contracts
---
To make easier development for the team with identifiers and types, it is better to put them into separate constants and add the [Contract] attribute, which will explicitly indicate which type is expected for a specific key.

```csharp
//File for keeping object ids
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

//File for keeping object types
public static class ObjectType
{
    public const string Damagable = nameof(Damagable);
    public const string Moveable = nameof(Moveable);
    public const string Jumpable = nameof(Jumpable);
}
```

```csharp

//Interact with object properties by contract ids
bool isDamagable = character.Is(ObjectType.Damagable);

int health = character.GetValue<int>(ObjectAPI.Health).Value;

character.CallAction(ObjectAPI.TakeDamageAction, 2);

character.ListenEvent(ObjectAPI.DeathEvent,()=> Debug.Log("I'm dead!"));
```

Good Practices
===
In this section, I would like to share good practices on how to develop game objects using the atomic approach as sustainably as possible.
One of the key thoughts is that you can always put any block of code that you repeat into a separate class and assign responsibility to it.

Reusing of object structure
---
**If you need reuse game mechanics between different objects then you can create common component**

For example you need to reuse heath mechanics between Character and Tower.
To do this, you can make HealthComponent that will contain health data, damage action, and death event:

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

**Reuse health component between Tower and Character**

```csharp
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
```
Division into sections
---
//TODO

Reflection Baking
---
//TODO









Add properties at runtime
---

```csharp
IMutableAtomicEntity character = gameObject.GetComponent<IMutableAtomicEntity>();

//Make character invisible
character.AddType("Invisible");

//Add resource bag to the character
character.AddData("ResourceBag", new AtomicVariable<int>());

//Remove jump ability
character.RemoveData("JumpAction");
```

Add mechanics at runtime
---
For example you wanna add movement mechanics towards direction for your character.

First of all, extend Character from AtomicObject. 

```csharp
public sealed class Character : AtomicObject //derived from MonoBehaviour
{
    [Get("Transform")]
    public Transform mainTrainsform;

   //Define when Fixed Update required for AtomicObject
   private void FixedUpdate()
   {
       base.OnFixedUpdate(Time.fixedDeltaTime);
   }
}
```

Create MovementMechanics class and implement IAtomicFixedUpdate interface which support FixedUpdate of AtomicObject

```csharp
public sealed class MovementMechanics : IAtomicFixedUpdate
{
    private readonly IAtomicEntity entity;

    public MovementMechanics(IAtomicEntity entity)
    {
        this.entity = entity;
    }
    
    public void OnFixedUpdate(float deltaTime)
    {
        if (!entity.TryGet("Transform", out Transform transform) ||
            !entity.TryGet("MoveSpeed", out IAtomicValue<float> moveSpeed) ||
            !entity.TryGet("MoveDirection", out IAtomicValue<Vector3> moveDirection))
        {
            return;
        }

        Vector3 offset = moveDirection.Value * (moveSpeed.Value * deltaTime);
        transform.Translate(offset);
    }
}
```

Add movement mechanics to your Character

```csharp
IMutableAtomicObject character = gameObject.GetComponent<IMutableAtomicObject>();

//Add movement mechanics at runtime
character.AddData("MoveSpeed", new AtomicVariable<float>(3));
character.AddData("MoveDirection", new AtomicVariable<Vector3>(Vector3.forward));
character.AddLogic(new MovementMechanics(character));

//Remove movement mechanics at runtime
character.RemoveLogic<MovementMechanics>();
```

