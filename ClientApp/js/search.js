document.getElementById('searchButton').addEventListener('click', () => {
    const keyword = document.getElementById('searchInput').value.trim();

    if (keyword !== '') {
        fetch(`http://localhost:5139/api/clientes/search/?keyword=${encodeURIComponent(keyword)}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                if (data.length > 0) {
                    window.location.href = `results.html?search=${encodeURIComponent(keyword)}`;
                } else {
                    alert('No se encontraron resultados.');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Hubo un problema al buscar el pedido. Por favor, intenta de nuevo más tarde.');
            });
    } else {
        alert('Por favor ingresa un nombre o NIT válido.');
    }
});
