const API_BASE = "http://localhost:5235";

document.addEventListener("DOMContentLoaded", () => {
    const formRegistro = document.getElementById("formRegistro");
    const formInicio = document.getElementById("formInicio");

    if (formRegistro) {
        formRegistro.addEventListener("submit", (e) => {
            e.preventDefault();
            registrar();
        });
    }

    if (formInicio) {
        formInicio.addEventListener("submit", (e) => {
            e.preventDefault();
            iniciarSesion();
        });
    }

    configurarModal();
    mostrarCuenta();
});

async function registrar() {
    const nombre = document.getElementById("regNombre")?.value.trim();
    const correo = document.getElementById("regCorreo")?.value.trim();
    const telefono = document.getElementById("regTelefono")?.value.trim();
    const password = document.getElementById("regPassword")?.value.trim();

    if (!nombre || !correo || !telefono || !password) {
        alert("Completa todos los campos.");
        return;
    }

    try {
        const r = await fetch(`${API_BASE}/api/Usuarios/registro`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                nombreCompleto: nombre,
                correo,
                telefono,
                password
            })
        });

        const data = await r.json().catch(() => ({}));

        if (!r.ok) {
            alert(data.mensaje || "Error al registrar.");
            return;
        }

        alert("Registro exitoso.");
        mostrarLogin();
    } catch {
        alert("No se pudo procesar el registro.");
    }
}

async function iniciarSesion() {
    const correo = document.getElementById("loginCorreo")?.value.trim();
    const password = document.getElementById("loginPassword")?.value.trim();

    if (!correo || !password) {
        alert("Ingresa tus datos.");
        return;
    }

    try {
        const r = await fetch(`${API_BASE}/api/Usuarios/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ correo, password })
        });

        const data = await r.json().catch(() => ({}));

        if (!r.ok) {
            alert(data.mensaje || "Credenciales incorrectas.");
            return;
        }

        if (data.usuario) {
            localStorage.setItem("usuario", JSON.stringify(data.usuario));
        }

        mostrarCuenta();
        cerrarModal();
        alert("Sesión iniciada.");
    } catch {
        alert("No se pudo iniciar sesión.");
    }
}

function mostrarCuenta() {
    const panel = document.getElementById("panelCuentaUsuario");
    const vacio = document.getElementById("panelCuentaVacio");
    const nombreSpan = document.getElementById("cuentaNombre");
    const correoSpan = document.getElementById("cuentaCorreo");
    const telefonoSpan = document.getElementById("cuentaTelefono");

    if (!panel || !vacio || !nombreSpan || !correoSpan || !telefonoSpan) return;

    const u = localStorage.getItem("usuario");
    if (!u) {
        panel.style.display = "none";
        vacio.style.display = "block";
        return;
    }

    const usuario = JSON.parse(u);
    nombreSpan.textContent = usuario.nombreCompleto || "";
    correoSpan.textContent = usuario.correo || "";
    telefonoSpan.textContent = usuario.telefono || "";

    panel.style.display = "block";
    vacio.style.display = "none";
}

function cerrarSesion() {
    localStorage.removeItem("usuario");
    mostrarCuenta();
}

function configurarModal() {
    const modal = document.getElementById("modal");
    const btnLogin = document.getElementById("btnAbrirLogin");
    const btnRegistro = document.getElementById("btnAbrirRegistro");
    const btnHeroLogin = document.getElementById("btnHeroLogin");
    const cerrar = document.getElementById("cerrarModal");
    const contLogin = document.getElementById("contenedorLogin");
    const contRegistro = document.getElementById("contenedorRegistro");
    const linkIrRegistro = document.getElementById("linkIrRegistro");
    const linkIrLogin = document.getElementById("linkIrLogin");

    if (!modal || !contLogin || !contRegistro) return;

    function abrirLogin() {
        contLogin.classList.remove("oculto");
        contRegistro.classList.add("oculto");
        modal.style.display = "flex";
    }

    function abrirRegistro() {
        contRegistro.classList.remove("oculto");
        contLogin.classList.add("oculto");
        modal.style.display = "flex";
    }

    if (btnLogin) btnLogin.addEventListener("click", abrirLogin);
    if (btnHeroLogin) btnHeroLogin.addEventListener("click", abrirLogin);
    if (btnRegistro) btnRegistro.addEventListener("click", abrirRegistro);
    if (linkIrRegistro) linkIrRegistro.addEventListener("click", abrirRegistro);
    if (linkIrLogin) linkIrLogin.addEventListener("click", abrirLogin);

    if (cerrar) {
        cerrar.addEventListener("click", () => {
            modal.style.display = "none";
        });
    }

    window.addEventListener("click", (e) => {
        if (e.target === modal) {
            modal.style.display = "none";
        }
    });

    window.mostrarLogin = abrirLogin;
}

function cerrarModal() {
    const modal = document.getElementById("modal");
    if (modal) modal.style.display = "none";
}
