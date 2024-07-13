const counter = (elem) => {
    let currentCount = elem.textContent;
    elem.textContent = ++currentCount;
}