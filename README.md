<!-- u240509 -->

<div align="center">

  ![](./.github/images/logos/cooke-logo-repository-readme.png)

  <h1>
    Generate git repository documentation.
  </h1>

  <img src="https://img.shields.io/badge/status-active-darkgreen.svg">&nbsp;&nbsp;&nbsp;&nbsp;[![License](https://img.shields.io/github/license/aprettycoolprogram/Cooke)](https://www.apache.org/licenses/LICENSE-2.0)&nbsp;&nbsp;&nbsp;&nbsp;![.NET](https://img.shields.io/badge/.NET-8-blue)&nbsp;&nbsp;&nbsp;&nbsp;![GitHub release](https://img.shields.io/github/release/aprettycoolprogram/Cooke?label=latest%20release)

*** DOCUMENTATION NOT COMPLETE ***

</div>

# About Cooke

Cooke is a command-line tool that creates documentation for git repositories.

Currently, only CHANGELOG.md files are created.

# Installation

1. Download the [current release]()
2. Extract that file to `%REPOSITORY-NAME%/Cooke/`

# Configuration

## Creating the cooke.config configuration file

Cooke configuration settings are located in a file called **cooke.config**.

To create cooke-config, open a command prompt at *%REPOSITORY-NAME%/Cooke/* and type: `cooke`

This will create all of the requirements that Cooke needs, as well as the cook.config file.

## Cooke configuration settings

Cooke has the following user-definable settings:

- `SleepDuration`  
  This is a depreciated setting that will be removed in an upcoming release of Cooke, and should be ignored.

- `KeepHistory`  
  When set to **true**, Cooke will save timestamped copies of any documentation it creates, and save them to the ./history folder.  
  When set to **false**, historical files will not be created.

- `RepositoryName`  
  The name of the repository you are using Cooke with.
  
- `RepositoryUrl`  
  The URL of the repository you are using Cooke with.

- `IncludeRepositoryNameInChangelog`  
  When set to **true**, the CHANGELOG.md header will be "%RepositoryName% CHANGELOG.  
  When set to **false** the CHANGELOG.md header will be "CHANGELOG".

- `ChangelogStartTag`  
  The character that indicates the start of a changelog item. By default this is `[`, but if you require another character, you can define it here.

- `ChangelogEndTag`  
  The character that indicates the rnf of a changelog item. By default this is `]`, but if you require another character, you can define it here.
  
- `ChangelogMdPath`  
  The path where the generated CHANGELOG.md file will be saved, relative to where Cooke is executed.

 for Cooke, and is created the first time you run Cooke.

In general, you only need to modify the following entries:

# Usage

There are two components to using Cooke:

1. Writing git -commit messages
2. Executing Cooke


## Writing `git -commit` messages

Cooke uses "tags" to determine what will be added to a changelog.

Tags look like this (not including quotes): "[ADDED]"

For example:

&nbsp;&nbsp;&nbsp;&nbsp;[ADDED] New functionality

By default, tags start with the `[` character, and end with the `]` character, although that can be changed in the configuration file.

Cooke will convert tags to a nice format for CHANGELOG.md.

For example:

&nbsp;&nbsp;&nbsp;&nbsp;`ADDED` New functionality

In order for something to be added to a changelog the comment line must start with a tag (ex, [NEW]).

Comments that do not start with tags are not included in the CHANGELOG.md file (but will be included in future "release notes" functionality)

### Special tags

You can use any text as a tag, so if you would rather use "[ADD]" than "[ADDED]", go ahead. For the most part, Cooke doesn't care what you use.

However, there are a few special tags that Cooke does care about:

- `[INFO]`  
  The [INFO] tag will be converted to a Mardown quote, and will always be at the top of the current changelog block.

## Executing Cooke
  
To use Cooke, open a terminal in *%REPOSITORY-NAME%/Cooke/* and type a cooke **command**.

### Valid Cooke commands

- `cooke`  
  Create all available git repository documentation.

- `cooke changelog`  
  Generate a CHANGELOG.md file

- `cooke reset`  
  Reset the current Cooke environment, including removing all related files and resetting cooke.config to default values.

- `cooke help`  
  Display the Cooke help screen.

## Generating CHANGELOG.md

TBD