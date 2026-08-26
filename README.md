<!--
  260612_code
  260612_documentation
-->

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset=".github/repository/logo/repository-logo-dark.png">
    <source media="(prefers-color-scheme: light)" srcset=".github/repository/logo/repository-logo-light.png">
    <img alt="Fallback image description" src=".github/repository/logo/repository-logo-light.png">
  </picture>

  ![RELEASE](https://img.shields.io/badge/v1.0-teal)&nbsp;
  ![STAGE](https://img.shields.io/badge/BETA-yellow)&nbsp; <!-- Alpha = Red, Beta = Yellow, Stable = Green -->
  ![LICENSE](https://img.shields.io/badge/License-apache-blue)&nbsp;
  ![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)&nbsp;

  <h3>CHANGELOG.md generator</h3>

</div>

---

<h6 align="center">

  [MANUAL](docs/man/README.md)&nbsp;&bull;&nbsp;[CHANGELOG](docs/CHANGELOG.md)&nbsp;&bull;&nbsp;[ROADMAP](docs/ROADMAP.md)&nbsp;&bull;&nbsp;[KNOWN ISSUES](docs/KNOWN-ISSUES.md)

</h6>

---

| CONTENTS                                    |
|---------------------------------------------|
| [About Cooke](#about-cooke) |
| [How It Works](#how-it-works)               |
| [Getting Started](#getting-started)         |
| [Installing](#installing)                   |
| [Usage](#usage)                             |
| [Acknowledgements](#acknowledgements)       |
| [Related Projects](#related-projects)       |
| [License](#license)                         |

## About Cooke

Cooke is a command-line tool that creates customized documentation for git repositories.
### Features

* Generates a CHANGELOG.md file
* Generates a RELEASE-NOTES.md file
* Highly customizable

### What's New

* New feature — A brief description of the new feature and its benefits.
* Improvement — A brief description of the improvement and its benefits.
* Bug fix — A brief description of the bug fix and its impact.

### Requirements

* .NET 8 runtime
* Windows operating system

> [!INFO] 
> Since Cooke is written in .NET 10 it should be (in theory) cross-platform, but currently has only been tested on Windows.

## How It Works

```mermaid
---
title: Overview of Cooke process
---
graph TB
  %% Components
  StartCooke@{shape: circle, label: "Start"}
  %%InitCooke@{shape: hex, label: "Initialize\nCooke"}
  %%ExpLog@{shape: lean-r, label: "Export\n git commit\nlogs"}
  %%GenDoc@{shape: rect, label: "Generate\ndocumentation"}
  %%ExpDoc@{shape: lean-r, label: "Export\ngenerated\ndocumentation"}
  %%StopCooke@{shape: fr-circ, label: ""}

  %%StartCooke --> InitCooke ---> ExpLog ---> GenDoc --> ExpDoc --> StopCooke
  %%InitCooke --> InitializeCooke
  subgraph InitializeCooke ["Initialize Cooke"]
    direction LR
    %% Components
    LoadConfig@{shape: hex, label: "Load\nconfiguration\nsettings"}
    %% Layout: none
    %% Styles: Global
  end

  subgraph ExportGitCommitLog ["Export git commit log"]
    direction LR
    %% Components
    StartExp@{shape: sm-circ, label: ""}
    RunCmd@{shape: rounded, label: "Execute the\n\"git log --pretty=fuller\"\ncommand"}
    ExpLog@{shape: lean-r, label: "Export\n git commit\nlogs"}
    %% Layout
    StartExp --> RunCmd --> ExpLog -.-> Continue:::Hidden
    %% Styles
    classDef Other stroke:#eb9430,fill:#FFFFFF,color:#000000,stroke-width:1px
    classDef ExportLog stroke:#FFFFFF,fill:#eb9430,color:#FFFFFF,stroke-width:3px
    class StartExp,RunCmd Other
    class ExpLog ExportLog
  end

  subgraph GenerateDocumentation ["Generate documentation"]
    direction LR
    %% Components
    StartParse@{shape: sm-circ, label: ""}
    ReadLog@{shape: rect, label: "Read line\nof git log"}
    StartWithCookeTag@{shape: diamond, label: "Does the line start\nwith a **CookeTag**?"}
    CookeTagChange@{shape: rounded, label: "[%CHANGE%]"}
    CookeTagNote@{shape: rounded, label: "[NOTE]"}
    CookeTagInfo@{shape: rounded, label: "[INFO]"}
    CookeTagRelease@{shape: rounded, label: "[RELEASE]"}
    FormatDoc@{shape: rounded, label: "Format\nDocumentation"}
    GenerateDoc@{shape: docs, label: "Generate\ndocumentation"}
    %% Layout
    StartParse --> ReadLog
    ReadLog --> StartWithCookeTag
    StartWithCookeTag -- NO --> ReadLog
    StartWithCookeTag -- YES: [%CHANGE%] --> CookeTagChange
    StartWithCookeTag -- YES: [NOTE] --> CookeTagNote
    StartWithCookeTag -- YES: [INFO] --> CookeTagInfo
    StartWithCookeTag -- YES: [RELEASE] --> CookeTagRelease
    CookeTagChange --> ReadLog
    CookeTagNote --> ReadLog
    CookeTagInfo --> ReadLog
    CookeTagRelease --> ReadLog
    CookeTagChange --> FormatDoc
    CookeTagNote --> FormatDoc
    CookeTagInfo --> FormatDoc
    CookeTagRelease --> FormatDoc
    FormatDoc --> GenerateDoc
    %% Styles
    %%classDef ReadLine stroke:#000000,fill:#FFFFFF,color:#000000,stroke-width:1px
    classDef Decision stroke:#FFFFFF,fill:#eb9430,color:#000000,stroke-width:3px
    classDef CookeTag stroke:#FFFFFF,fill:#a64369,color:#FFFFFF,stroke-width:3px
    classDef Generate stroke:#FFFFFF,fill:#58a6da,color:#000000,stroke-width:3px 
    class FormatDoc,ReadLog BW
    class StartWithCookeTag Decision
    class CookeTagChange,CookeTagNote,CookeTagInfo,CookeTagRelease CookeTag
    class GenerateDoc Generate
  end

  %% Layout
  StartCooke --> InitializeCooke --> ExportGitCommitLog --> GenerateDocumentation

  %% Global Styles
  classDef BW stroke:#000000,fill:#FFFFFF,color:#000000,stroke-width:1px
  classDef WB stroke:#FFFFFF,fill:#000000,color:#FFFFFF,stroke-width:1px
  classDef Hidden display: none;
  %% Styles
  classDef StartStop stroke:#FFFFFF,fill:#000000,color:#FFFFFF,stroke-width:1px
  classDef Initialize stroke:#A6A6A6,fill:#008060,color:#FFFFFF,stroke-width:1px
  classDef ExportLog stroke:#FFFFFF,fill:#eb9430,color:#FFFFFF,stroke-width:3px
  classDef GenerateDoc stroke:#FFFFFF,fill:#58a6da,color:#FFFFFF,stroke-width:3px
  classDef ExportDoc stroke:#FFFFFF,fill:#a64369,color:#FFFFFF,stroke-width:3px
  class StartCooke,StopCooke StartStop
  class Init Initialize
  class ExpLog ExportLog
  class GenDoc GenerateDoc
  class ExpDoc ExportDoc
```

<!-- ========================================================= [HOW IT WORKS] -->

<!-- [GETTING STARTED] =========================================================
* Before you begin
  Any prerequisites, assumptions, or other information a user should know before
  getting started.
* Prerequisites
  List of software, hardware, or other requirements.

  If this section is only comprised of prerequisites, it can be merged with the
  About section.
============================================================================ -->

## Getting Started

A quick overview of how to get started with the project.

### Before you begin

Any assumptions, or other information a user should know before

### Requirements

| Requirement | Minimum version | Notes |
|-------------|-----------------|-------|
| [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) | 10.0 | Required to build and run. |
| Requirement | | |
| Requirement | | |

<!-- [INSTALLING] =========================================================
* Installing
  Step-by-step instructions for installing the project on supported platforms.

  This section may contain the Prerequisites.

  In general, this should be a quick overview of the installation process,
  with a link to docs/man/README.md.
============================================================================ -->

## Installing

Quick summary of installation instructions, or link to the Installing documentation.

<!-- ========================================================== [INSTALLING] -->

<!-- [USAGE] ===================================================================
* Usage
  Step-by-step instructions for using the project on supported platforms.
  Remove OS sections that are not supported.

  In general, this should be a quick overview of the usage process,
  with a link to docs/man/README.md.
============================================================================ -->

<!-- [SETUP] ===================================================================
* Setup
  Step-by-step instructions for setting up the project on supported platforms.
  Remove OS sections that are not supported.

  In general, this should be a quick overview of the setup process,
  with a link to docs/man/README.md.
============================================================================ -->

<!-- =============================================================== [SETUP] -->

## Usage

Step-by-step instructions for using the project on supported platforms.

<!-- =============================================================== [USAGE] -->

<!-- [DOCUMENTATION] ===========================================================
* Documentation
  A quick overview of the documentation.
============================================================================ -->

## Documentation

Documentation is available.

<!-- ======================================================= [DOCUMENTATION] -->

<!-- [ACKNOWLEDGEMENTS] ========================================================
* Acknowledgements
  List of acknowledgements, or remove this section if there are none.
============================================================================ -->

## Acknowledgements

None.

<!-- ==================================================== [ACKNOWLEDGEMENTS] -->

<!-- [RELATED PROJECTS] ========================================================
* Related projects
  List of related projects, or remove this section if there are none.
============================================================================ -->

## Related projects

None.

<!-- ==================================================== [RELATED PROJECTS] -->

<!-- [LICENSE] =================================================================
* License
  The license under which the project is distributed.
============================================================================ -->

## License

Distributed under the [Apache 2.0 License](LICENSE).  
Copyright &copy; 2026 %Owner%

<!-- ============================================================= [LICENSE] -->

---

<!-- [HORIZONTAL MENU] =========================================================
* Horizontal menu (bottom)
  Contains components that aren't in/don't belong in the table of contents.
---------------------------------------------------------------------------- -->

<h6 align="center">

  [FAQ](docs/FAQ.md)&nbsp;&bull;&nbsp;[DEVELOPMENT](docs/DEVELOPMENT.md)&nbsp;&bull;&nbsp;[API](docs/api/README.md)&nbsp;&bull;&nbsp;[TESTING](docs/TESTING.md)&nbsp;&bull;&nbsp;[SUPPORT](docs/SUPPORT.md)&nbsp;&bull;&nbsp;[NOTICES](docs/NOTICES.md)
  
</h6>

<!-- ===================================================== [HORIZONTAL MENU] -->

---

<sub>Last updated: 260612</sub>
