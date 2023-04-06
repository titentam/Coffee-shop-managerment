// get combobox element
const myComboBox = document.getElementById("myComboBox");

function getSelectedOption() {
    //myComboBox.addEventListener("change", function () {
    //    const selectedOption = myComboBox.options[myComboBox.selectedIndex].text;
    //    return selectedOption;
    //});
    return myComboBox.options[myComboBox.selectedIndex].text;
}

// get search text
const search_input = document.getElementById("search-input");

function getSearchText() {
    //myComboBox.addEventListener("change", function () {
    //    const selectedOption = myComboBox.options[myComboBox.selectedIndex].text;
    //    return selectedOption;
    //});
    return search_input.value;
}

// responsive header
// When the user scrolls down 500px from the top of the document, show the button
let scrollup = document.getElementById("scrollUp");
window.onscroll = function () { scrollFunction() };
function scrollFunction() {
    if (document.body.scrollTop > 500 || document.documentElement.scrollTop > 500) { scrollup.style.display = "block"; }
    else { scrollup.style.display = "block"; }
}
// When the user clicks on the button, scroll to the top of the document
scrollup.onclick = function () { topFunction() };
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
// scroll up
function myFunction() {
    var x = document.getElementById("Mynavbar");
    if (x.className === "Duynavbar") {
        x.className += " responsive";
        //x.style.display = "block";
    } else {
        x.className = "Duynavbar";
    }
}
