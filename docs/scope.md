# 🎯 Project Scope — Week 1 (MVP “Viewer”)

**Project:** Freemium Photo Editor
**Owner:** Michael Villasfer
**Purpose:** Demonstrate a professional, production-ready DevOps workflow using a deliberately simple application — a “freemium” photo editor.

---

## 1. MVP Description

The MVP focuses on **viewing** and **basic editing** of images (PNG, JPG).  
The “Save” and “Premium” flows are **mocked** — they will appear in later phases (Week 3+).  

This week’s deliverable:  
✅ Open and display images.  
✅ Support undo/redo (in-memory).  
✅ Core engine operations defined and partially tested.  
✅ Documentation, CI, and basic testing pipeline.

---

## 2. Editing Features (defined but minimal implementation)

| Feature  |  Description | Parameters | Validation / Notes |
|----------|--------------|-------------|--------------------|
| **Crop** | Cut a rectangular region of the image. | `x`, `y`, `width`, `height` (pixels) | Must stay within image bounds. Negative values ignored. |
| **Rotate** | Rotate image clockwise. | `degrees` (90 / 180 / 270 / custom float) | If not multiple of 90, auto-expand canvas to fit. |
| **Brightness / Contrast** | Adjust image tone and contrast. | `brightness` ∈ [-100, +100]; `contrast` ∈ [-100, +100] | Defaults = 0 (no change). Use normalized scale. |
| **Black & White** | Convert to grayscale. | `intensity` ∈ [0, 100]; `algorithm`: `luminosity` \| `BT.709` | 100 = full B&W; 0 = original color. |

---

## 3. Technical Constraints

|       Area      | Rule |
|-----------------|--------------------------------------------------------------|
| **Image Size**  | Max 4096×4096 px (prevent memory overflow).                  |
| **File Types**  | PNG, JPG, JPEG.                                              |
| **Engine**      | .NET + ImageSharp (open-source and cross-platform).          |
| **Undo/Redo**   | Max stack size = 10. Beyond that, oldest states are dropped. |
| **Performance** | Basic operations should complete < 1 second for 1080p image. |
| **Storage**     | In-memory only (no save/export yet).                         |
| **UI Platform** | .NET (WPF or MAUI) — single-window minimal interface.        |

---

## 4. Out of Scope (Week 1)

❌ Save / Export  
❌ Premium popup  
❌ Theming, localization  
❌ Analytics or telemetry  
❌ Cloud integration or deployment (covered in later weeks)

---

## 5. Architecture Summary
src/
├── Editor.App → UI layer (open, visualize, trigger commands)
├── Editor.Core → Image engine (pure logic)
└── tests/Editor.Core.Tests → Golden file tests & core unit tests

**Flow:**
User → UI → Core.Apply(Operation) → Updated image → Undo/Redo stack

---

## 6. Testing Approach

- **Golden Files**: expected outputs stored under `tests/_golden/`.
- **Comparison Method**: byte array or hash equality.
- **Policy**: golden updates only allowed in PRs labeled `test:update-snapshots`.
- **Example test**: rotate 90°, crop center, brightness +20 → compare with golden.

---

## 7. Deliverables for Week 1

- [x] Public GitHub repo (`editor-fotos-freemium`)  
- [x] LICENSE (MIT)  
- [x] README.md and templates (PR, Issues)  
- [x] Core engine with operation definitions  
- [x] Basic viewer (open image + undo/redo)  
- [x] Golden file tests running in CI  
- [x] Docs folder (scope + architecture)  

---

## 8. Future Extensions (for later sprints)

- Save & export (Week 2)
- Premium popup with subscription mock (Week 3)
- CI/CD + Azure deployment + Terraform IaC (Week 3)
- Unit + integration + coverage (Week 4)
- Screenshots, GIFs, and public portfolio polish
