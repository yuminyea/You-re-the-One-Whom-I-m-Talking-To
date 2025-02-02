# **You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios**

This repository supports the **IMWUT 2025 February submission (ID: 1102)** by providing detailed technical documentation, experimental protocols, and source code pertinent to the study. The repository is **anonymized** to comply with double-blind peer review requirements.  
Upon acceptance of the manuscript, Unity packages and additional resources will be shared, including full project files and build executables.



## **Overview**
This study explores how **contextual external human-machine interfaces (eHMIs)** in autonomous vehicles (AVs) facilitate communication with multiple road users—**pedestrians, cyclists, and manual vehicle drivers**—in complex traffic scenarios.  
Using a **VR-based multi-agent simulation**, we demonstrate how eHMIs can reduce conflicts and improve safety.



## **Features**
- **VR-Based Multi-Agent Simulation**: Real-time simulation of interactions between AVs and road users.
- **Scripts**: The repository provides the core scripts and implementation details necessary for reproducing and extending the study's findings.
- **Real-Time Road User Data Synchronization**:  
  - Pedestrian: **Joint positions & rotations**  
  - Cyclist: **Position, rotation, and wheel/pedal data**  
  - Driver: **Position, rotation, and wheel data**  



## **Repository Structure**
- **Scripts/** – Unity C# scripts for server and client functionality, including handling real-time synchronization of road user movement data 
- **Docs/** – Documentation and system overview
  - **SetupGuide.pdf** – Step-by-step setup instructions
  - **SystemDiagram.pdf** – Overview of system architecture



## **Quickstart**

### **1. Setting Up the Server**

1. Clone the repository and open the Unity project in **Unity Editor**.
2. Configure the **server functionality** using the provided scripts in the `Scripts/` folder.
3. Implement the **server-side logic** by creating a server scene using the `NetworkManager` and related scripts.

### **2. Setting Up the Clients**

1. Configure the **client functionality** using the scripts provided in the `Scripts/` folder.
2. Assign appropriate roles (Pedestrian, Cyclist, Driver) and create client scenes for each.
3. Implement the **connection logic** to synchronize with the server.

> **Note:** The repository provides **core code only**. Unity scenes, pre-built executables, or configuration assets are not included. You can create these elements using the provided scripts and detailed documentation.



## **Server Setup and Control**

The server functionality is implemented using the **Mirror networking framework** and is designed to run within Unity. While the repository does not include pre-built executables or Unity scenes, the provided scripts allow users to configure a functional server-client architecture.

### **Configuring the Server**

1. **Open Unity Editor** and create a new scene for the server.
2. Add a **NetworkManager** object to the scene.
3. Ensure the **Mirror** networking package is installed in your Unity project. You can install it via Unity’s Package Manager or from the [Mirror GitHub repository](https://github.com/MirrorNetworking/Mirror).
4. Assign the provided server scripts (found in the `Scripts/` folder) to handle synchronization and experimental conditions.
5. Configure the **NetworkManager**:
   - Set up the **port** and **max connections** as needed.
   - Ensure that **Spawnable Prefabs** are configured if necessary.

### **Server Controls (Keyboard-Based)**

The server can be controlled using keyboard inputs within Unity during runtime.

#### **Vehicle Control**
- **S** → Start driving  
- **X** → Stop driving  

#### **eHMI Condition Selection**
Use **number keys (1–10 on the top row)**, along with **-** or **=**, to set specific eHMI conditions:

| Key | Condition               |
|----|-------------------------|
| 1  | No eHMI (Yield)         |
| 2  | No eHMI (No Yield)      |
| 3  | No Context (Yield)      |
| 4  | No Context (No Yield)   |
| 5  | Whom (Pedestrian)       |
| 6  | Whom (Driver)           |
| 7  | Whom (Cyclist)          |
| 8  | Whom (No Yield)         |
| 9  | When (Yield)            |
| 0  | When (No Yield)         |
| -  | Where (Yield)           |
| =  | Where (No Yield)        |

> **Note:** The repository only includes server and client functionality scripts. Users must set up Unity scenes and objects manually using the provided documentation. The **Mirror networking framework** handles message serialization and real-time synchronization across clients.


## **Object Movement (VR & Keyboard)**

The repository provides scripts for implementing movement mechanics for pedestrians, cyclists, and drivers. While VR-based and keyboard-based movement functionalities are supported in the code, users must manually integrate these scripts into their Unity project to fully configure the movement system.

### **1. VR-Based Movement**
- Users wearing VR HMDs can control their characters naturally using **head movement & controllers**.
- Real-time tracking of **position & orientation** is synchronized across the server and other clients using the **Mirror networking framework**.

**To Enable VR Movement:**
1. Import a supported VR framework into your Unity project (e.g., Unity XR Interaction Toolkit or OpenXR).
2. Attach the relevant scripts (`PedestrianController.cs`, `BicycleController.cs`, or `DriverController.cs`) to the corresponding objects.
3. Configure your VR hardware in the Unity project settings.

### **2. Keyboard-Based Movement**
Users can also use keyboard inputs for testing purposes. This is especially useful for non-VR setups or quick debugging:

| Action      | Key (PC)     |
|------------|-------------|
| Move Forward  | `W` / `UpArrow` |
| Move Backward | `S` / `DownArrow` |
| Turn Left  | `A` / `LeftArrow` |
| Turn Right | `D` / `RightArrow` |

**To Enable Keyboard Movement:**
1. Open Unity and create a new scene for your desired role (pedestrian, cyclist, or driver).
2. Add the respective script (`PedestrianController.cs`, `BicycleController.cs`, or `DriverController.cs`) to the main object representing the character.
3. Assign the necessary transforms (e.g., joints, wheels) in the Unity Inspector.


> **Note:**
This repository does not include pre-built Unity scenes or assets. Users must set up the project environment manually:
- Configure objects for **VR or keyboard input** using the provided scripts.
- Ensure that the server and clients are properly synchronized using the **Mirror networking framework**.



## **Documentation**
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step installation & setup instructions  
- **[System Diagram](Docs/SystemDiagram.pdf)**: Overview of system architecture  



## **Notes**
- This repository currently provides core scripts and documentation, with plans for significant expansion in future updates. These updates will include the **complete Unity project files**, **build executables**, and an **API** to enhance usability and accessibility for researchers and developers.
- During testing, we identified considerations related to **Unity asset licensing** and **network connectivity** in certain machine setups. We are actively addressing these aspects to ensure a seamless and robust experience.
- Upon manuscript acceptance, the repository will include the **full source code and additional resources**, enabling a fully reproducible and comprehensive research workflow.


## **Demo Video**
Watch the system in action:  
[![Demo Video](link will be added)]


## **License**
This repository is licensed under the **MIT License**. See `LICENSE` for details.


