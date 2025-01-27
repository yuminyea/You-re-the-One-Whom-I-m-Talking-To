# You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios

This repository supports the IMWUT 2025 February submission (ID: 1102) by providing documentation, pre-built executables, and visual materials related to the study. The repository is anonymized to comply with double-blind peer review requirements. Upon acceptance of the manuscript, Unity packages, source code, and additional resources will be shared.

---

## Overview
This study explores how contextual external human-machine interfaces (eHMIs) in autonomous vehicles (AVs) facilitate communication with multiple road users—pedestrians, cyclists, and manual vehicle drivers—in complex traffic scenarios. Using a VR-based multi-agent simulation, we demonstrate the effectiveness of eHMIs in reducing conflicts and improving safety.

---

## Features
- **VR-Based Multi-Agent Simulation**: Simulates interactions between AVs and road users in real-time.
- **Pre-Built Executables**: Allows reviewers to replicate the experimental setup without requiring source code.

## Repository Structure
- **Builds/**: Contains pre-built executables for the server and client environments (e.g., `server.exe`, `pedestrian.exe`, `cyclist.exe`, `driver.exe`).
- **Docs/**: Includes the Setup Guide (`SetupGuide.pdf`) and the System Diagram (`SystemDiagram.pdf`).

---

## Builds
The `Builds/` folder contains pre-built executables for running the system:
- **server**: Manages real-time synchronization between all clients.
- **pedestrian**: Simulates pedestrian interactions in VR.
- **cyclist**: Simulates cyclist interactions in VR.
- **driver**: Simulates manual vehicle driver interactions in VR.

Each folder contains the respective executable file (`.exe`) and necessary dependencies.

---

## Quickstart

### Running the Pre-Built Executables
1. **Download the appropriate executable** from the `Builds/` folder.
2. **Start the server**:
   - Navigate to the `server/` folder:
     ```bash
     cd path/to/server
     ```
   - Run the server in headless mode:
     ```bash
     ./server.exe -batchmode -nographics
     ```
   - The server will bind to `0.0.0.0` and listen on port `7777` by default.
3. **Run the client executable** for the desired role:
   - Pedestrian: `./pedestrian.exe`
   - Cyclist: `./cyclist.exe`
   - Driver: `./driver.exe`
4. **Connect to the server** using the server's IP address:
   - Default: `127.0.0.1` for local testing.
   - Custom: Specify the IP address with the `-address` flag:
     ```bash
     ./client.exe -address <server_ip>
     ```


---

## Documentation
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step instructions for running the system.
- **[System Diagram](Docs/SystemDiagram.pdf)**: Overview of the system architecture.

---

## Supported Platforms
Currently, only **Windows** builds are provided to ensure compatibility. Builds for **MacOS** or **Linux** can be generated upon request.

---

## Notes
- In the original experimental setup, the pedestrian environment also acted as the server. To enhance deployment flexibility, the server is now independent, allowing all clients (pedestrians, cyclists, drivers) to connect seamlessly. This change does not affect the experimental results or the system's functionality.

- Unity source code and additional resources will be shared upon manuscript acceptance.

---

## Demo Video
Watch the system in action through our demo video:
[![Demo Video](link will be added)]

---

## License
This repository is licensed under the MIT License. See `LICENSE` for details.
