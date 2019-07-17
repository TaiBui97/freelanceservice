
//em nghĩ nó k nhận đc cái @
$(function () {
    // Reference the auto-generated proxy for the hub.


    //// Create a function that the hub can call back to display messages.
    //chat.client.addChatMessage = function (who, message) {
          
    //    var expression = /[-a-zA-Z0-9@@:%_\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@@:%_\+.~#?&//=]*)?/gi;
    //    var regex = new RegExp(expression);
    //    if (message.match(regex)) {
    //        console.log('is link');
    //        $('#discussion').append('<li><strong>' + htmlEncode(who) + '</strong>: ' + '<a href=' + message + '>'
    //            + htmlEncode(message) + '</a> ' + '</li>');
    //    } else {
    //        $('#discussion').append('<li><strong>' + htmlEncode(who)
    //            + '</strong>: ' + htmlEncode(message) + '</li>');
    //    }
    //   //console.log(document.querySelector("#noti").innerHTML = "có tin nhắn hoặc có người mua sản phẩm của bạn!");
    //}

});
$(document).ready(function () {
    var i;
    var x = document.getElementsByClassName("filer-style");
    var y = document.getElementsByClassName("filter-radio");
    var z = document.getElementsByClassName("filer-include");
    var c = document.getElementsByClassName("fixed-items-category");
    for (i = 0; i < x.length; i++) {
        if (i > 2) {
            x[i].style.display = "none";
        }
    }
    for (i = 0; i < y.length; i++) {
        if (i > 2) {
            y[i].style.display = "none";
        }
    }
    for (i = 0; i < z.length; i++) {
        if (i > 2) {
            z[i].style.display = "none";
        }
    }
    for (i = 0; i < c.length; i++) {
        if (i > 4) {
            c[i].style.display = "none";
        }
    }
});

function gigincluded4() {
    var x = document.getElementsByClassName("fixed-items-category");
    var i;
    for (i = 0; i <= x.length; i++) {
        if (x[i].style.display === "none") {
            x[i].style.display = "block";
            document.getElementById("btn-category").innerHTML = "Thu gọn (-)";
        } else if (x[5].style.display === "block") {
            for (i = 5; i < x.length; i++) {
                x[i].style.display = "none";
                document.getElementById("btn-category").innerHTML = "Xem thêm (+)";
            }
        }
    }
};
function gigincluded() {
    var x = document.getElementsByClassName("filter-radio");
    var i;
    for (i = 0; i <= x.length; i++) {
        if (x[i].style.display === "none") {
            x[i].style.display = "block";
            document.getElementById("btn-radio-add").innerHTML = "Thu gọn (-)";
        } else if (x[3].style.display === "block") {
            for (i = 3; i < x.length; i++) {
                x[i].style.display = "none";
                document.getElementById("btn-radio-add").innerHTML = "Xem thêm (+)";
            }
        }
    }
};
function gigincluded2() {
    var x = document.getElementsByClassName("filer-style");

    var i;
    for (i = 3; i <= x.length; i++) {
        if (x[i].style.display === "none") {
            x[i].style.display = "block";
            document.getElementById("check-btn").innerHTML = "Thu gọn (-)";
        } else if (x[3].style.display === "block") {
            for (i = 3; i < x.length; i++) {
                x[i].style.display = "none";
                document.getElementById("check-btn").innerHTML = "Xem thêm (+)";
            }
        }
    }
};
function gigincluded3() {
    var x = document.getElementsByClassName("filer-include");
    var i;
    for (i = 0; i <= x.length; i++) {
        if (x[i].style.display === "none") {
            x[i].style.display = "block";
            document.getElementById("include-btn").innerHTML = "Thu gọn (-)";
        } else if (x[3].style.display === "block") {
            for (i = 3; i < x.length; i++) {
                x[i].style.display = "none";
                document.getElementById("include-btn").innerHTML = "Xem thêm (+)";
            }
        }
    }
};
$('#number,#numberTwo').on('input', function (e) {
    $(this).val(formatCurrency(this.value.replace(/[,]/g, '')));
}).on('keypress', function (e) {
    if (!$.isNumeric(String.fromCharCode(e.which))) e.preventDefault();
}).on('paste', function (e) {
    var cb = e.originalEvent.clipboardData || window.clipboardData;
    if (!$.isNumeric(cb.getData('text'))) e.preventDefault();
});
function formatCurrency(number) {
    var n = number.split('').reverse().join("");
    var n2 = n.replace(/\d\d\d(?!$)/g, "$&,");
    return n2.split('').reverse().join('');
}

//$(document).ready(function () {
//    var s;
//    var $inputs = $('.namess > input');
//    var ids = {};
//    $inputs.each(function (index) {
//        // For debugging purposes...
//        // alert(index + ': ' + $(this).attr('id'));

//        ids[$(this).attr('name')] = $(this).attr('id');
//    });

//    ids = document.getElementById("counter").value;
//    ids = s.replace(/(^\s*)|(\s*$)/gi, "");
//    ids = s.replace(/[ ]{2,}/gi, " ");
//    ids = s.replace(/\n /, "\n");

//    var a = ids.split(' ').length;
//    console.log(a);
//});

$(function () {
    $('.item>img').on('click', function () {
        $('.enlargeImageModalSource').attr('src', $(this).attr('src'));
        $('#enlargeImageModal').modal('show');
    });
});
$('#exampleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var recipient = button.data('whatever') // Extract info from data-* attributes

    var modal = $(this)
    modal.find('.modal-title').text('New message to ' + recipient)
    modal.find('.modal-body input').val(recipient)
});

    //});
    //$('.check-styles').on('change', function () {
    //    var values = [];
    //    $('.check-styles:checked').each(function () {
    //        var result = $(this).val();
    //        values.push(result);
    //        console.log(result);
    //    });
    //});
    //$('.auto-submit').click(function () {
    //    var ss = document.getElementById("myInput").value;
    //    console.log(ss);
    //});
    //$('.check-sv').on('change', function () {
    //    var values = [];
    //    $('.check-sv:checked').each(function () {
    //        var result = $(this).val();
    //        values.push(result);
    //        console.log(result);
    //    });
 
    //$('.filter-range').click(function () {
    //    var n = document.getElementById("number").value;
    //    var m = document.getElementById("numberTwo").value;
    //    var res = n.replace(/,/g, '');
    //    var resT = m.replace(/,/g, '');
    //    console.log(res);
    //    console.log(resT);
    //});
