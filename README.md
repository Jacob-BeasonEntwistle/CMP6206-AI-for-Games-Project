# Garden of Agents

Garden of Agents is a demo scene set in a back garden with a multitude of creatures and critters that traverse the ground and patrol the air. 

## Implemented AI
* Finite State Machine (FSM)
    * Pigeon - Passive

## AI that will be implemented
* Behaviour Tree
    * Bee - Neutral (passive until threatened)

* Perception Agent (Agents & Environments)
    * Dog - Neutral/aggressive

* Pathfinding AI
    * Snail - Passive

## Target Game Engine
* Unity

## Methodology & Planning

I'm using Trello to track my progress throughout the project, creating a list of tasks that help with prioritising the development of key features and AI as well as keep track of what has been completed.

Trello board: [Garden of Agents](https://trello.com/b/SeBHeLrO/garden-of-agents)

# Research

## Bee Behaviour Research
### Hivernation
* On [Itch.io](https://itch.io/) I found a game called [Hivernation](https://skuuully.itch.io/hivernation) where a user named [Skuuully](https://skuuully.itch.io/) created a game about bees
* The bees are controlled via a finite state machine with 3 states: Idle, Move, and Build
* The Move state uses [Aron Granberg's A* pathfinding project](https://arongranberg.com/astar/) to move to its target
* It uses [DOTween's DOPath](https://dotween.demigiant.com/documentation.php) to fly to different checkpoints (flowers) before returning to its Idle state

To expand upon this I will use Unity's Behaviour Graphs to increase the complexity of the AI agent.

### Information on Bees 
Source: [Buzzed About Bees](https://www.buzzedaboutbees.com/identifying-bees-behavior-of-bees)

**Memory** - "Bees have an incredible memory and can remember where they’ve been and which flowers have the most nectar. They also remember the scent of flowers, so they can find them even if they’ve moved."

* I could make it so that each flower has a 'nectar/pollen count' and a bee can memorize and keep track of this value
* Then the location of each flower they visit can be stored and revisited, then discarded once it runs out of nectar/pollen

**Communication** - "Bees communicate with one another using a type of dance called the “waggle dance”. 
When a bee finds a source of food, it will fly back to the hive and perform a figure-eight shaped dance. 
This tells the other bees which direction to fly in order to find the food."

* I could make it so that if a bee discovers a flower with high nectar/pollen count, it can return to the hive and share the location with other bees - increasing the chance that more foragers will visit that flower.

**Honey Tasting** - "Bees have taste receptors on their feet! This helps them determine whether a flower has enough nectar to make it worth visiting."

* I could make it so that once a bee finds a flower, it checks the 'nectar/pollen count' and if it's below a certain threshold, it ignores it and checks back later.

**Nectar VS Pollen** - Bees collect both nectar and pollen. Nectar is converted into honey to serve as a colony's primary long-term source of food. Pollen is collected to feed developing larvae. 
So in short: Nectar is a survival resource whereas Pollen is an expansion resource.

* I could make the bees select a behaviour based on the amount of nectar and pollen the hive contains. If the hive is low on honey, bees will go out as nectar foragers.
If the hive is low on food for the brood (bee younglings), bees will go out as pollen foragers.

Individual bees could operate using local rules (memory, communication, resource evaluation, and collection), but collectively the behaviours could produce an adaptive colony capable of balancing survival and population growth.

# References
1. Lists in C# - https://www.geeksforgeeks.org/c-sharp/c-sharp-adding-an-element-to-the-list/
2. Instantiating Prefabs - https://learn.unity.com/tutorial/instantiate
3. Looking towards a point - https://docs.unity3d.com/6000.6/Documentation/ScriptReference/Quaternion.LookRotation.html
4. Bee Research - https://www.buzzedaboutbees.com/identifying-bees-behavior-of-bees

## TODO: Add the following details and update regularly

1. [PENDING] Description/overview - What AI features will the project have/showcase and in what way will they be realised.  For example, for FSM what type of agents will be created using FSM such as enemies, companion, allies, etc.
2. [PENDING] Research - background to the project such as inspiration and what you need to find out to achieve the AI features.  If real behaviour is to be implemented then examples such as flocking or using Artificial Life (ALife) approaches such as cellular automata should be used to support your project and provide a means of comparison of how successful your implementation is.
4. Progress documentation - regular commits/updates to the project need to be documented with a brief overview and reflection on what has been achieved.  For example, what feature(s) have been added?  Were they achieved in the time allowed?  What problems were there or remain?  What has been learnt and what would you do differently if you had to repeat the project?
5. References to sources used.  This must include graphics resources used such as models and textures, and code resources (websites, book, AI).
