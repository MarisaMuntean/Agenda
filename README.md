# Agenda

<img width="868" height="563" alt="image" src="https://github.com/user-attachments/assets/711f468c-c91a-407f-9659-12eceb96f545" />

**Agenda** is a C# desktop application for managing personal or team meetings and events.  
It allows users to create, view, edit, and delete agenda items, ensuring a structured and conflict-free schedule.

---

## 🧭 Overview

This project demonstrates a simple but functional scheduling system built in C#.  
It supports core agenda management features (CRUD operations, validation, and data persistence) and serves as a foundation for more complex calendar or meeting systems.

---

## ⚙️ Features

- 📝 **Add** new agenda items with title, description, date, start and end times  
- 📅 **View** all items or filter by specific dates  
- ✏️ **Edit** existing meetings or events  
- ❌ **Delete** items from the agenda  
- 🕓 **Validation** — prevents overlapping meetings and ensures valid time ranges  
- 💾 **Data persistence** — stores agenda data locally (file or database, depending on configuration)  
- 🔍 **Search / filter** — view meetings by keyword or time period (if implemented)  

---

## 🧩 Architecture & Technologies

- **Language:** C# (.NET)  
- **Architecture:** Layered structure separating domain, data access, and UI  
- **Core Components:**
  - `AgendaItem` model — defines meeting properties  
  - Repository interfaces — handle CRUD logic  
  - Validation layer — enforces time and overlap rules  
  - UI layer — provides console or GUI interaction  
