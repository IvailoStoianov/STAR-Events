document.addEventListener("DOMContentLoaded", function () {
    const profilePicture = document.getElementById("profile-picture");
    const defaultImageUrl = "/images/default-pfp.svg";

    // Check if the initial src is empty and set to default image
    if (!profilePicture.src || profilePicture.src.trim() === "") {
        profilePicture.src = defaultImageUrl;
    }

    // Set the default image if the image fails to load
    profilePicture.onerror = function () {
        profilePicture.src = defaultImageUrl;
    };
});
