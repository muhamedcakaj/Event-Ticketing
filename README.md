# **Event Social Network – ASP.NET Core MVC**  

Ky projekt është një rrjet social i fokusuar te eventet, i zhvilluar me **ASP.NET Core MVC**, **Razor Pages**, dhe **SQL Server** për backend-in.  

 🚀 **Karakteristikat Kryesore**  

### 🔐 **Autentifikimi & Siguria**  
- **Regjistrimi dhe hyrja** bëhen me **ASP.NET Identity**.  
- **Konfirmimi i emailit** është i detyrueshëm për të hyrë në sistem.  
- **Password-i ruhet i hash-uar** për siguri maksimale.  
- Të gjitha kërkesat kryhen mbi **HTTPS** për mbrojtje shtesë.  
- Aksesimi i faqes është i kufizuar me `[Authorize]`, që do të thotë se përdoruesit e paregjistruar nuk mund të shohin përmbajtjen.  

---

🌍 **Funksionalitetet Sociale**  

### 🏠 **Home**  
- Home shfaq **postimet dhe repostimet** e përdoruesve që ke ndjekur.  
- Mund të **pëlqesh (Like), ripostoshe (Repost), ruash (Save) dhe raportosh (Report)** çdo postim.  
- Çdo ndërveprim i dërgon një **njoftim** autorit të postimit.  

### 🔎 **Search**  
- Lejon kërkimin e **përdoruesve të tjerë** me emër ose username.  
- Klikimi mbi një përdorues të çon në **profilin e tij**.  

### 🔔 **Notifications**  
- Tregon një listë të **njoftimeve në kohë reale** për:  
  - **Pëlqime, ripostime dhe ruajtje** të postimeve.  
  - **Vizita të profilit nga përdorues të tjerë.**  
  - **Ndjekës të rinj (followers).**  
  - **Raportime të postimeve të tua.**  

### 👤 **Profile**  
- Tregon **postimet dhe repostimet** e përdoruesit.  
- Lejon **ndjekjen (Follow) ose heqjen nga ndjekja (Unfollow)** e përdoruesve të tjerë.  
- Mundëson **modifikimin ose fshirjen** e postimeve personale.  

### 💾 **Saved Posts**  
- Përdoruesi mund të shohë të gjitha **postimet që ka ruajtur** për më vonë.  

### ❤️ **Likes Activity**  
- Tregon historikun e **postimeve që ke pëlqyer**.  

### 👀 **Profile Viewer**  
- Shfaq një listë të **të gjithë përdoruesve që kanë vizituar profilin tënd**.  
- Çdo vizitë regjistrohet dhe i shfaqet si **njoftim përdoruesit të vizituar**.  

### ⚙️ **Privacy & Account Management**  
- Përdoruesit mund të:  
  - **Ndryshojnë informacionet personale**.  
  - **Fshijnë llogarinë e tyre** përfundimisht.  
  - **Vlerësojnë platformën me një rating**.  

---

## 🎛 **Admin Panel**  
- Vetëm përdoruesit me **rolin "Admin"** kanë akses në **Admin Area**.  
- Mund të menaxhojnë të gjitha entitetet me **CRUD Operations**.  
- Aplikohen politika autorizimi që ndajnë rolet **User** dhe **Admin**.  

---

## 🏗 **Arkitektura e Projektit**  
- **ASP.NET Core MVC** me **Razor Pages**.  
- **SQL Server** për ruajtjen e të dhënave.  
- **Model-View-Controller (MVC) Architecture**:  
  - **Model:** Definimi i entiteteve dhe krijimi i tabelave në database.  
  - **View:** Razor Pages për ndërfaqen e përdoruesit.  
  - **Controller:** Menaxhon të gjitha veprimet e faqes.
