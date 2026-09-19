# Contributing to StarDetective

Welcome to our repository, StarDetective. We welcome your willingness for contribution.

In our opinion, the repository must be organized to enable newcomers to contribute to this repository easily.
So we hope you to follow this contribution policies to keep this repository organized.

## Branch Naming Conventions

When contributing to this repository, please follow the branch naming format below. 

### Branch Name Structure
* **If you are using an AI agent** (such as Codex or Claude), please use a double-prefix structure:
  ```text
  [Change Type]/[AI Agent Name]/[kebab-case-description]
  ```
  *Example:* `feat/codex/add-user-settings-dialog`
* **If you are contributing directly (without an AI agent)**, please use the change type as the prefix:
  ```text
  [Change Type]/[kebab-case-description]
  ```
  *Example:* `feat/add-user-settings-dialog`

### Change Type Prefixes

Whether you are working manually or with an AI, please always explicitly specify one of the following prefixes to indicate the type of change:

|   Prefix   | Description                                                           |
|:----------:|:----------------------------------------------------------------------|
|   `feat`   | Adding a new feature                                                  |
|   `fix`    | Fixing a bug                                                          |
|   `docs`   | Documentation updates                                                 |
| `refactor` | Code changes that neither fix bugs nor add features                   |
|   `test`   | Adding or updating tests                                              |
|   `perf`   | Performance improvements                                              |
|  `chore`   | Maintenance tasks, dependency updates, or build configuration changes |

## Repository Rules & Workflow

To ensure that all ongoing work remains visible to everyone and benefits from diverse perspectives, this repository enforces strict collaboration guidelines. Please read and follow these steps carefully:

### 1. Link Everything to an Issue or Discussion (Avoid Duplication)

* **All Pull Requests must be associated with an Issue or a Discussion Topic.**
* **Important:** Before creating a new Issue or Discussion, **please carefully search existing and closed Issues/Discussions** to avoid creating duplicates. Do not add redundant topics; instead, join the conversation on an existing thread if your proposal relates to it.
* Before starting any work, make sure there is a proper, unique Issue or Discussion describing what you plan to do, and reference it in your PR.

### 2. Open Collaboration & Intermediate Reporting

This project values collective oversight and continuous feedback. We encourage everyone to share their progress early and often:

* **Early and Interim Updates:** Do not wait until your work is 100% complete to share it. Post intermediate updates, push work-in-progress (WIP) branches, or open draft PRs early to keep everyone informed.
* **Incorporate Feedback:** Actively seek out and welcome opinions from other contributors and maintainers during the editing process. Be ready to adapt your approach based on the discussion.

### 3. Pull Request Process

1. **Check & Discuss First:** Search existing topics to avoid duplicates, then open or comment on an Issue/Discussion outlining your approach.
2. **Draft PR & Updates:** Open a Draft Pull Request early, use the correct branch prefix, and provide regular progress reports in the PR comments.
3. **Review & Iterate:** Engage with reviewers, address feedback openly, and refine the implementation together.
4. **Merge:** Once approved and all checks pass, the PR will be merged into the main branch.
