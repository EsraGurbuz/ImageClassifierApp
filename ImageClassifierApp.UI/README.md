# ImageClassifierApp 📸🤖

An advanced Image Classification application built with **C#**, **.NET 8.0**, **Windows Forms**, and **ML.NET** (Machine Learning for .NET). Designed with strict adherence to Object-Oriented Programming (OOP) principles and clean architecture patterns.

## 🚀 Key Features
- **Deep Learning Integration:** Uses ML.NET's ImageClassification trainer (leveraging TensorFlow backend).
- **Asynchronous Execution:** Model training operations are safely offloaded to background threads using `Task.Run` to prevent UI freezing.
- **Clean Architecture:** Separated into `Interfaces`, `Services`, `Models`, and `UI` layers.

## 🧠 Applied Software Patterns & OOP Concepts
- **Singleton Pattern:** Engineered `ImageClassifierService` as a thread-safe Singleton to manage heavy ML contexts and optimize memory consumption.
- **Abstraction (Loose Coupling):** UI layer interacts exclusively with the `IImageClassifierService` interface, allowing underlying ML libraries to be swapped without breaking changes.
- **Event-Driven Programming:** Leveraged Windows Forms event framework combined with `async/await` patterns for interactive user experiences.
- **Data Transfer Objects (DTO):** Utilized `ModelInput` and `ModelOutput` structures to enforce Single Responsibility.

## 📁 Project Structure
```text
ImageClassifierApp/
│
├── ImageClassifierApp.UI/
│   ├── Interfaces/        # Service contracts (Abstractions)
│   ├── Models/            # DTOs and Data schemas for ML.NET
│   ├── Services/          # Concrete business logic & ML pipelines (Singleton)
│   └── UI/                # MainForm and visual layers (Code-behind)