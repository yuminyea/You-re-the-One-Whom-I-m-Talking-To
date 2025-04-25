# **You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios**

This repository supports the **IMWUT 2025 February submission (ID: 1102)** by providing source code pertinent to the study.



## **Overview**
This study explores how **contextual external human-machine interfaces (eHMIs)** in autonomous vehicles (AVs) facilitate communication with multiple road users—**pedestrians, cyclists, and manual vehicle drivers**—in complex traffic scenarios.  
Using a **VR-based multi-agent simulation**, we demonstrate how eHMIs can reduce conflicts and improve safety in real-time interaction settings.



## **Features**
- **Core Scripts**: Implements real-time server-client synchronization for pedestrian, cyclist, and driver roles.
- **Real-Time Data Exchange**:
    - Pedestrians: **Joint positions and rotations**
    - Cyclists: **Position, rotation, wheel, and pedal data**
    - Drivers: **Position, rotation, and wheel data**



## **Repository Structure**
### **Scripts/**
Contains all **Unity C# scripts** used for server and client functionality.

#### **Client/**
- `AVController.cs` – Manages the behavior of an AV in various eHMI modes.
- `BicycleController.cs` – Manages the behavior and data synchronization for the cyclist client.
- `ClientEHMIManager.cs` – Manages eHMI states for the client.
- `DriverController.cs` – Manages the behavior and data synchronization for the driver client.
- `PedestrianController.cs` – Manages the behavior and data synchronization for the pedestrian client.
- `SharedMessages.cs` – Message structure for sending road user data.
  
#### **Server/**
- `CustomNetworkManager.cs` – Custom NetworkManager for handling server and client-specific logic with KCP transport.
- `ServerController.cs` – Server-side controller to manage autonomous vehicle speed and driving state, and synchronize these states with connected clients.
- `ServerManager.cs` – Manages server-side functionality, including registering handlers and broadcasting messages between clients.
- `SharedMessages.cs` – Message structure for sending road user data.
- `expManager.cs` – Manages experimental conditions on the server, allowing condition changes and broadcasting them to connected clients.


## **Quickstart**

### **1. Setting Up the Server**

1. Clone the repository and open the project in Unity.
2. Create a **server scene**:
   - Add a **NetworkManager** object.
   - Configure the **port**, **maximum connections**, and **Mirror networking framework** settings.
3. Attach the provided **server scripts** (`ServerManager.cs`, etc.) to handle synchronization and eHMI experimental conditions.

### **2. Setting Up the Clients**

1. Create separate Unity scenes for **Pedestrian**, **Cyclist**, and **Driver** roles.
2. Add the respective **client scripts** (`PedestrianController.cs`, `BicycleController.cs`, or `DriverController.cs`) to the main role object.
3. Ensure the **server IP address** is correctly set in the client configuration.

### **3. Running the Project**

1. Start the **server scene** in Unity.
2. Launch the **client scenes** for each role (pedestrian, cyclist, driver).  
3. The **Mirror networking framework** will synchronize all role movements and interactions in real-time.



## **Server Controls**
The server can be controlled within Unity using keyboard inputs during runtime.

### **Vehicle Control**
- **S** → Start driving  
- **X** → Stop driving  

### **eHMI Condition Selection**
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



## **Object Movement**

### **1. VR-Based Movement**
- Users can control their characters using **head movement & controllers** via supported VR hardware.
- Real-time tracking of **position and orientation** is synchronized across the server and other clients.

**To Enable VR Movement:**
1. Import a supported VR framework (e.g., Unity XR Interaction Toolkit).
2. Attach the relevant scripts (`PedestrianController.cs`, `BicycleController.cs`, or `DriverController.cs`) to the character objects.
3. Configure the VR hardware in Unity.

### **2. Keyboard-Based Movement**
Keyboard inputs allow for non-VR interaction and testing:

| Action      | Key (PC)     |
|------------|-------------|
| Move Forward  | `W` / `UpArrow` |
| Move Backward | `S` / `DownArrow` |
| Turn Left  | `A` / `LeftArrow` |
| Turn Right | `D` / `RightArrow` |

**To Enable Keyboard Movement:**
1. Attach the respective controller script to the character object.
2. Assign necessary transforms (e.g., wheels, joints) in the Unity Inspector.

## **ExampleScene**
- ExampleScene.unity – A standalone demo scene that replicates core AV motion without networking. 
- Demonstrates the AV behavior and contextual eHMI logic in a self-contained setting.
- Includes pre-configured:
  - Autonomous Vehicle with eHMI controller
  - Pedestrian, Cyclist, Driver avatars

## **Notes**
- The Cyclist (Bike) model is from **Bicycle by jeremy [CC-BY] via Poly Pizza**.
- This repository currently focuses on **core scripts** and documentation. Future updates will include the **complete Unity project files**, **build executables**, and an **API** to enhance usability and accessibility.
- During testing, we identified considerations related to **Unity asset licensing** and **network connectivity** in certain machine setups. We are actively addressing these aspects to ensure a seamless and robust experience.
- Upon manuscript acceptance, the repository will provide the **full source code and additional resources**, enabling reproducibility and expanded research opportunities.


## **License**
This repository is licensed under the **MIT License**. See `LICENSE` for details.
