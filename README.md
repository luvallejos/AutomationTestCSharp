# Automation Exercise Project
[![Build Status](https://dev.azure.com/lucia-qa-automation/AutomationProjectCSharp/_apis/build/status%2Fluvallejos.AutomationTestCSharp?branchName=master)](https://dev.azure.com/lucia-qa-automation/AutomationProjectCSharp/_build/latest?definitionId=1&branchName=master)

## 📄 Table of Contents
1. [Introduction](#introduction)
2. [Technologies and tools used](#technologies)
3. [Framework Architecture](#framework)

<a name="introduction"></a>
## Introduction

This project contains an automation framework built with **C#**, **Selenium WebDriver**, and **NUnit**.
It includes a full suite of UI tests, along with a set of API request helpers used to support and validate different parts of the UI workflows.

The framework follows the Page Object Model (POM) methodology, ensuring a clean structure, easy maintenance, and high scalability as the project expands.

---

<a name="technologies"></a>
## 🔧 Technologies and tools used in this project:

### C# / Selenium / NUnit

**Selenium WebDriver:** used to automate all UI interactions and browser actions.

**NUnit**: provides the testing framework, assertions, and test execution engine.

**C#**: core language used to build the test framework, helpers, and utilities.

#### API Helpers

Lightweight API requests implemented as helpers to support UI scenarios.

Used to prepare test data, validate backend states, and ensure tests remain stable and reliable.

---

<a name="framework"></a>

## 📦 Framework Architecture

### Page Object Model (POM):

Encapsulates page elements, user actions, and business logic to improve reusability and maintainability.

Clear separation between page objects, test classes, and API utilities, making the framework scalable and easy to extend.

---

### 📁 Project Structure

```bash
📁 AutomationTestCSharp
├── 📁 Resources/          # Files and JSON data sources for tests
├── 📁 Tests/              # UI test suites created with NUnit
├── 📁 Utilities/          # Custom tests helpers, data manage, and common methods
📁 UITestFramework
├── 📁 Dto/                # Classes holding structured test data (e.g., users, products, API payloads)
├── 📁 Pages/              # Page Object Model classes for each application page
├── 📁 Pages/Common/       # POM classes for shared UI elements across screens
├── 📁 Utilities/          # Custom helpers, extensions, and common methods
└── 📄 README.md           # Documentation file
```
---

## 🚀 Continuous Integration (CI/CD) with Azure DevOps

This project includes a fully configured **Azure DevOps pipeline** that runs automatically on every push to the repository.

The pipeline performs the following steps:

- ✔️ Restore dependencies  
- ✔️ Build the project  
- ✔️ Execute all automated tests  
- ✔️ Capture screenshots for failed tests  
- ✔️ Generate test result files (`.trx`, logs, etc.)  
- ✔️ Publish the results to GitHub Releases  

Even though the pipeline itself is private, the test execution results are **automatically uploaded and publicly accessible**.

---

## 📊 Test Execution Report (GitHub Releases)

You can download and view the latest automated test execution results here:

👉 **[Latest Test Report (HTML)](https://github.com/luvallejos/AutomationTestCSharp/releases/latest)**

Each pipeline run uploads a ZIP file containing:

- 📁 Screenshots of failed tests  
- 📄 `.trx` test result files  
- 📊 Additional logs/evidence generated during the run  
- 📊 The test report which includes: 
    - Pass/Fail summary  
    - Execution time  
    - Error stack traces  
    - Per-test details 