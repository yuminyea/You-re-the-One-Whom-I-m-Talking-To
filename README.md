# You’re the One Whom I’m Talking To: The Role of Contextual External Human-Machine Interfaces in Multi-Road User Conflict Scenarios

This repository accompanies the IMWUT 2025 February submission (ID: 1102) and supports the open science goals of the research. It contains pre-built executables, key scripts, and documentation to facilitate reproducibility and transparency in the study.

---

## Abstract
As autonomous vehicles (AVs) become more prevalent, mixed traffic environments involving pedestrians, cyclists, and manual vehicle drivers present challenges for safe and effective interactions. External Human-Machine Interfaces (eHMIs) have emerged as a solution, with context-based eHMIs—providing specific information such as whom, when, and where—showing potential for improving communication in complex scenarios. However, their impact on road user behavior and safety in interactions involving multiple road users remains insufficiently explored. This study addresses this gap by examining how contextual eHMIs affect crossing performance and subjective feelings during multi-user conflict scenarios. Using a virtual reality-based multi-agent simulation, 42 participants were evenly divided into three groups, each representing pedestrians, cyclists, and manual vehicle drivers, to make crossing decisions in interactions with an AV. Our findings demonstrated that providing contextual information in AV-multi-road user interactions significantly improved participants’ crossing performance and enhanced their perceived safety, trust, and clarity. These findings highlight the potential of context-based eHMIs to support safer and more intuitive interactions in mixed-traffic environments.

---

## Repository Structure
- **Builds/**: Pre-built executables for server and client environments.
- **Docs/**: Detailed documentation, including methodology, setup guides, and system diagrams.
- **Scripts/**: Core Unity scripts for server-client interactions.
- **Data/**: Sample logs and data for reproducing the study's results.

---

## Getting Started

### Option 1: Pre-Built Executables
1. Download the appropriate executable from the `Builds/` folder.
2. Start the server executable first.
3. Run the respective client executable (Pedestrian, Cyclist, Driver).
4. Follow the on-screen instructions to connect to the server.

### Option 2: Unity Package
1. Download the scripts from the `Scripts/` folder.
2. Import them into a Unity 2021.3 or later project.
3. Configure the Unity Mirror network settings for server-client communication.

---

## Documentation
- **[Setup Guide](Docs/SetupGuide.pdf)**: Step-by-step instructions for running the system.
- **[Methodology](Docs/Methodology.pdf)**: Details of the research design and experimental setup.
- **[System Diagram](Docs/SystemDiagram.png)**: Overview of the system architecture.

---

## Citation
If you use this repository in your research, please cite:

---

## License
This repository is licensed under the MIT License. See `LICENSE` for details.
