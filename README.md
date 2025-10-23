# 🖼️ Freemium Photo Editor

A sample project to demonstrate that even a basic app can have **professional-grade quality**.  
The focus isn’t photo editing itself, but rather the **infrastructure, best practices, and DevOps pipeline** behind it.

---

## 🚀 Goal

Build a **“Freemium” Photo Editor**:
- You can **open** and **edit** images.  
- But when you **save**, a *premium pop-up* appears (humorous yet polished).  
- Premium plans (per photo, monthly, annual) are simulated — they serve as a pretext to showcase architecture and deployment.

---

## 📅 Roadmap (DevOps version)

- Define scope (crop, rotate, brightness/contrast, B&W)
- Public repo, LICENSE, Conventional Commits, PR/Issue templates
- .NET project + minimal UI (open/view, undo/redo)
- Initial core tests (“golden files” / snapshots)
- README with brief architecture and a **pending demo GIF**

- Implement real editing operations (Core)
- Expose controls in the UI
- Visual tests and validations
- Local logs and metrics

- Premium pop-up + fake plan (polished UX)
- CI/CD (GitHub Actions → Azure)
- Basic Terraform (infrastructure as code)
- Image storage + CDN (simulated or free tier)

- Linter + static analysis
- Integration tests
- Full technical documentation
- GIFs, screenshots, badges, and a summary for the CV

---

## ⚙️ Architecture
Editor.App → Minimal UI (WPF/WinUI/.NET MAUI)
Editor.Core → Image engine (ImageSharp / free)
Editor.Tests → Golden files + unit tests

**Principles:**
- UI separated from core (SRP)
- Undo/Redo with limited history
- Reproducible tests (hash/bytes)
- DevOps-first: pipelines, conventions, docs

---

## 🧪 Tests (Week 1)
- “Golden files”: base and transformed images  
- Binary or hash-based comparison  
- Policy: only update golden files via PR tagged `test:update-snapshots`

---

## 🏗️ Conventions

| Type | Example |
|------|----------|
| Commit | `feat(core): add brightness adjustment` |
| Branch | `feat/ui-viewer`, `fix/undo-stack`, `chore/ci` |
| Labels | `type:feature`, `type:bug`, `area:core`, `area:ui`, `test`, `docs` |

---

## 📘 License
MIT — free use, modification, and distribution.

---

## 🧩 Pending GIF
*(Will be added at the end of Week 1 once the UI is functional)*

---

## 🧰 Badges (placeholder)
- [ ] CI: Build/Test  
- [ ] Codecov (Week 3)  
- [ ] License  
