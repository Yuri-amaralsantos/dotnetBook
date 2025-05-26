# 🗨️ **BookRatingSite**  

Um site de avaliação de livros com frontend Vue.js, backend dotnet core, autenticação JWT, e PostgreSQL.  

---

### 🚀 **Funcionalidades**  

- ✅ Autenticação de Usuários (Login/Registro com JWT & PostgreSQL)  
- ✅ Sistema para adicionar livros favoritos do usuário 
- ✅ API Segura (dotnet core + PostgreSQL)  
- ✅ Sistema de gerenciamento de avaliação de livro com nota e comentário
- ✅ Sistema de cargos de adminstração com funções de remover comentário e editar livros
- ✅ Interface Simples & Responsiva  

---

## 🛠️ **Como Instalar**

### 1️⃣ **Clonar o Repositório**

git clone https://github.com/Yuri-amaralsantos/bookRatingNetCore.git

---

### 2️⃣ **Configurar o Backend**

cd JwtAuthApi
dotnet restore

#### **Alterar o connectionStrings no appsettings.json:**

"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=bookDb;Username={usuário};Password={senha}"
},

#### **Iniciar o backend:**

dotnet run

---

### 3️⃣ **Configurar o Frontend**

cd front
npm run dev

---

## 🏗️ **Tecnologias Utilizadas**

Frontend: HTML, CSS, JavaScript, Vue.js, Axios,
Backend: Dotnet, swagger, JWT
Banco de Dados: PostgreSQL


---

## 🤝 **Contribuição**

Contribuições são bem-vindas! Sinta-se à vontade para enviar issues e pull requests.

---

## 📜 **Licença**

Este projeto está licenciado sob a Licença MIT
