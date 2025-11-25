async function converter() {
    const json = document.getElementById("jsonInput").value;

    if (!json.trim()) {
        alert("A caixa de texto está vazia.");
        return;
    }

    const response = await fetch("/converter", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: json
    });

    if (!response.ok) {
        const err = await response.text();
        alert("Erro: " + err);
        return;
    }

    const csv = await response.text();
    document.getElementById("csvOutput").value = csv;
}

function limpar() {
    document.getElementById("jsonInput").value = "";
    document.getElementById("csvOutput").value = "";
}
