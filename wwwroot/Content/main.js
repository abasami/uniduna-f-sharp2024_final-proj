var selecterBox = document.getElementById('sample');
var selecter = document.getElementById('changeSelect');
var item1 = document.getElementById('Box');
var item2 = document.getElementById('Box2');
var item3 = document.getElementById('Box3');
var item4 = document.getElementById('Box4');
var item5 = document.getElementById('Box5');
selecterBox.style.display = "none";
item1.style.display = "none";
item2.style.display = "none";
item3.style.display = "none";
item4.style.display = "none";
item5.style.display = "none";

    function formSwitch() {
        check = document.getElementsByClassName('js-check')
        if (check[0].checked) {
            selecterBox.style.display = "none";
            item1.style.display = "none";
            item2.style.display = "none";
            item3.style.display = "none";
            item4.style.display = "none";
            item5.style.display = "none";
            selecter.selectedIndex = 0;

        } else if (check[1].checked) {
            selecterBox.style.display = "block";

        } else {
            selecterBox.style.display = "none";
        }
    }
    window.addEventListener('load', formSwitch());

function entryChange2(){
    if(document.getElementById('changeSelect')){
    id = document.getElementById('changeSelect').value;

    if(id == 'select2'){
        item1.style.display = "block";
        item2.style.display = "none";
        item3.style.display = "none";
        item4.style.display = "none";
        item5.style.display = "none";
    }
    else if(id == 'select3'){
        item1.style.display = "none";
        item2.style.display = "block";
        item3.style.display = "none";
        item4.style.display = "none";
        item5.style.display = "none";
    }
    else if(id == 'select4'){
        item1.style.display = "none";
        item2.style.display = "none";
        item3.style.display = "block";
        item4.style.display = "none";
        item5.style.display = "none";
    }
    else if(id == 'select5'){
        item1.style.display = "none";
        item2.style.display = "none";
        item3.style.display = "none";
        item4.style.display = "block";
        item5.style.display = "none";
    }
     else if(id == 'select6'){
        item1.style.display = "none";
        item2.style.display = "none";
        item3.style.display = "none";
        item4.style.display = "none";
        item5.style.display = "block";
    }
    else{
        item1.style.display = "none";
        item2.style.display = "none";
        item3.style.display = "none";
        item4.style.display = "none";
        item5.style.display = "none";
    }
}
}