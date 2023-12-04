document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const searchResultsContainer = document.getElementById('searchResults');

    searchInput.addEventListener('input', function (event) {
        const searchTerm = event.target.value;

        if (searchTerm.length === 0) {
            searchResultsContainer.innerHTML = '';
            searchResultsContainer.style.display = 'none'; // Hide the dropdown if search term is empty
            return;
        }

        fetch(`/Professor/search?term=${encodeURIComponent(searchTerm)}`)
            .then(response => response.json())
            .then(data => {
                searchResultsContainer.innerHTML = '';
                if (data.length > 0) {
                    searchResultsContainer.style.display = 'block'; // Show the dropdown if there are results
                    data.forEach(professor => {
                        let div = document.createElement('div');
                        div.textContent = `${professor.firstName} ${professor.lastName}`; // Customize as needed
                        div.classList.add('search-result-item');
                        div.addEventListener('click', () => {
                            window.location.href = `/Professors?professorId=${professor.id}`; // Redirect on click
                        });
                        searchResultsContainer.appendChild(div);
                    });
                } else {
                    searchResultsContainer.style.display = 'none'; // Hide the dropdown if no results
                }
            })
            .catch(error => {
                console.error('Error:', error);
                searchResultsContainer.style.display = 'none';
            });
    });

    // Hide the dropdown when clicking outside
    document.addEventListener('click', function (event) {
        if (!searchInput.contains(event.target) && !searchResultsContainer.contains(event.target)) {
            searchResultsContainer.style.display = 'none';
        }
    });

    // Search Bar animation
    const rollingText = document.querySelector('.rolling-text');
    const words = ["professora", "profesor", "professora", "profesor", "enseignant", "docente", "lehrer", "insegnante", "pedagogo"];
    let currentIndex = 0;

    function startSlideAnimation() {
        let top = 0;
        let step = 0.03;

        function animate() {
            rollingText.style.top = `${top * -4}rem`;
            top += step;
            if (top > 4) {
                top = 0;
                rollingText.innerHTML = `<b>${words[currentIndex]}</b>`;
                currentIndex = (currentIndex + 1) % words.length;
            }
            requestAnimationFrame(animate);
        }

        animate();
    }

    window.addEventListener('load', startSlideAnimation);
});
