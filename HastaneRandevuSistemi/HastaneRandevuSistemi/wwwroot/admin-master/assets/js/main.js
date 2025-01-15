<!-- JavaScript -->
// Sayfa yüklendiğinde alanları getir
$(document).ready(function () {
    $.ajax({
        url: '/Hasta/GetAlanlar', // Alanları getiren endpoint
        type: 'GET',
        success: function (data) { // JSON başarılı şekilde dönerse
            var alanDropdown = $("#alanDropdown"); // Dropdown'ı seç
            alanDropdown.empty(); // Var olan seçenekleri temizle
            alanDropdown.append('<option value="">Alan Seçiniz</option>'); // Varsayılan seçeneği ekle
            $.each(data, function (index, alan) { // Gelen JSON listesini döngüyle işle
                alanDropdown.append(`<option value="${alan}">${alan}</option>`); // Seçenek olarak ekle
            });
        },
        error: function () { // AJAX çağrısı başarısız olursa
            alert("Alanlar yüklenirken bir hata oluştu.");
        }
    });
});

// Alan seçildiğinde ilgili doktorları getir
$("#alanDropdown").change(function () {
    var selectedAlan = $(this).val();
    var doktorDropdown = $("#doktorDropdown");

    if (selectedAlan) {
        $.ajax({
            url: '/Hasta/GetDoktorlar',
            type: 'GET',
            data: {alan: selectedAlan},
            success: function (data) {
                doktorDropdown.empty();
                doktorDropdown.append('<option value="">Doktor Seçiniz</option>');
                $.each(data, function (index, doktor) {
                    doktorDropdown.append(`<option value="${doktor.id}">${doktor.userName}</option>`);
                });
                doktorDropdown.prop("disabled", false);
            },
            error: function () {
                alert("Doktorlar yüklenirken bir hata oluştu.");
            }
        });
    } else {
        doktorDropdown.empty();
        doktorDropdown.append('<option value="">Önce bir alan seçiniz</option>');
        doktorDropdown.prop("disabled", true);
    }
});

// Form gönderildiğinde randevuyu kaydet
$("#randevuForm").submit(function (e) {
    e.preventDefault();

    var randevuData = {
        DoktorId: $("#doktorDropdown").val(),
        RandevuTarihi: $("#randevuTarihi").val()
    };
    console.log("Gönderilen Veri:", randevuData);
    if (!randevuData.DoktorId || !randevuData.RandevuTarihi) {
        alert("Lütfen tüm alanları doldurun.");
        return;
    }

    $.ajax({
        url: '/Hasta/RandevuEkle',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(randevuData),
        success: function (response) {
            // Başarı mesajı ve başlangıçta boş progress bar ekle
            $("#message").html(`
                <div class="alert alert-success">${response}</div>
                <div class="progress mt-2">
                    <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 0%; transition: width 3s;" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
            `);

            // Progress bar'ı 3 saniyede doldur
            setTimeout(function () {
                $(".progress-bar").css("width", "100%");
            }, 100); // Biraz gecikme ekliyoruz ki animasyon düzgün çalışsın.

            // 3 saniye sonra mesajı kaldır
            setTimeout(function () {
                $("#message").html('');
            }, 3100); // 3 saniye + küçük gecikme
        },
        error: function (error) {
            // Hata mesajı ve başlangıçta boş progress bar ekle
            $("#message").html(`
                <div class="alert alert-danger">${error.responseText}</div>
                <div class="progress mt-2">
                    <div class="progress-bar bg-danger progress-bar-striped progress-bar-animated" role="progressbar" style="width: 0%; transition: width 3s;" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
            `);

            // Progress bar'ı 3 saniyede doldur
            setTimeout(function () {
                $(".progress-bar").css("width", "100%");
            }, 100);

            // 3 saniye sonra mesajı kaldır
            setTimeout(function () {
                $("#message").html('');
            }, 3100);
        }
    });
});


$.noConflict();

jQuery(document).ready(function ($) {

    "use strict";

    [].slice.call(document.querySelectorAll('select.cs-select')).forEach(function (el) {
        new SelectFx(el);
    });

    jQuery('.selectpicker').selectpicker;


    $('.search-trigger').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $('.search-trigger').parent('.header-left').addClass('open');
    });

    $('.search-close').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $('.search-trigger').parent('.header-left').removeClass('open');
    });

    $('.equal-height').matchHeight({
        property: 'max-height'
    });

    // var chartsheight = $('.flotRealtime2').height();
    // $('.traffic-chart').css('height', chartsheight-122);


    // Counter Number
    $('.count').each(function () {
        $(this).prop('Counter', 0).animate({
            Counter: $(this).text()
        }, {
            duration: 3000,
            easing: 'swing',
            step: function (now) {
                $(this).text(Math.ceil(now));
            }
        });
    });


    // Menu Trigger
    $('#menuToggle').on('click', function (event) {
        var windowWidth = $(window).width();
        if (windowWidth < 1010) {
            $('body').removeClass('open');
            if (windowWidth < 760) {
                $('#left-panel').slideToggle();
            } else {
                $('#left-panel').toggleClass('open-menu');
            }
        } else {
            $('body').toggleClass('open');
            $('#left-panel').removeClass('open-menu');
        }

    });


    $(".menu-item-has-children.dropdown").each(function () {
        $(this).on('click', function () {
            var $temp_text = $(this).children('.dropdown-toggle').html();
            $(this).children('.sub-menu').prepend('<li class="subtitle">' + $temp_text + '</li>');
        });
    });


    // Load Resize 
    $(window).on("load resize", function (event) {
        var windowWidth = $(window).width();
        if (windowWidth < 1010) {
            $('body').addClass('small-device');
        } else {
            $('body').removeClass('small-device');
        }

    });


});

