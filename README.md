# ImageClassifierApp 📸🤖

An advanced Image Classification application built with **C#**, **.NET 8.0**, **Windows Forms**, and **ML.NET** (Machine Learning for .NET). Designed with strict adherence to Object-Oriented Programming (OOP) principles, clean architecture, and modern data-binding pipelines.

## 🚀 Key Features
- **Deep Learning Integration:** Uses ML.NET's ImageClassification trainer (leveraging an optimized TensorFlow backend via Transfer Learning with ResNet50).
- **Asynchronous Execution:** Model training and resource-heavy operations are safely offloaded to background threads using `Task.Run` and `async/await` patterns to keep the User Interface (UI) smooth and responsive.
- **Robust Data Hydration:** Replaced rigid, error-prone text loaders with a custom dynamic `File.ReadAllBytes` workflow that loads images directly as raw byte streams into memory, preventing deep learning schema mismatches.
- **Clean Architecture:** Strictly separated into `Interfaces`, `Services`, `Models`, and `UI` layers.

## 🧠 Applied Software Patterns & OOP Concepts
- **Singleton Pattern:** Engineered `ImageClassifierService` as a thread-safe, double-checked locking Singleton to manage heavy ML contexts, prediction engines, and optimize RAM consumption.
- **Abstraction & Loose Coupling:** The UI layer interacts exclusively with the `IImageClassifierService` interface. This allows underlying ML frameworks or training pipelines to be swapped without causing breaking changes in the presentation layer.
- **Data Transfer Objects (DTO):** Utilized robust `ModelInput` and `ModelOutput` classes to enforce the Single Responsibility Principle (SRP) and ensure schema consistency (`byte[]` data representation).
- **Event-Driven Programming:** Leveraged the Windows Forms event architecture to link user components seamlessly to asynchronous back-end service methods.

## 📁 Project Structure
```text
ImageClassifierApp/
│
├── ImageClassifierApp.UI/
│   ├── Interfaces/        # Service contracts (Abstractions)
│   ├── Models/            # DTOs, data schemas, and ML byte structures (byte[] representation)
│   ├── Services/          # Concrete business logic & ML pipelines (Singleton)
│   ├── UI/                # MainForm and visual layers (Code-behind / Partial classes)
│   └── MyDataset/         # Train/Test folders and data mapping files
```
## 🛠️ Installation & Requirements
- **IDE:** Visual Studio 2022 (with .NET Desktop Development workload enabled)
- **Framework:** .NET 8.0
- **NuGet Packages:**
  - Microsoft.ML
  - Microsoft.ML.ImageAnalytics
  - Microsoft.ML.Vision

## How to Run & Dataset Guide

1.Clone the repository and open the solution file (.sln) in Visual Studio.

2.Prepare your dataset inside MyDataset/ with Train/ and Test/ subfolders (e.g., Apple, Banana).

3. Create train_data.txt and test_data.txt using absolute paths mapped to labels:
```text
C:\YourPath\MyDataset\Train\Apple\elma1.jpg,Apple
C:\YourPath\MyDataset\Train\Banana\muz1.jpg,Banana
```
4.Update the dataset file paths inside MainForm.cs to match your local paths.

5.Build and run the application. Use the UI buttons to load data, train the model, and make predictions.
