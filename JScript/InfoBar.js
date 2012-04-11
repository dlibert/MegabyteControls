function InfoBarDisplayText(clientid, text,rendertype) {
    var elt = document.getElementById(clientid);
    elt.setAttribute("class", rendertype);
    elt.style.display = 'block';   
    elt.getElementsByTagName("p")[0].innerHTML = text;
}