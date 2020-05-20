//  פונקציות וילידציה לטופס

function checkCodeStart() {
    $("#startP").hide();
    var dateString = document.getElementById('start').value;
    var myDate = new Date(dateString);
    var nextDate = new Date(document.getElementById('end').value)
    var today = new Date();
    if (myDate <= today) {
        $('#start').after('<p class="pVali" id="startP">You cannot enter a date that was in the past!</p>');
        document.getElementById('start').value = "";
        return false;
    }
    else if (document.getElementById('end').value != "") {
        if (myDate > nextDate) {
            $('#start').after('<p class="pVali" id="startP">You cannot enter a date that is later then the end date!</p>');
            document.getElementById('start').value = "";
            return false;
        }
        return true;
    }
}
function checkCodeEnd() {
    $("#endP").hide();
    var dateString = document.getElementById('end').value;
    var myDate2 = new Date(dateString);
    var myDate1 = new Date(document.getElementById('start').value)
    var today2 = new Date();
    if (myDate2 < today2) {
        $('#end').after('<p class="pVali" id="endP">You cannot enter a date that was in the past!</p>');
        document.getElementById('start').value = "";
        return false;

    }
    else if (myDate2 < myDate1) {
        $('#end').after('<p class="pVali" id="endP">You cannot enter a date before start date!</p>');
        document.getElementById('end').value = "";
        return false;
    }
    return true;
}
