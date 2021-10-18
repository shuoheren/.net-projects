// question 1

let salaries = {
    John: 100,
    Ann: 160,
    Pete: 130
}

let sum = salaries.John + salaries.Ann + salaries.Pete;
console.log(sum);

// question 2


let menu = {
    width: 200,
    height: 300,
    title: "My menu"
};

function multiplyNumeric(menu) {
    menu.width = menu.width* 2;
    menu.height = menu.height * 2;
}

multiplyNumeric(menu);

console.log(menu);


// question 3

function checkEmailId(str) {
    let a = str.indexOf('@');
    let b = str.indexOf('.');
    if (a <= - 1){
    return false;
    }
    if (b <= - 1){
    return false;
    }
    if (a+1>b){
    return false;
    }
    return true;
}

console.log(checkEmailId("billgates@microsoft.com"));


// question 4

function truncate(str, maxlength) {
    let res = "";
    for (var i = 0; i < maxlength - 1; i++) {
        res += str[i];
    }
    res += str.length > maxlength ? "…"   :     str[maxlength - 1];
    return res;
}

//question 5
let arr = ["James", "Brennie"];
arr.push("Robert");
arr.splice(arr.length / 2, 1, "Calvin");
console.log(arr.splice(0, 1));
arr.splice(0, 0, "Rose", "Regal");
console.log(arr);


