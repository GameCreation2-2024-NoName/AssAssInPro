# MonoBehaviour event handlers # {#monobehaviour_events}

[TOC]
# Trigger # {#Trigger}
The block will execute when a 3d physics trigger matching some basic conditions is met.

Defined in Fungus.Trigger

Property | Type | Description
 --- | --- | ---
Collider Var | Fungus.ColliderVariable | Optional variable to store the collider that caused the trigger to occur.
Fire On | Fungus.BasePhysicsEventHandler+PhysicsMessageType | Which of the physics messages do we trigger on.
Tag Filter | System.String[] | Only fire the event if one of the tags match. Empty means any will fire.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# CharacterCollider # {#CharacterCollider}
The block will execute when tag filtered OnCharacterColliderHit is received

Defined in Fungus.CharacterControllerCollide

Property | Type | Description
 --- | --- | ---
Col Hit Var | Fungus.ControllerColliderHitVariable | Optional variable to store the ControllerColliderHit
Tag Filter | System.String[] | Only fire the event if one of the tags match. Empty means any will fire.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Particle # {#Particle}
The block will execute when the desired OnParticle message for the monobehaviour is received.

Defined in Fungus.Particle

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.Particle+ParticleMessageFlags | Which of the Rendering messages to trigger on.
G Ocollider Var | Fungus.GameObjectVariable | Optional variable to store the gameobject that particle collided with.
Tag Filter | System.String[] | Only fire the event if one of the tags match. Empty means any will fire.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Render # {#Render}
The block will execute when the desired Rendering related message for the monobehaviour is received.

Defined in Fungus.Render

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.Render+RenderMessageFlags | Which of the Rendering messages to trigger on.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Update # {#Update}
The block will execute every chosen Update, or FixedUpdate or LateUpdate.

Defined in Fungus.UpdateTick

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.UpdateTick+UpdateMessageFlags | Which of the Update messages to trigger on.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Animator # {#Animator}
The block will execute when the desired OnAnimator* message for the monobehaviour is received.

Defined in Fungus.AnimatorState

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.AnimatorState+AnimatorMessageFlags | Which of the OnAnimator messages to trigger on.
I K Layer | System.Int32 | IK layer to trigger on. Negative is all.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Mouse # {#Mouse}
The block will execute when the desired OnMouse* message for the monobehaviour is received

Defined in Fungus.Mouse

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.Mouse+MouseMessageFlags | Which of the Mouse messages to trigger on.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Trigger2D # {#Trigger2D}
The block will execute when a 2d physics trigger matching some basic conditions is met.

Defined in Fungus.Trigger2D

Property | Type | Description
 --- | --- | ---
Collider Var | Fungus.Collider2DVariable | Optional variable to store the collider that caused the trigger to occur.
Fire On | Fungus.BasePhysicsEventHandler+PhysicsMessageType | Which of the physics messages do we trigger on.
Tag Filter | System.String[] | Only fire the event if one of the tags match. Empty means any will fire.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Transform # {#Transform}
The block will execute when the desired OnTransform related message for the monobehaviour is received.

Defined in Fungus.TransformChanged

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.TransformChanged+TransformMessageFlags | Which of the OnTransformChanged messages to trigger on.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Collision # {#Collision}
The block will execute when a 3d physics collision matching some basic conditions is met.

Defined in Fungus.Collision

Property | Type | Description
 --- | --- | ---
Collision Var | Fungus.CollisionVariable | Optional variable to store the collision object that is provided by Unity.
Fire On | Fungus.BasePhysicsEventHandler+PhysicsMessageType | Which of the physics messages do we trigger on.
Tag Filter | System.String[] | Only fire the event if one of the tags match. Empty means any will fire.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Collision2D # {#Collision2D}
The block will execute when a 2d physics collision matching some basic conditions is met.

Defined in Fungus.Collision2D

Property | Type | Description
 --- | --- | ---
Collision Var | Fungus.Collision2DVariable | Optional variable to store the collision object that is provided by Unity.
Fire On | Fungus.BasePhysicsEventHandler+PhysicsMessageType | Which of the physics messages do we trigger on.
Tag Filter | System.String[] | Only fire the event if one of the tags match. Empty means any will fire.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

# Application # {#Application}
The block will execute when the desired OnApplication message for the monobehaviour is received.

Defined in Fungus.ApplicationState

Property | Type | Description
 --- | --- | ---
Fire On | Fungus.ApplicationState+ApplicationMessageFlags | Which of the Application messages to trigger on.
Suppress Block Auto Select | System.Boolean | If true, the flowchart window will not auto select the Block when the Event Handler fires. Affects Editor only.

Auto-Generated by Fungus.ExportReferenceDocs