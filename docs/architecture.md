# 🧩 Architecture Overview

**Project:** Freemium Photo Editor
**Owner:** Michael Villasfer
**Goal:** Showcase clean architecture, testing discipline, and DevOps mindset — not just a working image editor.

---

## 1. Architectural Vision

The project follows a **Clean Architecture** pattern:
- **Separation of concerns** between UI, Core, and Tests.
- **Dependency flow is one-way:** `App → Core`, `Tests → Core`.
- Designed for **CI/CD automation** and easy cloud deployment (later weeks).

---

## 2. Layer Overview

| Layer | Responsibility | Key Tech / Library |
|--------|----------------|--------------------|
| **Editor.App** | Presentation (UI). Opens, displays, and triggers edit commands. | .NET (WPF / WinUI / MAUI — Windows target). |
| **Editor.Core** | Business logic and image processing engine. No UI dependencies. | C#, ImageSharp (open source, cross-platform). |
| **Editor.Core.Tests** | Unit + snapshot (“golden file”) tests for deterministic validation. | xUnit, FluentAssertions, GitHub Actions CI. |

---

## 3. Data Flow
+-----------------------+␣␣ _
| User Interface |
| (Editor.App - UI) |
+-----------+-----------+_
|
| 1️⃣ User triggers an operation
v
+-----------------------+
| Core Engine |
| (Editor.Core) |
| - Validates params |
| - Applies operation |
| - Updates history |
+-----------+-----------+
|
| 2️⃣ Resulting image (in-memory)
v
+-----------------------+
| Display / Preview |
| (UI re-renders image) |
+-----------------------+

---

## 4. Undo / Redo Model

- Each operation is an **ICommand-like object** implementing:
  - `Apply(ImageState)` → returns new state
  - `Undo(ImageState)` → restores previous state  
- The `HistoryManager` maintains two stacks:
  - `UndoStack` (max 10)
  - `RedoStack`  
- When new operation is applied → `RedoStack` is cleared.

---

## 5. Image Processing Pipeline
Image → Operation (Crop / Rotate / Adjust / B&W)
→ Core Processor
→ Output Bitmap (in-memory)
→ Viewer UI


- Stateless functions where possible.  
- Each operation has its own DTO + validator.  
- Image data handled as byte arrays (for test reproducibility).  
- Future-ready: pipeline could be parallelized or offloaded to GPU (v3+).

---

## 6. DevOps Integration (Planned)

| Area | Tool / Practice | Goal |
|------|-----------------|------|
| **CI/CD** | GitHub Actions | Build + test on push/PR. |
| **IaC** | Terraform (Week 3) | Deploy infra to Azure Storage + CDN. |
| **Release Mgmt** | GitHub Releases | Track versions via tags (v0.1, v0.2...). |
| **Branch Policy** | Protected `main`, Conventional Commits | Enforce quality & traceability. |
| **Artifacts** | Test snapshots, golden diffs | Debug failed tests visually. |

---

## 7. Design Decisions (ADR Summary)

| Decision | Option | Rationale |
|-----------|---------|-----------|
| **Language / Platform** | .NET 8 (C#) | Strong ecosystem, GUI-ready, good free tooling. |
| **Image Engine** | ImageSharp | Fully managed, MIT licensed, no native deps. |
| **UI Framework** | WPF (or MAUI) | Fast to prototype, native performance. |
| **Tests** | xUnit + Golden Files | Deterministic and visual regression validation. |
| **CI/CD** | GitHub Actions | Free tier + great integration. |
| **Infra as Code** | Terraform (v3) | Industry-standard for Azure IaC. |

---

## 8. Example File Tree
FreemiumPhotoEditor/
├── src/
│ ├── Editor.App/
│ │ ├── MainWindow.xaml
│ │ ├── Commands/
│ │ └── ViewModels/
│ └── Editor.Core/
│ ├── Operations/
│ ├── Models/
│ └── Services/
├── tests/
│ └── Editor.Core.Tests/
│ ├── Golden/
│ └── Unit/
├── docs/
│ ├── scope.md
│ └── arquitectura.md
└── .github/
├── ISSUE_TEMPLATE/
└── PULL_REQUEST_TEMPLATE.md


---

## 9. Scalability & Future Extensions

- Could be refactored to **micro-plugins** for new filters.  
- Potential **API layer** for cloud processing (Week 3+).  
- **Premium mode** integration simulated via UI + config flag.  
- Optional **telemetry + A/B testing** hooks (Week 4).

---

## 10. Summary

✅ Simple app → professional structure.  
✅ Strong testing discipline (golden/snapshot).  
✅ CI/CD-ready from day one.  
✅ Freemium concept = perfect excuse for DevOps storytelling.

> “From a silly photo editor to a full DevOps pipeline — showing that quality and process matter more than app complexity.”

