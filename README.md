# # Developer Test - Arclite
# # Unity Editor Version 6000.0.54f1

## ⚙️ Overview

This project implements a **runtime-driven player movement system** that adapts to dynamic targets.
It separates responsibilities into distinct modules, making it **easy to extend, debug, and maintain**.

The design encourages **performance efficiency**, **code reusability**, and **low coupling**, ensuring the system remains lightweight even on **mobile platforms**.

---

## 🧩 Core Design Principles

### ✅ **Modular Architecture**

Each system (movement, feeding, saving, etc.) operates independently.

### ⚡ **Optimized for Runtime**

Targets are assigned dynamically using a lightweight feeder — no heavy list traversal or redundant transforms.
Only essential calculations (like direction and rotation) run per frame.

### 🔄 **Event-Driven Flow**

Player movement progression and state changes are event-based, allowing flexible game logic integration (e.g., triggering effects, audio, or animations when a target is reached).

### 🧠 **Decoupled Game State Integration**

The system hooks into an external `StateManager`, ensuring clear separation between gameplay logic and system behavior.

---

## 🚀 Features

* Dynamic runtime target assignment
* Smooth rotation & frame-independent movement
* Optimized for **mobile performance**
* Extensible **TargetFeeder** for procedural gameplay
* Event callbacks for precise gameplay triggers
* Modular design

---

## 🧱 Extending the System

* A **Save System** to track previously visited preferences
* A dynamic **UI Tab System**
* **Custom Feeder Logic** (e.g., AI pathfinding, randomized targets, network-synced objectives)

---

## 🔧 Tech Stack

* **Unity 6000.0.54f1**
* **C#**
* Compatible with **Mobile, PC, and WebGL**

---


---

## Note: This readme was generated with the help of an LLM. 🤖

