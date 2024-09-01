$(function () {
    function openModal() {
        $('#myModal').fadeIn();
        $('#myModal').css('display', 'flex');
    }

    $('.add-btn').click(function () {
        openModal();
    });

    $('.close').click(function () {
        $('#myModal').fadeOut();
    });
})
