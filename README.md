# Contextual eHMI System for Multi-User Traffic Scenarios

This repository supports the open science goals of the research paper titled:
**"Physiological and Behavioral Analysis of Contextual eHMI in Multi-Road User Conflict Scenarios"**.

The system simulates a VR-based contextual eHMI for autonomous vehicles interacting with multiple road users (pedestrians, cyclists, and drivers). This repository includes pre-built executables, core scripts, and documentation to facilitate reproducibility and understanding of the study.

---

## Repository Structure
- **Builds/**: Pre-built executables for Windows, Mac, and Linux.
- **Docs/**: Documentation, including setup guides, methodology, and system diagrams.
- **Scripts/**: Core Unity scripts used for the server and client environments.
- **Data/**: Sample logs and example data for reproducing study results.

---

## Getting Started

### Option 1: Run Pre-Built Executables
1. Download the appropriate executable from the `Builds/` folder.
2. Start the **Server** executable first.
3. Run the respective **Client** executable:
   - `Pedestrian.exe` for the pedestrian environment.
   - `Cyclist.exe` for the cyclist environment.
   - `Driver.exe` for the driver environment.
4. Connect the client to the server using the displayed IP address.

### Option 2: Reproduce or Extend Using Unity
1. Download the Unity project scripts from the `Scripts/` folder.
2. Import them into a Unity 2021.3 or later project.
3. Configure the Unity Mirror settings for server-client communication.

---

## Documentation
- **SetupGuide.pdf**: Step-by-step instructions for installation and execution.
- **Methodology.pdf**: Details about the study's design and methodology.
- **SystemDiagram.png**: Visualization of the system's architecture.

---

## License
This repository is licensed under the MIT License. See `LICENSE` for details.
