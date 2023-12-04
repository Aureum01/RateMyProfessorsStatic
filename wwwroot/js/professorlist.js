document.addEventListener('DOMContentLoaded', function () {
    var modal = document.getElementById("professorModal");
    var span = document.getElementsByClassName("close")[0];

    document.body.addEventListener('click', function (event) {
        if (event.target && event.target.matches(".rate-button")) {
            var professorId = event.target.getAttribute('data-professor-id');

            // Create an XMLHttpRequest object
            var xhr = new XMLHttpRequest();
            xhr.open("GET", '/Home/OpenModal', true);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            xhr.onload = function () {
                if (xhr.status === 200) {
                    // Populate the modal with the response and show it
                    modal.querySelector('.modal-content').innerHTML = xhr.responseText;
                    modal.style.display = "block";
                } else {
                    alert('Failed to load professor data.');
                }
            };

            xhr.onerror = function () {
                alert('Failed to load professor data.');
            };

            xhr.send("professorId=" + encodeURIComponent(professorId));

        } else if (event.target === span) {
            modal.style.display = "none";
        } else if (event.target === modal) {
            modal.style.display = "none";
        }
    });
});





// Close modal functionality
document.querySelector("#professorModal .close").onclick = function () {
    document.getElementById("professorModal").style.display = 'none';
};


