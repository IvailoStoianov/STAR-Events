document.getElementById('profilePictureFile').addEventListener('change', function (event) {
    var file = event.target.files[0];
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('profile-picture').src = e.target.result;
            document.getElementById('profilePictureUrl').value = e.target.result;
        };
        reader.readAsDataURL(file);
    }