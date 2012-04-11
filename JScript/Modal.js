function FreezeScreen() {
    var element = 'grayScreen';
    var myParent = document.getElementById('doc').parentNode;
    var sTop = myParent.scrollTop; //Hauteur de défilement de l'élément parent
    var sLeft = myParent.scrollLeft; //Longueur de défilement de l'élément parent
    var wHeight = myParent.scrollHeight; // Hauteur de l'ecran complet
    var pHeight = myParent.offsetHeight; //Hauteur de l'élément parent
    var pWidth = myParent.offsetWidth; //Largeur de l'élément parent
    document.getElementById('doc').style.overflow = 'hidden'; // Hide scrollbars
    document.getElementById(element).style.height = (wHeight > pHeight) ? wHeight + 'px' : pHeight + 'px';
    document.getElementById(element).style.width = pWidth + 'px';
    document.getElementById(element).style.visibility = 'visible';
}

function UnFreeze() {
    // Si un message box est generé par le serveur on ne dégrise pas.    
    document.getElementById('grayScreen').style.visibility = 'Hidden';
    document.getElementById('grayScreen').style.height = '0px';
    document.getElementById('grayScreen').style.width = '0px';

    var mbox = document.getElementById('PleaseWaitDiv');

    if (mbox)
        document.body.removeChild(mbox);
}

function PleaseWait() {
    FreezeScreen();
    DisplayBox("Saving", "<table width='100%'><tr><td style='vertical-align:middle'><img src='" + progressbarpath + "' alt='Progress bar, please wait...' align='ABSMIDDLE'  /></td><td style='vertical-align:middle;font-size:14px'>Please wait...</td></tr></table>");
}

function DisplayBox(title, text) {
    var mboxwidth = 150;
    var myParent = document.getElementById('doc').parentNode;
    var width = (myParent.offsetWidth / 2) - (mboxwidth / 2);
    var element = creatediv("PleaseWaitDiv", text, mboxwidth, 45, width, 150)    
}

function creatediv(id, html, width, height, left, top) {

    var newdiv = document.createElement('div');
    newdiv.setAttribute('id', id);

    newdiv.style.zIndex = 99;

    if (width) {
        newdiv.style.width = width +"px";
    }

    if (height) {
        newdiv.style.height = height+"px";
    }

    if ((left || top) || (left && top)) {
        newdiv.style.position = "absolute";

        if (left) {
            newdiv.style.left = left +"px";
        }

        if (top) {
            newdiv.style.top = top +"px";
        }
    }

    newdiv.style.background = "#FFFFFF";
    newdiv.style.border = "1px solid #25B9E9";

    if (html) {
        newdiv.innerHTML = html;
    } else {
        newdiv.innerHTML = "";
    }

    document.body.appendChild(newdiv);
}

var progressbarpath;