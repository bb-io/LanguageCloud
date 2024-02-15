# Blackbird.io Language Cloud

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

RWS Language Cloud is a translation management system (TMS) developed by RWS, a global leader in language services and technology solutions. RWS Language Cloud offers a comprehensive suite of tools and services for managing translation and localization projects efficiently. It provides capabilities such as project management, translation memory, terminology management, quality assurance, and workflow automation.

## Before setting up

Before you can connect you need to make sure that:

- You have access to a Language Cloud instance and have admin rights on this instance.
- You have your Language Cloud API key. (This can be retrieved from the RWS Trados Enterprise web UI, in the top right-hand corner, select your profile, and then select Integrations > Api Keys tab, get API KEY value. You may need to create one by clicking on the New API key button on the same tab.)
- You have your Language Cloud Tenant ID. (This can be retrieved from the RWS Trados Enterprise web UI, in the top right-hand corner, select your profile, and then select Manage Account > Account Information tab, check the value for Trados Account ID.)

## Connecting

1. Navigate to Apps and search for Trados. If you cannot find Trados then click _Add App_ in the top right corner, select Trados and add the app to your Blackbird environment.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My Trados connection'.
4. Fill in the API Key of your Language Cloud instance.
5. Fill in the Tenant ID of your Language Cloud instance.
7. Click _Connect_.

![TradosNewConnection](image/README/TradosNewConnection.png)

## Actions

### Customers 

- **Get customer** Get customer by Id
- **List all customers** List all customers

### Files 

- **Upload source file** Upload source file to project
- **Attach source file to project** Attach source file to project
- **Download target file** Download target file by id
- **Get source file info** Get source file info
- **Get target file info** Get target file info
- **List project source files** List project source files
- **List project target files** List target source files
- **Upload zip archive** Upload zip archive with source files

### Folders 

- **Get folder** Get folder by Id
- **List all folders** List all folders

### Glossaries 

- **Export glossary** Export glossary
- **Import glossary** Import glossary

### Groups 

- **Get group** Get group by Id
- **List all groups** List all groups

### Languages 

- **List all languages** List all languages

### Projects 

- **Get project** Get project by Id
- **Complete project** Complete project by Id
- **Create project** Create project
- **Create project from template** Create project from template
- **Edit project** Edit project
- **Get project template** Get project template by Id
- **List all project templates** List all project templates
- **List all projects** List all projects
- **Start project** Start project by Id
- **Delete project** Delete project

### Reports 

- **Download quote report** Download quote report for project

### Tasks 

- **Accept task** Accept task by Id
- **Assign task** Assign task by Id
- **Complete task** Complete task by Id
- **Get task** Get task by Id
- **List all project tasks** List all project tasks
- **List all tasks** List all tasks
- **Reclaim task** Reclaim task by Id
- **Reject task** Reject task by Id
- **Release task** Release task by Id

### Translation Memories 

- **Create translation memory** Create translation memory
- **Get translation memory** Get translation memory
- **Import TMX file** Import TMX file
- **List translation memories** List translation memories

### Users 

- **Get user** Get user by Id
- **List all users** List all users

## Events

### File 

- **On source file created**
- **On source file deleted**
- **On source file updated**
- **On target file created**
- **On target file deleted**
- **On target file updated**

### Group Project 

- **On group project membership changed**

### Project 

- **On project created**
- **On project deleted**
- **On project updated**

### Project Template 
- **On project template deleted**
- **On project template updated**
- **On project template created**

### Task 
- **On task updated**
- **On task accepted**
- **On task completed**
- **On task created**
- **On task deleted**

### Error Task 

- **On error task created**
- **On error task deleted**
- **On error task updated**

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.
