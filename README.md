# Automation Exercise Project

## 📄 Table of Contents
1. [Introduction](#introduction)
2. [Technologies and tools used](#technologies)
3. [Framework Architecture](#framework)

<a name="introduction"></a>
## Introduction

This project contains an automation framework built with **C#**, **Selenium WebDriver**, and **NUnit**.
It includes a full suite of UI tests, along with a set of API request helpers used to support and validate different parts of the UI workflows.

The framework follows the Page Object Model (POM) methodology, ensuring a clean structure, easy maintenance, and high scalability as the project expands.

<a name="technologies"></a>
## 🔧 Technologies and tools used in this project:

### C# / Selenium / NUnit

**Selenium WebDriver:** used to automate all UI interactions and browser actions.

**NUnit**: provides the testing framework, assertions, and test execution engine.

**C#**: core language used to build the test framework, helpers, and utilities.

#### API Helpers

Lightweight API requests implemented as helpers to support UI scenarios.

Used to prepare test data, validate backend states, and ensure tests remain stable and reliable.

<a name="framework"></a>

## 📦 Framework Architecture

### Page Object Model (POM):

Encapsulates page elements, user actions, and business logic to improve reusability and maintainability.

Clear separation between page objects, test classes, and API utilities, making the framework scalable and easy to extend.

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