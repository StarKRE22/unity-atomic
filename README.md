![Official](https://img.shields.io/badge/official-%20)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/starkre22/unity-atomic?color=orange)](https://github.com/starkre22/unity-atomic/releases)

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
- [Documentation](#documentation)
    - [Structures](#structures)
        - [AtomicValue](#atomic-value)
        - [AtomicVariable](#atomic-variable)
        - [AtomicAction](#atomic-action)
        - [AtomicEvent](#atomic-event)
        - [AtomicFunction](#atomic-function)
        - [AtomicProperty](#atomic-property)
        - [AtomicObservable](#atomic-observable)
        - [AtomicPropertyObservable](#atomic-property-observable)
        - [AtomicSetter](#atomic-setter)
        - [AtomicExpressions](#atomic-expressions)
    - [Objects](#objects)
        - [Atomic Entity](#atomic-entity)
        - [Atomic Object](#atomic-object)
    - [Contracts](#contracts)
    - [Extensions](#extensions)
        - [Structures](extensions-for-structures)
        - [Objects](extensions-for-objects)
- [Good Practices](#good-practices)
    - [Reusing of object structure](#reusing-of-object-structure)
    - [Division into sections](#division-into-sections)
    - [Reflection Baking](#reflection-baking)

## Installation

**There are 3 ways of installation:**

_1. Download code from repository_

_2. Download last [Atomic.unitypackage](https://github.com/StarKRE22/unity-atomic/releases/download/ver-2.0/Atomic.unitypackage) from [release notes](https://github.com/StarKRE22/unity-atomic/releases)_ 

_3. Install via Unity Package Manager_

```
"com.starkre.unity-atomic": "https://github.com/StarKRE22/unity-atomic.git" 
```

## Quick Start
In this section you will see the basic functionality of how you can create a character class and interact with it.

_Let's create a Character class with health mechanics:_
```csharp
[Is("Damagable")]
public sealed class Character : AtomicEntity
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

_Then you can see examples below of how you can interact with character properties:_
```csharp

IAtomicEntity character = gameObject.GetComponent<IAtomicEntity>();

//Check if damagable object 
bool isDamagable = character.Is("Damagable");

//Get health
int health = character.GetValue<int>("Health").Value;

//Set health
character.SetVariable("Health", 5);

//Deal damage
character.InvokeAction("TakeDamage", 2);

//Subscribe on death
character.SubscribeOnEvent("DeathEvent", () => Debug.Log("I'm dead!"));
```

These were simple examples demonstrating the basic capabilities of the atomic approach. 
For more detail information look in the documentation section.

Documentation
===
In this section you will see full capabilities of the atomic approach with theory.

## Structures

There are several different atomic structures in the library that may be required to develop game objects
- [AtomicValue](#atomic-value)
- [AtomicVariable](#atomic-variable)
- [AtomicAction](#atomic-action)
- [AtomicEvent](#atomic-event)
- [AtomicFunction](#atomic-function)
- [AtomicProperty](#atomic-property)
- [AtomicObservable](#atomic-observable)
- [AtomicPropertyObservable](#atomic-property-observable)
- [AtomicSetter](#atomic-setter)
- [AtomicExpressions](#atomic-expressions)

### Atomic Value
Represents a read-only property (See class: [AtomicValue](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicValue.cs))

```csharp
var damage = new AtomicValue<float>(5.0f);
float damageValue = damage.Value; //5.0f
```

Example of using as speed property

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicValue<float> Speed => this.speed;

    [SerializeField]    
    private AtomicValue<float> speed = new(3.0f);
}
```

### Atomic Variable
Represents a read-write reactive property (See class: [AtomicVariable](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicVariable.cs))

```csharp
var health = new AtomicVariable<int>(5);
int healthValue = health.Value;

health.Value = 3;
health.Subscribe(value => Debug.Log($"On health changed {value}"));
health.Dispose(); //Unsubscribe all listeners
```

Example of using as health property

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicVariableObservable<int> Health => this.health;

    [SerializeField]    
    private AtomicVariable<int> health = new(5);
}
```

### Atomic Action
Represents a method object (See class: [AtomicAction](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicAction.cs))

```csharp
var takeDamageAction = new AtomicAction<int>(damage => Debug.Log($"Take Damage {damage}"))
takeDamageAction.Invoke(5);
```

Example of using action

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicAction<int> TakeDamageAction => this.takeDamageAction;
    
    [SerializeField]    
    private AtomicAction<int> takeDamageAction;
    
    [SerializeField]    
    private AtomicVariable<int> health = new(100);
    
    private void Awake()
    {
        //Init take damage action
        this.takeDamageAction.Compose(damage => this.health.Value -= damage);
    }
}
```

### Atomic Event
Represents an event object (See class: [AtomicEvent](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicEvent.cs))

```csharp
var deathEvent = new AtomicEvent();
deathEvent.Subscribe(() => Debug.Log("I'm dead!"))
deathEvent.Invoke();
deathEvent.Dispose(); //Unsubscribe all listeners
```

Example of using event

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicObservable<int> DamageTakenEvent => this.damageTakenEvent;

    [SerializeField]    
    private AtomicAction<int> takeDamageAction;
    
    [SerializeField]    
    private AtomicVariable<int> health = new(100);

    [SerializeField]
    private AtomicEvent<int> damageTakenEvent;
    
    private void Awake()
    {
        //Init take damage action
        this.takeDamageAction.Compose(damage => {
            this.health.Value -= damage;
            this.damageTakenEvent.Invoke(damage);
        });
    }
}
```

### Atomic Function
Represents a function object (See class: [AtomicFunction](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicFunction.cs))

```csharp
var positionFunc = new AtomicFunction<Vector3>(() => gameObject.transform.position);
Vector3 position = positionFunc.Invoke();
```

Example of using function

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicValue<Vector3> Position => this.positionFunc;
    
    [SerializeField]
    private AtomicFunction positionFunc;
    
    private void Awake()
    {
        //Init function
        this.positionFunc.Compose(() => this.transform.position);
    }
}
```

### Atomic Property

Provides read-write interface to a specified source (See class: [AtomicProperty](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicProperty.cs))

```csharp
var positionProperty = new AtomicProperty<Vector3>(
    () => transform.position,
    value => transform.position = value
);

Vector3 position = positionProperty.Value;
positionProperty.Value = new Vector3(10, 10, 10);
```

Example of using property

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicVariable<Vector3> Position => this.positionProperty;
    
    [SerializeField]
    private AtomicFunction positionProperty;
    
    private void Awake()
    {
        //Init property
        this.positionFunc.Compose(
            () => this.transform.position,
            value => this.transform.position = value
        );
    }
}
```

### Atomic Observable

Provides observable interface to a specified source (See
class: [AtomicObservable](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicObservable.cs))

Example of using observable
```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicObservable<IWeapon> WeaponChangedEvent => this.weaponChangedEvent;

    [SerializeField]
    private AtomicObservable<IWeapon> weaponChangedEvent;

    [Inject]
    private IWeaponManager weaponManager;

    private void Awake()
    {
        this.weaponChangedEvent.Compose(
            weapon => this.weaponManager.OnChanged += weapon,
            weapon => this.weaponManager.OnChanged -= weapon
        );
    }
}

public interface IWeaponManager
{
    event Action<IWeapon> OnChanged;
}
```

### Atomic Property Observable

Combines Atomic Property & Atomic Observable (See
class: [AtomicPropertyObservable](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicPropertyObservable.cs))

### Atomic Setter
Provides setter interface to a specified source.
(See class: [AtomicSetter](https://github.com/StarKRE22/unity-atomic/blob/master/Elements/Implementations/AtomicSetter.cs))

### Atomic Expressions

Atomic expressions are one of the most powerful extensions of the atomic approach. Provide the ability to make composites of data in the form of sum, product, logical and/or.

For example, let's assume that we want to make the character move forward at a speed that can be changed by different multipliers. Let there be a trigger effector as an object that will change the character's speed

To do this, create an atomic expression of speed in the character class

```csharp
public sealed class Character : MonoBehaviour
{
    public IAtomicExpression<float> MoveSpeed => this.moveSpeedExpression;

    [SerializeField]
    private AtomicFloatProduct moveSpeedExpression;

    [SerializeField]
    private AtomicValue<float> moveSpeedBase = new(3);
    
    private void Awake()
    {
        //Add base speed to move expression:
        this.moveSpeedExpression.Append(this.moveSpeedBase);
    }

    private void FixedUpdate()
    {
        //Move character forward:
        Vector3 moveStep = this.moveSpeedExpression.Invoke() * Vector3.forward * Time.fixedDeltaTime;
        this.transform.position += moveStep;
    }
}
```

```csharp
//Increases character speed for 2 times: 
public sealed class SpeedAreaEffector : MonoBehaviour
{
    [SerializeField]
    private AtomicValue<float> speedMultiplier = new(2);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.MoveSpeed.Append(this.speedMultiplier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.MoveSpeed.Remove(this.speedMultiplier);
        }
    }
}
```

Thus, every time the character enters the trigger, his speed will double.

There are other expressions that you can check out in the library:
- [AtomicAnd](https://github.com/StarKRE22/unity-atomic/blob/master/Extensions/Expressions/AtomicAnd.cs)
- [AtomicOr](https://github.com/StarKRE22/unity-atomic/blob/master/Extensions/Expressions/AtomicOr.cs)
- [AtomicIntSum](https://github.com/StarKRE22/unity-atomic/blob/master/Extensions/Expressions/AtomicIntSum.cs)
- [AtomicIntProduct](https://github.com/StarKRE22/unity-atomic/blob/master/Extensions/Expressions/AtomicIntProduct.cs)
- [AtomicFloatSum](https://github.com/StarKRE22/unity-atomic/blob/master/Extensions/Expressions/AtomicFloatSum.cs)
- [AtomicFloatProduct](https://github.com/StarKRE22/unity-atomic/blob/master/Extensions/Expressions/AtomicFloatProduct.cs)


Objects
----
Let's now see how we can work with our game objects using the atomic approach. 
There are two objects in the library that represent the game object: [AtomicEntity](atomic-entity) & [AtomicObject](atomic-object)

### Atomic Entity
//TODO

### Atomic Object
//TODO


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

character.InvokeAction(ObjectAPI.TakeDamageAction, 2);

character.SubscribeOnEvent(ObjectAPI.DeathEvent,()=> Debug.Log("I'm dead!"));
```

Extensions
---
### Extensions for structures
### Extensions for objects

//TODO:

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

