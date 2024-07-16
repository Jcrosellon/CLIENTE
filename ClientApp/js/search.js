document.getElementById('searchButton').addEventListener('click', () => {
    const keyword = document.getElementById('searchInput').value.trim();

    if (keyword !== '') {
        const isNit = /^\d+$/.test(keyword);
        let apiUrl = `http://localhost:5139/api/clientes/search`;

        if (isNit) {
            apiUrl += `?keyword=${encodeURIComponent(keyword)}`;
        } else {
            apiUrl += `?keyword=${encodeURIComponent(keyword)}`;
        }

        fetch(apiUrl)
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
                console.error('Fetch error:', error);
                alert('Hubo un problema al buscar el pedido. Por favor, intenta de nuevo más tarde.');
            });
    } else {
        alert('Por favor ingresa un nombre o NIT válido.');
    }
});
