"use strict"

function encryptText() {
    var input = document.getElementById('entertext');
    var encryptedResult = document.getElementById('encryptedResult');
    var decryptedResult = document.getElementById('decryptedResult');
    var save = document.getElementById('save');
    if (isEmpty(input.value)) {
        alert('Error! Please input the field and try again.');
        return false;
    }
    let formData = new FormData();
    formData.append('EnteredText', input.value);
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Home/Encrypt");
    xhr.send(formData);
    xhr.onload = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                var res = JSON.parse(xhr.response);
                encryptedResult.value = res[0];
                decryptedResult.value = res[1];
                save.removeAttribute("disabled");
            }
        }
    }
}

function isEmpty(str) {
    return (!str || str.length === 0);
}