document.addEventListener('DOMContentLoaded', function() {
    const addToWatchlistButtons = document.querySelectorAll('.add-to-watchlist');

    addToWatchlistButtons.forEach(button => {
        button.addEventListener('click', function(event) {
            event.preventDefault();
            const gearId = this.dataset.gearId;

            // Fetch to check if the user is authenticated
            fetch('/api/auth/check-authentication', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => response.json())
            .then(data => {
                if (data.isAuthenticated) {
                    // Get CSRF token
                    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

                    // Add to watchlist
                    fetch('/api/watchlist', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'X-CSRF-TOKEN': token
                        },
                        body: JSON.stringify({ itemId: gearId })
                    })
                    .then(response => {
                        if (response.ok) {
                            alert('Item added to watchlist successfully.');
                            // Optionally, you can redirect or update the UI here
                        } else {
                            response.json().then(data => {
                                alert(data.message || 'Failed to add item to watchlist');
                            });
                        }
                    });
                } else {
                    alert('Must be logged in to add items to watchlist');
                }
            })
            .catch(error => {
                console.error('Error checking authentication:', error);
                alert('An error occurred. Please try again.');
            });
        });
    });
});
