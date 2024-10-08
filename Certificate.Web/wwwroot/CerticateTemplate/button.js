
    // İndir butonunu hover durumunda ikon değiştirme
    document.querySelectorAll('.download-buttons button').forEach(function (button, index) {
        button.addEventListener('mouseover', function () {
            let imgElement = button.querySelector('img');
            switch (index) {
                case 0:
                    imgElement.src = '/CerticateTemplate/Images/indirhover.png'; // İlk buton için hover ikon
                    break;
                case 1:
                    imgElement.src = '/CerticateTemplate/Images/görselhover.png'; // Görsel butonu hover
                    break;
                case 2:
                    imgElement.src = '/CerticateTemplate/Images/pdfhover.png'; // PDF butonu hover
                    break;
            }
        });

    button.addEventListener('mouseout', function () {
        let imgElement = button.querySelector('img');
    switch (index) {
                case 0:
    imgElement.src = '/CerticateTemplate/Images/indir.png'; // İlk buton normal ikon
    break;
    case 1:
    imgElement.src = '/CerticateTemplate/Images/görsel.png'; // Görsel butonu normal
    break;
    case 2:
    imgElement.src = '/CerticateTemplate/Images/pdf.png'; // PDF butonu normal
    break;
            }
        });
    });