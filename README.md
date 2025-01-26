# You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios

This repository accompanies the IMWUT 2025 February submission (ID: 1102). It serves to support the review process by providing documentation and illustrative materials related to the study. Full Unity packages, source code, and additional resources will be made available upon acceptance of the manuscript.

---

## Abstract
This study investigates the role of contextual external human-machine interfaces (eHMIs) in autonomous vehicles (AVs) for resolving multi-road user conflict scenarios. Using VR-based multi-agent simulations, we evaluate how AVs can effectively communicate with pedestrians, cyclists, and manual vehicle drivers in shared traffic environments.

---

## Repository Structure
- **Builds/**: Pre-built executables for server and client environments.
- **Docs/**: Documentation, including setup guides and system diagrams.
- **Data/**: Sample logs and data to illustrate the experimental setup.

---

## Getting Started

### Pre-Built Executables
1. Download the appropriate executable from the `Builds/` folder.
2. Start the server executable first.
3. Run the respective client executable (Pedestrian, Cyclist, Driver).
4. Follow the on-screen instructions to connect to the server.

---

## Documentation
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step instructions for running the system.
- **[System Diagram](Docs/SystemDiagram.png)**: Overview of the system architecture.

---

## Supported Platforms
For the purposes of this study, the provided build files currently support **Windows** only, as the primary focus is on ensuring a smooth review process for the submitted manuscript.

If needed, builds for **MacOS** or **Linux** can be generated upon request. Unity supports cross-platform builds, and we are prepared to accommodate additional platform-specific requirements to facilitate further testing or replication of the study.

---

## Notes
- In the original experimental setup, the pedestrian environment also acted as the server. However, for ease of deployment and improved flexibility, the pedestrian environment and server have been separated. This modification ensures that users can independently run the server and connect all client environments without additional configurations. The separation does not impact the experimental results or system functionality.
- To streamline the review process and focus on the key aspects of the study, we have temporarily withheld the full Unity project and source code. These resources, including comprehensive implementation details, will be openly shared following the manuscript's acceptance.

---

## License
This repository is licensed under the MIT License. See `LICENSE` for details.
