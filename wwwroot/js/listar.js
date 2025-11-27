function toggleDropdown(btn) {
    var box = btn.parentElement.nextElementSibling;
    if (box.classList.contains('open')) {
        box.classList.remove('open');
    } else {
        box.classList.add('open');
    }
}