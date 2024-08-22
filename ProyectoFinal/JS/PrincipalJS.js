//JS DEL POPOVER
document.addEventListener('DOMContentLoaded', function () {
    // Initialize popover
    var popoverTriggerEl = document.getElementById('popover-trigger');
    var popover = new bootstrap.Popover(popoverTriggerEl, {
        html: true,
        content: function () {
            return document.getElementById('popover-content').innerHTML;
        },
        placement: 'bottom',
        trigger: 'click'
    });
});