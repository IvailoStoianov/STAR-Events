function checkBodyHeight() {
    const body = document.body;
    const footer = document.querySelector('footer');
    const html = document.documentElement;

    const bodyHeight = Math.max(body.scrollHeight, body.offsetHeight,
        html.clientHeight, html.scrollHeight, html.offsetHeight);

    const viewportHeight = window.innerHeight;

    if (bodyHeight <= viewportHeight) {
        footer.classList.add("not-full-height");
    } else {
        footer.classList.remove("not-full-height");
    }
}

document.addEventListener("DOMContentLoaded", checkBodyHeight);
window.addEventListener("resize", checkBodyHeight);