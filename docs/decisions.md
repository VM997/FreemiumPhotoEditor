# 🧠 Architecture Decision Records (ADRs)

**Project:** Freemium Photo Editor
**Owner:** Michael Villasfer
**Goal:** Capture the main architectural and DevOps decisions made during development, with clear reasoning and alternatives considered.

---

## ADR-001 — Language & Framework

**Decision:** Use **.NET 8 (C#)** for both UI and Core.  
**Date:** Week 1  

### Context
The MVP requires a native desktop UI, good image processing support, and free tooling.

### Options Considered
- Python (PyQT / Tkinter / Pillow)
- Electron (JS/TS + Node)
- .NET (WPF / MAUI / WinUI)

### Decision
✅ Chose **.NET** due to:
- Mature image libraries (ImageSharp).
- Strong ecosystem for Windows desktop apps.
- Free tooling (Visual Studio / VS Code + SDK).
- Easy CI integration with GitHub Actions.
- Same language for UI + Core + Tests.

### Consequences
- Excellent maintainability and type safety.
- Cross-platform flexibility (future MAUI port).
- Slightly higher binary size (acceptable trade-off).

---

## ADR-002 — Image Processing Engine

**Decision:** Use **ImageSharp** library (open source).  
**Date:** Week 1  

### Context
Need deterministic, cross-platform image operations without native DLLs.

### Options
- System.Drawing (deprecated in .NET 6+)
- OpenCVSharp (powerful but heavy)
- ImageSharp (lightweight, MIT license)

### Decision
✅ ImageSharp chosen because:
- Pure C#, no native dependencies.
- Well-maintained and cross-platform.
- Deterministic output ideal for “golden” tests.
- MIT license (safe for public repo).

### Consequences
- Slightly slower than native libs — acceptable for MVP.
- Easy to swap in future (e.g., SkiaSharp or GPU backend).

---

## ADR-003 — Testing Strategy

**Decision:** Use **Golden Files / Snapshot Testing** for Core.  
**Date:** Week 1  

### Context
Visual operations are hard to verify via numeric assertions.

### Decision
✅ Compare resulting image bytes (hash) against reference “golden” files.  
✅ Store under `tests/_golden/`.

### Benefits
- Immediate visual regression detection.
- Works well in CI (byte-level comparison).
- Transparent updates via PR labels (`test:update-snapshots`).

### Trade-offs
- Golden files must be small (size control).
- Determinism required (same library version).

---

## ADR-004 — Repository & Workflow

**Decision:** Use **GitHub Public Repo + Conventional Commits + PR templates**.  
**Date:** Week 1  

### Context
Project serves as a DevOps showcase.

### Decision
✅ GitHub chosen due to:
- Free CI/CD integration.
- Visibility for recruiters.
- Built-in templates & issue management.

### Rules
- Branches: `feat/*`, `fix/*`, `chore/*`, `docs/*`.
- PRs require passing tests + 1 review.
- Commit style: `type(scope): message`.

### Benefits
- Clean history and traceability.
- Auto-changelog possible.
- CI badges directly on README.

---

## ADR-005 — Continuous Integration (CI)

**Decision:** Implement GitHub Actions for build & test.  
**Date:** Week 1  

### Pipeline
1. Restore + build solution.  
2. Run all tests (unit + golden).  
3. Upload diffs as artifacts if snapshots fail.  

### Goals
- Ensure every commit is stable.  
- Reproducible environment (Windows + .NET SDK).  

### Benefits
- Fully free tier.
- Fast feedback.
- Easy extension to Terraform deploy later.

---

## ADR-006 — Infrastructure as Code (planned)

**Decision:** Use **Terraform** (v3 roadmap).  
**Date:** Week 3  

### Context
Future steps will deploy the web/premium version (static web app + CDN).

### Alternatives
- Azure ARM templates
- Bicep
- Terraform

### Decision
✅ Terraform chosen because:
- Portable (works with multiple clouds).
- Enterprise standard.
- Free and declarative.

### Benefits
- Realistic DevOps portfolio piece.
- Enables automated environment provisioning.

---

## ADR-007 — Licensing & Public Visibility

**Decision:** Use **MIT License** and keep repo public.  
**Date:** Week 1  

### Rationale
- Encourages open collaboration and reuse.
- Safe for portfolio purposes.
- Compatible with all dependencies.

### Consequence
- Anyone can use/modify the code — intended and acceptable.

---

## ADR-008 — Scope Control

**Decision:** Limit MVP to “Viewer + Undo/Redo”.  
**Date:** Week 1  

### Rationale
- Focus on structure, not features.
- Prevent scope creep.
- Allow quick CI setup and test-driven iteration.

### Benefit
- A small but complete, testable product baseline.

---

## ADR-009 — Future DevOps Enhancements

**Planned**
- Week 2: Implement operations fully.
- Week 3: Add fake premium popup + Terraform deploy.
- Week 4: Add documentation, code coverage, GIFs, and dashboards.

> Each new milestone will include an updated ADR section summarizing changes.
