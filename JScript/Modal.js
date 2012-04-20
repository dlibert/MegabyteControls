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

    document.body.style.overflow = 'hidden';
}

function UnFreeze(divid) {
    document.body.removeChild(document.getElementById(divid));
}

function PleaseWait() {
    FreezeScreen();
    CreateWait();
}

function CreateWait() {
    DisplayBox("PleaseWaitDiv", "Saving", "<table width='100%'><tr><td style='vertical-align:middle'><img src='" + progressbarpath + "' alt='Progress bar, please wait...' align='ABSMIDDLE'  /></td><td style='vertical-align:middle;font-size:14px'>Please wait...</td></tr></table>", 150);
}

function DisplayBox(id, title, text, mboxwidth) {
    /*var myParent = document.getElementById('doc').parentNode;
    var width = (myParent.offsetWidth / 2) - (mboxwidth / 2);*/
    var height = 45;
    var left = GetCenterWidthPosition(mboxwidth);
    var top = GetCenterHeightPosition(height);
    creatediv(id, text, mboxwidth, height, left, top, "DialogWait", null, "#FFFFFF", "1px solid #25B9E9")
}

function GetCenterHeightPosition(divHeight) {
    return (GetScreenHeight() / 2) + getScrollPosition()[1] - (divHeight / 2);
}

function GetCenterWidthPosition(divWidth) {
    return (GetScreenWidth() / 2) + getScrollPosition()[0] - (divWidth / 2);
}

function GetScreenHeight() {
    return screen.availHeight;
}

function GetScreenWidth() {
    return screen.availWidth;
}

function getScrollPosition() {
    return Array((document.documentElement && document.documentElement.scrollLeft) || window.pageXOffset || self.pageXOffset || document.body.scrollLeft, (document.documentElement && document.documentElement.scrollTop) || window.pageYOffset || self.pageYOffset || document.body.scrollTop);
}

// Affiche un please wait en ajoutant tout les div au document
function PleaseWaitRT() {
    creatediv("ALLDOCUMENTDARK", "", null, null, getScrollPosition()[0], getScrollPosition()[1], "modalBackground", "visible", null, null);
    DisplayBox("PleaseWaitRTDiv", "Please wait", '<table width=\'100%\'><tr><td style=\'vertical-align:middle\'><img src=\'<%=WebResource("Megabyte.Web.Controls.Images.loading_white_40x40.gif")%>\' alt=\'Progress bar, please wait...\' align=\'ABSMIDDLE\'  /></td><td style=\'vertical-align:middle;font-size:14px\'>Please wait...</td></tr></table>', 150);
    document.body.style.overflow = 'hidden';
}

function UnPleaseWaitRT() {
    UnFreeze('ALLDOCUMENTDARK');
    UnFreeze('PleaseWaitRTDiv');
    document.body.style.overflow = '';
}

function creatediv(id, html, width, height, left, top, cssclass, visibility, background, border) {

    var newdiv = document.createElement('div');
    newdiv.setAttribute('id', id);


    if (cssclass)
        newdiv.className = cssclass;

    if (width) {
        newdiv.style.width = width + "px";
    }

    if (height) {
        newdiv.style.height = height + "px";
    }

    if ((left || top) || (left && top)) {
        newdiv.style.position = "absolute";

        if (left) {
            newdiv.style.left = left + "px";
        }

        if (top) {
            newdiv.style.top = top + "px";
        }
    }

    if (visibility)
        newdiv.style.visibility = visibility;

    if (background)
        newdiv.style.background = background;
    if (border)
        newdiv.style.border = border;

    if (html) {
        newdiv.innerHTML = html;
    } else {
        newdiv.innerHTML = "";
    }

    document.body.appendChild(newdiv);
}