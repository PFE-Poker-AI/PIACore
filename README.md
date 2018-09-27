# Poker AI Core

## Getting started

A simple tool for implementing custom poker AI that connects to PokerAI Online.

## How to implement

Clone this repository on your local machine.

### Step 1 : Add a Folder in the AI folder

All your work and custom classes will be stored within this specific AI Folder.
You can organise yourself however you want within this folder.

### Step 2 : Create your AI main class

To do so implement the `IAIManager` Interface.
Your logic will be implemented within the method `PlayAction`.

Everytime that it's your turn to play you will receive a `Table` model that defines the current state of the game.

You will have to respond by returning a `Play` model to play your hand.

### Step 3 : Add your AI Data to the runDefinition

Add the following json to the `runDefinition.json` at the root of the application.

``` json5
[
  {
    "ClassLocation":"PIACore.AI.AlwaysCallAI.ExempleAIManagerImplementation",
    "Slug":"Name of your AI",
    "TableIdentifier":"[TAG_OF_YOUR_AI]",
    "TimeInMillis":2000,
    "ApiKey":"the secret key"
  }
]
```

| Fields | Description |
| ------ | ----------- |
| ClassLocation | The location of your implementation of the IAiManager. Must be a class. Must be constructable. |
| Slug | The name you wanna display for your AI in the Console. |
| TableIdentifier | The tag you wanna use to find the tables you want to join. |
| TimeInMillis | The time in milliseconds between each consequent checking of the game state. |
| ApiKey | The API Key that will be used to connect to poker AI. |

### Step 4 : Code your AI

Enjoy