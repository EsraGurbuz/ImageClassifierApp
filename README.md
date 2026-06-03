# 📸 ImageClassifierApp (Desktop Deep Learning Application)

This project was developed as part of the **Object-Oriented Programming (OOP)** course at **Fırat University, Software Engineering Department**. The application is an advanced desktop image classification platform featuring deep learning integration powered by ML.NET and an optimized TensorFlow backend.

---

## 🎓 Student Information

* **Name & Surname:** Esra Gürbüz
* **Student ID:** 230543001
* **University:** Fırat University
* **Department:** Software Engineering (Sophomore / 2nd Year)

---

## 🚀 Architectural & OOP Highlights

This application avoids "Spaghetti Code" by segregating components into decoupled, dedicated architectural layers following production-ready standards:

* **Abstraction & Interfaces (`IImageClassifierService`):** Core business logic operations and training pipelines are abstracted through interfaces, enforcing a strict contract and allowing the underlying ML framework to be swapped seamlessly.
* **Dependency Inversion (Loose Coupling):** The presentation layer (`MainForm`) relies exclusively on the `IImageClassifierService` abstraction rather than concrete implementations, preventing rigid class dependencies.
* **Singleton Design Pattern:** Initializing the machine learning context and managing prediction engines is highly resource-intensive for RAM. To optimize memory consumption and enforce thread safety, the core service layer is engineered using a double-checked locking Singleton pattern.
* **Robust Data Hydration:** Replaced rigid, error-prone text loaders with a custom dynamic `File.ReadAllBytes` workflow. The system loads images from the local storage directly as raw `byte[]` (byte arrays) into memory, ensuring strict schema alignment with the deep learning model and eliminating runtime uyuşmazlığı.
* **Asynchronous Execution:** Deep learning training operations push the processor to full capacity. To preserve a smooth user experience, heavy pipeline orchestration is offloaded to background threads using `Task.Run` and `async/await` patterns, ensuring the presentation layer never freezes.
* **Event-Driven Programming:** The UI layer operates entirely on an asynchronous event-driven model, natively capturing user actions (clicks, training requests, image selections) via specialized WinForms Event Handlers.

---

## 📁 Directory Structure

The solution is divided into four distinct modules to achieve a clear separation of concerns:

```text
ImageClassifierApp/
│
├── ImageClassifierApp.UI/
│   ├── Interfaces/        # Service contracts, abstract business pipelines, and API boundaries
│   │   └── IImageClassifierService.cs
│   │
│   ├── Models/            # Domain Data Transfer Objects (DTO) and strict ML schema models
│   │   ├── ModelInput.cs  # Encapsulates raw byte array representation (byte[]) of input data
│   │   └── ModelOutput.cs # Encapsulates classification scores and predicted label outcomes
│   │
│   ├── Services/          # Concrete business logic, ML pipelines, and Singleton implementation
│   │   └── ImageClassifierService.cs
│   │
│   └── UI/                # Presentation layer forms, visual state management, and code-behind
│       ├── MainForm.cs
│       └── Program.cs     # Application bootstrapper and main entry point
```
## 🛠️ Tech Stack & Dependencies

* **Language:** C#
* **Framework:** .NET 8.0 Ecosystem
* **User Interface:** Windows Forms (WinForms)
* **Core Framework:** ML.NET (Machine Learning for .NET)
* **Deep Learning Architecture:** ResNet50 Architecture (TensorFlow Backend via Transfer Learning)
* **Dependencies:** (Managed via NuGet Package Manager)
  * `Microsoft.ML`
  * `Microsoft.ML.ImageAnalytics`
  * `Microsoft.ML.Vision`

---

## 🔧 Installation & Setup

1. Clone this repository to your local machine:
   ```bash
   git clone [https://github.com/your-username/ImageClassifierApp.git](https://github.com/your-username/ImageClassifierApp.git)
   ```
2. Open the `ImageClassifierApp.sln` solution file in **Visual Studio**.
3. Restore the NuGet packages (the IDE will automatically fetch and restore `Microsoft.ML` and its dependencies).
4. Prepare your local dataset folder structures inside the project directory (e.g., separating training and testing image sets mapped within your local paths).
5. Build the project using `Ctrl + Shift + B` to ensure all dependencies are linked.
6. Press `F5` or click the green **Start** button to launch the desktop application.

---
*Developed for educational purposes as a technical OOP portfolio milestone.*
