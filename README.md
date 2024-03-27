<!-- Last updated: 240326 -->

<div align="center">

 ![](./.github/Images/Logos/cooke-logo_256x256.png)

# Cooke

  ![Version](https://img.shields.io/badge/VERSION-0.1.1-white?style=for-the-badge)

</div>

Generate repository documentation.

The current version of Cooke (0.1.1) generates a CHANGELOG.md file.

## Installation

1. Download release 0.1.1
2. Extract that file to %REPOSITORY-NAME%/Cooke/

## Configuration

The `cooke-config.json` file contains the configuration settings for Cooke, and is created the first time you run Cooke.

In general, you only need to modify the following entries:

```#text
"RepoName": "%YOUR-REPOSITORY-NAME%",
"RepoURL": "%YOUR-REPOSITORY-URL%",
```

For example:

```#text
"RepoName": "Cooke",
"RepoURL": "https://github.com/APrettyCoolProgram/Cooke",
```

### Optional configuration

You may want to change the following configuration settings:

- `VerboseLog`  
  When set to `true`, the messages displayed will be more detailed.

- `DotGitPath`  
  The path to your repository `.git/` folder

- `ChangelogIncludeName`  
  When set to `true`, the CHANGELOG.md title will include your repository name.

  When set to `false`, the CHANGELOG.md title will just be "Changelog".

- `ChangelogRepoPath`  
  The path where you want the CHANGELOG.md to be generated.

## Usage

1. Install Cooke
2. Open a terminal in %REPOSITORY-NAME%/Cooke/
3. Type `cooke`

### First execution

The first time Cooke is executed, it will create a configuration file named `cooke-config.json`.

You should review this file and make the necessary changes for your repository.

### Arguments

You can use the following arguments with Cooke:

- `reset`  
  Resets Cooke to a default state, with a default set of directories and configuration file.

### Changelog keywords

These are the reserved keywords:

- `[VERSION]`


