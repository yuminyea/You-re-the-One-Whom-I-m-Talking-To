# You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios

This repository supports the IMWUT 2025 February submission (ID: 1102) by providing documentation, pre-built executables, and visual materials related to the study. Upon acceptance of the manuscript, Unity packages, source code, and additional resources will be shared.

---

## Abstract
This study investigates the role of contextual external human-machine interfaces (eHMIs) in autonomous vehicles (AVs) for resolving multi-road user conflict scenarios. Using VR-based multi-agent simulations, we evaluate methods for effective communication between AVs and pedestrians, cyclists, and manual vehicle drivers in complex traffic scenarios.

---

## Repository Structure
- **Builds/**: Contains pre-built executables for the server and client environments (e.g., `Server.exe`, `Pedestrian.exe`, `Cyclist.exe`, `Driver.exe`).
- **Docs/**: Includes the Setup Guide (`SetupGuide.pdf`) and the System Diagram (`SystemDiagram.pdf`).

---

## Builds
The `Builds/` folder contains pre-built executables for running the system:
- **Server**: Manages real-time synchronization between all clients.
- **Pedestrian**: Simulates pedestrian interactions in VR.
- **Cyclist**: Simulates cyclist interactions in VR.
- **Driver**: Simulates manual vehicle driver interactions in VR.

Each folder contains the respective executable file (`.exe`) and necessary dependencies.

---

## Getting Started

### Pre-Built Executables
1. Download the appropriate executable from the `Builds/` folder.
2. **Start the server executable first**:
   - Use the command below to run the server in **Headless Mode** (recommended for deployment):
     ```
     Server.exe -batchmode -nographics
     ```
   - By default, the server will bind to all available network interfaces (`0.0.0.0`) and listen on port `7777`.
   - The client will need the server's IP address to connect. See [Server IP Setup](#server-ip-setup) for details.
3. Run the respective client executable (`Pedestrian.exe`, `Cyclist.exe`, `Driver.exe`).
4. Follow the on-screen instructions to connect to the server.

---

### Server IP Setup
- **Default Setting**: 
  - The server uses the local IP address (`127.0.0.1`) for testing and local execution.
- **Custom IP Address**:
  - To connect a client to a remote server, specify the server's IP address:
    - **Command Line**: Use the `-address` argument when launching the client executable.  
      Example: `Client.exe -address 192.168.1.100`
- **Port Configuration**:
  - The server and client communicate over port `7777` by default. Ensure that the port is open and not blocked by firewalls or network restrictions.

---

## Documentation
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step instructions for running the system.
- **[System Diagram](Docs/SystemDiagram.pdf)**: Overview of the system architecture.

---

## Supported Platforms
Currently, only **Windows** builds are provided to ensure compatibility. Builds for **MacOS** or **Linux** can be generated upon request.

---

## Notes
In the original experimental setup, the pedestrian environment also acted as the server. For deployment flexibility, the server and pedestrian environments have been separated, allowing the server to operate independently while all clients (pedestrians, cyclists, and drivers) connect seamlessly. This change does not affect the experimental results or the system's functionality.

- To focus on the study's key aspects, the full Unity project and source code have been temporarily withheld. Upon acceptance of the manuscript, these resources, including detailed implementation, will be shared openly.

---

## Demo Video
Watch the system in action through our demo video:
[![Demo Video](link will be added)]

---

## License
This repository is licensed under the MIT License. See `LICENSE` for details.
