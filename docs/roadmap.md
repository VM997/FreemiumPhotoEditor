# 🗺️ Project Roadmap — Editor de Fotos Freemium

**Owner:** Mikey  
**Purpose:** Define sprint goals, milestones, and expected deliverables for each week.  
**Duration:** 4 weeks (flexible mode: 2–3 months part-time).  
**Focus:** Demonstrate DevOps excellence through a small but complete software lifecycle.

---

## 🧭 Overview

| Phase | Theme | Focus | Status |
|--------|--------|--------|--------|
| Week 1 | **Base & MVP Viewer** | Foundation, repo setup, basic image viewer | ✅ In Progress |
| Week 2 | **Editor Core** | Implement image operations (crop, rotate, adjust, B&W) | ⏳ Planned |
| Week 3 | **Freemium + DevOps Infra** | Premium popup, CI/CD, Terraform, Azure deploy | ⏳ Planned |
| Week 4 | **Polish & Documentation** | Quality, coverage, GIFs, final presentation | ⏳ Planned |

---

## 🧩 Week 1 — MVP “Viewer” (Base Layer)

**Goal:** Build the base of the app with full repo hygiene.

**Deliverables**
- Public GitHub repo with:
  - LICENSE, README, templates, labels
  - `.NET` solution: `App`, `Core`, `Tests`
  - Minimal UI to open and view an image
  - Undo/Redo system (in-memory)
  - Golden file test setup
- CI pipeline (GitHub Actions: build + test)
- Docs: `scope.md`, `architecture.md`, `decisions.md`, `roadmap.md`

**Milestone tag:** `v0.1.0 – MVP Viewer`

---

## 🎨 Week 2 — Editor Core (Feature Layer)

**Goal:** Implement the real image operations and integrate with the UI.

**Deliverables**
- Core operations implemented and unit tested:
  - Crop
  - Rotate
  - Brightness / Contrast
  - Black & White
- Validation + error handling in `Editor.Core`
- Update UI with control elements (sliders, buttons)
- Test coverage for each operation
- Extended golden files (1 per operation)
- Add *logging* (simple local logger)

**Milestone tag:** `v0.2.0 – Functional Editor`

---

## 💰 Week 3 — Freemium + DevOps (Cloud & CI/CD Layer)

**Goal:** Add the “freemium” fun element and demonstrate full DevOps pipeline.

**Deliverables**
- “Premium Popup” implemented (mocked payment plans)
- Environment variables for fake plans (per-photo, monthly, annual)
- GitHub Actions CI extended to:
  - Lint + test + build
  - Package & deploy to Azure (Static Web App or App Service)
- Terraform scripts (`/infra/terraform`)
  - Create Resource Group, Storage Account, CDN endpoint
- Basic monitoring (App Insights or logging stub)
- README badges: build, license, version

**Milestone tag:** `v0.3.0 – Freemium Cloud Demo`

---

## 🧽 Week 4 — Polishing & Presentation (Quality Layer)

**Goal:** Make it portfolio-ready.

**Deliverables**
- Code cleanup, consistent naming, docs updated
- Unit + integration tests passing (90%+)
- Code coverage via Coverlet / ReportGenerator
- Visual GIF of app in README
- Architecture diagram (PNG/SVG)
- Public GitHub release notes
- Optional: LinkedIn post with summary + link

**Milestone tag:** `v1.0.0 – Public Showcase`

---

## 🧱 Bonus (Optional Future Work)

| Area | Idea | Description |
|-------|------|-------------|
| Cloud Storage | Azure Blob / CDN | Host edited images (temporary). |
| API Gateway | ASP.NET Minimal API | Expose editing operations via REST. |
| Auth | Mock OAuth2 | For premium login flow. |
| Telemetry | Application Insights | Track fake premium usage metrics. |
| Cross-Platform | .NET MAUI | Bring editor to macOS/Linux. |

---

## ⚙️ DevOps Lifecycle Overview

Plan → Code → Build → Test → Release → Deploy → Monitor → Improve


| Stage | Tool | Outcome |
|--------|------|----------|
| **Plan** | GitHub Issues / Milestones | Scope & tasks tracking |
| **Code** | .NET 8 + ImageSharp | Modular core |
| **Build/Test** | GitHub Actions | CI validation |
| **Release** | Tags + GitHub Releases | Versioned outputs |
| **Deploy** | Terraform + Azure | Infra as code |
| **Monitor** | Logs / Telemetry (v4+) | Observability |
| **Improve** | ADR updates | Continuous improvement |

---

## 📅 Timeline Summary

| Week | Milestone | Deliverable |
|------|------------|-------------|
| 1 | MVP Viewer | Base repo + CI + docs |
| 2 | Editor Core | Functional features |
| 3 | Freemium + Infra | Cloud + Terraform |
| 4 | Polish & Docs | Public-ready product |

---

> _"From a simple viewer to a full DevOps showcase — proving that engineering quality is independent of project complexity."_
