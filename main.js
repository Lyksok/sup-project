async function injectHTML(filePath) {
    
    const response = await fetch(filePath)
    .then(response => response.text())
    .then(text => document.querySelector(".main").innerHTML = text)
}

injectHTML("./pages/accueil.html");