# You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios

This repository supports the IMWUT 2025 February submission (ID: 1102) by providing documentation, pre-built executables, and visual materials related to the study. Upon acceptance of the manuscript, Unity packages, source code, and additional resources will be shared.

---

## Abstract
This study investigates the role of contextual external human-machine interfaces (eHMIs) in autonomous vehicles (AVs) for resolving multi-road user conflict scenarios. Using VR-based multi-agent simulations, we evaluate methods for effective communication between AVs and pedestrians, cyclists, and manual vehicle drivers in complex traffic scenarios.

---

## Repository Structure
- **Builds/**: Contains pre-built executables for the server and client environments (e.g., Pedestrian.exe, Cyclist.exe, Driver.exe).
- **Docs/**: Includes the Setup Guide (SetupGuide.pdf) and the System Diagram (SystemDiagram.png).
- **Data/**: Provides sample logs and simulation data for understanding the experimental setup.

---

## Getting Started

### Pre-Built Executables
1. Download the appropriate executable from the `Builds/` folder.
2. Ensure all devices are connected to the same network.
3. Start the server executable first and confirm it is running.
4. Run the respective client executable (Pedestrian, Cyclist, Driver).
5. Follow the on-screen instructions to connect to the server.

---

## Documentation
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step instructions for running the system.
- **[System Diagram](Docs/SystemDiagram.png)**: Overview of the system architecture.

---

## Supported Platforms
Currently, only **Windows** builds are provided to ensure compatibility. Builds for **MacOS** or **Linux** can be generated upon request.

---

## Notes
In the original experimental setup, the pedestrian environment also acted as the server. For deployment flexibility, the server and pedestrian environments have been separated, allowing the server to operate independently while all clients (pedestrians, cyclists, and drivers) connect seamlessly. This change does not affect the experimental results or the system's functionality.
- To focus on the study's key aspects, the full Unity project and source code have been temporarily withheld. Upon acceptance of the manuscript, these resources, including detailed implementation, will be shared openly.

---

## License
This repository is licensed under the MIT License. See `LICENSE` for details.
