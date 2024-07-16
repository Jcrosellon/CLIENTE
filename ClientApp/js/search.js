document.getElementById('searchButton').addEventListener('click', () => {
    const keyword = document.getElementById('searchInput').value.trim();

    if (keyword !== '') {
        // Determinar si el input es un NIT (solo números) o un nombre (texto)
        const isNit = /^\d+$/.test(keyword);

        let apiUrl = `http://localhost:5139/api/clientes/search`;

        // Construir la URL de la solicitud dependiendo de si es NIT o nombre
        if (isNit) {
            apiUrl += `?nit=${encodeURIComponent(keyword)}`;
        } else {
            apiUrl += `?nombre=${encodeURIComponent(keyword)}`;
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
