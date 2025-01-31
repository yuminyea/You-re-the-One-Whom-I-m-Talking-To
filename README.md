# **You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios**

This repository supports the **IMWUT 2025 February submission (ID: 1102)** by providing documentation, pre-built executables, and visual materials related to the study. The repository is **anonymized** to comply with double-blind peer review requirements.  
Upon acceptance of the manuscript, Unity packages, source code, and additional resources will be shared.



## **Overview**
This study explores how **contextual external human-machine interfaces (eHMIs)** in autonomous vehicles (AVs) facilitate communication with multiple road users—**pedestrians, cyclists, and manual vehicle drivers**—in complex traffic scenarios.  
Using a **VR-based multi-agent simulation**, we demonstrate how eHMIs can reduce conflicts and improve safety.



## **Features**
- **VR-Based Multi-Agent Simulation**: Real-time simulation of interactions between AVs and road users.
- **Pre-Built Executables**: Allows reviewers to replicate the experimental setup without requiring source code.
- **Server-Side Experiment Control**: Control AV behaviors (e.g., start/stop driving) and experimental conditions (e.g., eHMI designs) **directly from the Unity interface**.
- **Real-Time Road User Data Synchronization**:  
  - Pedestrian: **Joint positions & rotations**  
  - Cyclist: **Position, rotation, and wheel/pedal data**  
  - Driver: **Position, rotation, and wheel data**  



## **Repository Structure**
- **Builds/** – Pre-built executables for server and clients
  - **server/** – Server executable
  - **pedestrian/** – Pedestrian client executable
  - **cyclist/** – Cyclist client executable
  - **driver/** – Driver client executable
- **Docs/** – Documentation and system overview
  - **SetupGuide.pdf** – Step-by-step setup instructions
  - **SystemDiagram.pdf** – Overview of system architecture



## **Quickstart**

### **1. Running the Server in Unity**
1. Open **Unity** and load the project.
2. Run the **"Server" scene** inside Unity.
3. The server will start, handling real-time synchronization and experimental conditions.

### **2. Running the Client Executables**
1. Download the appropriate client executable from `Builds/`:
   - **Pedestrian:** `pedestrian.exe`
   - **Cyclist:** `cyclist.exe`
   - **Driver:** `driver.exe`
2. Run the executable and **connect to the server**.

> **Default server IP**: `127.0.0.1` (for local testing)  
> **Custom server IP**: Specify with `-address` flag  
> ```bash
> ./client.exe -address <server_ip>
> ```



## **Server Control (Unity-Based)**
The server is controlled directly within Unity.  
Use **keyboard inputs inside the Unity Editor** to modify AV behavior and experimental conditions.

### **Vehicle Control**
- **S** → Start driving  
- **X** → Stop driving  

### **eHMI Condition Selection**
Press **the number keys (1–10 on the top row) and - or =** to change eHMI conditions:

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
| 0 | When (No Yield)         |
| - | Where (Yield)           |
| = | Where (No Yield)        |



## **Object Movement (VR & Keyboard)**
### **1. VR-Based Movement**
- Users wearing VR HMDs can control their characters naturally using **head movement & controllers**.
- Real-time tracking of **position & orientation** is synchronized across the server and other clients.

### **2. Keyboard-Based Movement**
Users can also use keyboard inputs for testing purposes:

| Action      | Key (PC)     |
|------------|-------------|
| Move Forward  | `W` / `UpArrow` |
| Move Backward | `S` / `DownArrow` |
| Turn Left  | `A` / `LeftArrow` |
| Turn Right | `D` / `RightArrow` |



## **Documentation**
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step installation & setup instructions  
- **[System Diagram](Docs/SystemDiagram.pdf)**: Overview of system architecture  



## **Supported Platforms**
- **Windows (Primary Support)**
- MacOS & Linux builds available upon request



## **Notes**
- The **server runs exclusively inside Unity** and is controlled via **keyboard inputs within the Unity Editor**.
- **Real-time synchronization:**  
  - Pedestrians: **Joint positions & rotations**  
  - Cyclists: **Wheel & pedal data**  
  - Drivers: **Steering wheel data**  
- Upon manuscript acceptance, **source code and additional resources will be shared**.



## **Demo Video**
Watch the system in action:  
[![Demo Video](link will be added)]


## **License**
This repository is licensed under the **MIT License**. See `LICENSE` for details.


