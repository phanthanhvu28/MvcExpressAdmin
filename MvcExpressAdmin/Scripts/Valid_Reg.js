function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}
function ValidateDate(dtValue) {
    var dtRegex = new RegExp(/\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/);
    return dtRegex.test(dtValue);
}


function validNumber(sNumber) {
    var filter = /[0-9]$/;
    if (filter.test(sNumber)) {
        return true;
    }
    else {
        return false;
    }
}

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

function make_friendly_link(s) {
    if (typeof s == "undefined") {
        return;
    }

    var i = 0, uni1, arr1;
    var newclean = s;
    uni1 = 'à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ|A';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'a');

    uni1 = 'è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ|E';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'e');

    uni1 = 'ì|í|ị|ỉ|ĩ|Ì|Í|Ị|Ỉ|Ĩ|I';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'i');

    uni1 = 'ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ|O';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'o');

    uni1 = 'ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ|U';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'u');

    uni1 = 'ỳ|ý|ỵ|ỷ|ỹ|Ỳ|Ý|Ỵ|Ỷ|Ỹ|Y';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'y');

    uni1 = 'd|Đ|D';
    arr1 = uni1.split('|');
    for (i = 0; i < uni1.length; i++) newclean = newclean.replace(uni1[i], 'd');

    newclean = newclean.toLowerCase()
    ret = newclean.replace(/[\&]/g, '-and-').replace(/[^a-zA-Z0-9._-]/g, '-').replace(/[-]+/g, '-').replace(/-$/, '');

    return ret;
}
