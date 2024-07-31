// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    $('.add-to-watchlist-btn').on('click', function(e) {
        e.preventDefault();

        var itemId = $(this).data('item-id');

        $.ajax({
            type: 'POST',
            url: '/api/Watchlist',
            contentType: 'application/json',
            data: JSON.stringify({ itemId: itemId }),
            success: function(response) {
                alert('Item added to watchlist successfully.');
                // Optionally, refresh the watchlist or update UI
            },
            error: function(xhr) {
                if (xhr.status === 401) {
                    alert(xhr.responseJSON.message);
                } else {
                    alert('An error occurred. Please try again.');
                }
            }
        });
    });
});
