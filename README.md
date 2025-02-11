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
 










![1](https://github.com/user-attachments/assets/10810017-8551-4b1c-9a66-8c876a6d226d)
![2](https://github.com/user-attachments/assets/6bdd7e82-6381-4ddf-b6d5-c45cd57122d1)
![3](https://github.com/user-attachments/assets/5a917276-1c35-4b3a-9622-8ff5f04a7ab2)
![4](https://github.com/user-attachments/assets/8de98e0c-9814-4075-b800-4d2e9f420849)
![5](https://github.com/user-attachments/assets/f05d0695-6b69-4dc0-956d-4e6c99104e3b)
![6](https://github.com/user-attachments/assets/1d5a9d42-6b0c-42c4-a9eb-07b40644be2b)
![7](https://github.com/user-attachments/assets/f87bc20a-c2e2-4d3b-af0a-52b4d52a2d25)
![8](https://github.com/user-attachments/assets/5a5899af-f10f-4b05-a6fe-b55c28e575ad)
![9](https://github.com/user-attachments/assets/0056b60b-c5a9-4c59-bce7-248b270e8444)
![10](https://github.com/user-attachments/assets/51ee3df7-8f03-4a9e-9fe0-67b2e216b03f)
![11](https://github.com/user-attachments/assets/91995ad9-457a-44ac-bb83-8308d34d8f1c)
![12](https://github.com/user-attachments/assets/4dec8378-f9b8-4a65-8dab-ce902b861243)
![13](https://github.com/user-attachments/assets/52757440-a116-44e9-ae10-b3c459dffd36)
![14](https://github.com/user-attachments/assets/65bfbb48-bedd-499a-9ed9-3a197c18239f)
![15](https://github.com/user-attachments/assets/036cbaee-d045-456c-ae18-481a87bd4d0a)














