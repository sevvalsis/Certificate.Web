$(document).ready(() => {
    /*------- button with class register -------*/
    let reg_btn = $('.container .register');

    /*------- button with class sign in --------*/
    let sig_btn = $('.container .signin');

    /*------- back button ----------------------*/
    let back_btn = $('.container .back');

    // Register button click handler
    reg_btn.click(function (e) {
        e.preventDefault();
        $(this).siblings('.reg').css({
            'transform': 'translateY(40%) scale(5)',
            'border-radius': '0',
            'width': '100%',
            'height': '100%'
        });

        reg_btn.siblings('h3:nth-of-type(1)').css({
            'top': '40%',
            'z-index': '8'
        });
    });

    // Sign in button click handler
    sig_btn.on('click', function (e) {
        e.preventDefault();
        $(this).siblings('.sig').css({
            'transform': 'translateY(40%) scale(5)',
            'border-radius': '0',
            'width': '100%',
            'height': '100%'
        });

        sig_btn.siblings('h3:nth-of-type(2)').css({
            'top': '40%',
            'z-index': '8'
        });
    });
});



// Pop-up kapatma iþlevi
function closePopup() {
    document.getElementById("error-popup").style.display = "none";
}


// Ýndir butonuna basýldýðýnda indirme seçeneklerini göster
function showDownloadOptions() {
    var downloadOptions = document.getElementById("download-options");
    if (downloadOptions.style.display === "none") {
        downloadOptions.style.display = "block"; // Butonlarý göster
    } else {
        downloadOptions.style.display = "none"; // Butonlarý gizle
    }
}



    //// Ýndir butonunu hover durumunda ikon deðiþtirme
    //document.querySelectorAll('.download-buttons button').forEach(function (button, index) {
    //    button.addEventListener('mouseover', function () {
    //        let imgElement = button.querySelector('img');
    //        switch (index) {
    //            case 0:
    //                imgElement.src = '/CerticateTemplate/Images/indirhover.png'; // Ýlk buton için hover ikon
    //                break;
    //            case 1:
    //                imgElement.src = '/CerticateTemplate/Images/görselhover.png'; // Görsel butonu hover
    //                break;
    //            case 2:
    //                imgElement.src = '/CerticateTemplate/Images/pdfhover.png'; // PDF butonu hover
    //                break;
    //        }
    //    });

    //button.addEventListener('mouseout', function () {
    //    let imgElement = button.querySelector('img');
    //switch (index) {
    //            case 0:
    //imgElement.src = '/CerticateTemplate/Images/indir.png'; // Ýlk buton normal ikon
    //break;
    //case 1:
    //imgElement.src = '/CerticateTemplate/Images/görsel.png'; // Görsel butonu normal
    //break;
    //case 2:
    //imgElement.src = '/CerticateTemplate/Images/pdf.png'; // PDF butonu normal
    //break;
    //        }
    //    });
    //});

