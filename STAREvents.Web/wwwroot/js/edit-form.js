document.getElementById('profilePictureFile').addEventListener('change', function (event) {
    var file = event.target.files[0];
    var maxSize = 100 * 1024; // 100 KB
    var fileSizeError = document.getElementById('fileSizeError');

    if (file) {
        if (file.size > maxSize) {
            fileSizeError.textContent = "File size exceeds 100 KB.";
            event.target.value = ""; // Clear the file input
        } else {
            fileSizeError.textContent = "";
            var reader = new FileReader();
            reader.onload = function (e) {
                var base64String = e.target.result.split(',')[1];
                document.getElementById('profilePicture').value = base64String;
            };
            reader.readAsDataURL(file);
        }
    }
});