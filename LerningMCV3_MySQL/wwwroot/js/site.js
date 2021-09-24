function hideButton(elmnt) {

    var buttonList = document.getElementsByName(elmnt.name);

    for (var i = 0; i < buttonList.length; i++) {

        if (i == 0) {

            buttonList[i].style.display = "none";
        } else {
            buttonList[i].style.display = "";
        }

    }

}
function showButton(elmnt) {

    var buttonList = document.getElementsByName(elmnt.name);
    // alert(elmnt.name);

    for (var i = 0; i < buttonList.length; i++) {

        if (i == 0) {

            buttonList[i].style.display = "";
        } else {
            buttonList[i].style.display = "none";
        }

    }


}

