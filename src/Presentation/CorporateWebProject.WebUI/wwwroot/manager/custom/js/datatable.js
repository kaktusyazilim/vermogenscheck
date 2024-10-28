$(document).ready(function () {

    jQuery('.numbersOnly').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });

    $(".myTable").DataTable({
        //"language": {
        //    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json",
        //    buttons: {
        //        print: "Yazdır",
        //        copy: "Kopyala",
        //        pdf: "PDF",
        //        excel: "Excel",
        //        csv: "Excel",
        //        colvis:"Kolon Gizle"
        //    }
        //},
        "language": {
            "sProcessing": "İşleniyor...",
            "sLengthMenu": "Sayfada _MENU_ Kayıt Göster",
            "sZeroRecords": "Eşleşen Kayıt Bulunmadı",
            "sInfo": "  _TOTAL_ Kayıttan _START_ - _END_ Arası Kayıtlar",
            "sInfoEmpty": "Kayıt Yok",
            "sInfoFiltered": "( _MAX_ Kayıt İçerisinden Bulunan)",
            "sInfoPostFix": "",
            "sSearch": "Bul:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "İlk",
                "sPrevious": "Önceki",
                "sNext": "Sonraki",
                "sLast": "Son"
            },

            buttons: {
                print: "Yazdır",
                copy: "Kopyala",
                pdf: "PDF",
                excel: "Excel",
                csv: "Excel",
                colvis: "Kolon Gizle"
            }

        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, ':visible']
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'
        ],
        autoWidth: false,
        ordering: true,
        pageLength: 50,
        paging: true,
        search: true,
        initComplete: function () {
            count = 0;
            this.api().columns().every(function () {
                var title = this.header();
                //replace spaces with dashes
                title = $(title).html().replace(/[\W]/g, '-');
                var column = this;
              

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>');
                });

                //use column title as selector and placeholder
                $('#' + title).select2({
                    multiple: true,
                    closeOnSelect: false,
                    placeholder: " ",
                });

                //initially clear select otherwise first option is selected
                $('.select2').val(null).trigger('change');
            });
        }

    });


    $(document).on("click tap", ".btnDelete", function myfunction() {
        var itemid = $(this).data("itemid");
        var url = $(this).data("url");
        Swal.fire({
            title: 'Silmek istediğine emin misin?',
            text: "Bu işlemi geri alamazsınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!',
            confirmButtonClass: 'btn btn-warning',
            cancelButtonClass: 'btn btn-danger ml-1',
            cancelButtonText:
                'Vazgeç',
            buttonsStyling: false,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    url: url,
                    data: { itemid: itemid },
                    success: function () {
                        Swal.fire({
                            icon: "success",
                            title: 'BAŞARILI!',
                            text: 'İşleminiz başarıyla tamamlandı.',
                            confirmButtonClass: 'btn btn-success',
                        }).then((finishResult) => {
                            if (finishResult.value) {
                                window.location.reload();
                            }
                        });

                    },
                    error: function () {
                        Swal.fire({
                            icon: "error",
                            title: 'Başarısız!',
                            text: 'Sunucu kaynaklı bir hata meydana geldi.',
                            confirmButtonClass: 'btn btn-danger',
                        });
                    }
                })

            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire({
                    title: 'Vazgeçildi',
                    text: 'Veriniz güvende',
                    icon: 'error',
                    confirmButtonClass: 'btn btn-success',
                })
            }
        });
    })


    $(document).on("click tap", ".btnRemove", function myfunction() {
        var itemid = $(this).data("itemid");
        var url = $(this).data("url");
        var btn = $(this);
        Swal.fire({
            title: 'Silmek istediğine emin misin?',
            text: "Bu işlemi geri alamazsınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!',
            confirmButtonClass: 'btn btn-warning',
            cancelButtonClass: 'btn btn-danger ml-1',
            cancelButtonText:
                'Vazgeç',
            buttonsStyling: false,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    url: url,
                    method: 'POST',
                    data: { itemid: itemid },
                    success: function () {
                        btn.parent().parent().remove();
                    },
                    error: function () {
                        Swal.fire({
                            icon: "error",
                            title: 'Başarısız!',
                            text: 'Sunucu kaynaklı bir hata meydana geldi.',
                            confirmButtonClass: 'btn btn-danger',
                        });
                    }
                })

            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire({
                    title: 'Vazgeçildi',
                    text: 'Veriniz güvende',
                    icon: 'error',
                    confirmButtonClass: 'btn btn-success',
                })
            }
        });
    })


    $(document).on("click tap", ".btnPassive", function myfunction() {
        var itemid = $(this).data("itemid");
        var url = $(this).data("url");
        Swal.fire({
            title: 'Devam etmek istediğine emin misin?',
            text: "Bu işlemi geri alamazsınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet',
            confirmButtonClass: 'btn btn-warning',
            cancelButtonClass: 'btn btn-danger ml-1',
            cancelButtonText:
                'Vazgeç',
            buttonsStyling: false,
        }).then(function (result) {
            if (result.value) {
                $.ajax({
                    url: url,
                    data: { itemid: itemid },
                    success: function () {
                        Swal.fire({
                            icon: "success",
                            title: 'BAŞARILI!',
                            text: 'İşleminiz başarıyla tamamlandı.',
                            confirmButtonClass: 'btn btn-success',
                        }).then((finishResult) => {
                            if (finishResult.value) {
                                window.location.reload();
                            }
                        });

                    },
                    error: function () {
                        Swal.fire({
                            icon: "error",
                            title: 'Başarısız!',
                            text: 'Sunucu kaynaklı bir hata meydana geldi.',
                            confirmButtonClass: 'btn btn-danger',
                        });
                    }
                })

            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire({
                    title: 'Vazgeçildi',
                    text: 'Veriniz güvende',
                    icon: 'error',
                    confirmButtonClass: 'btn btn-success',
                })
            }
        });
    })


    $(document).on("click tap", ".btnFSubmit", function myfunction() {
        var formName = $(this).data("form");
        Swal.fire({
            title: 'Bilgilendirme',
            text: "Devam etmek istediğinize emin misiniz?",

            icon: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet',
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger ml-1',
            cancelButtonText:
                'Vazgeç',
            buttonsStyling: false,
        }).then((result) => {
            if (result.value) {
                setTimeout(function () {

                    $("#" + formName).submit();
                    var validate = $("#" + formName + " .field-validation-error");
                    if (validate.length > 0) {
                        Swal.fire({
                            title: 'İşlem başarısız!',
                            text: 'İşleminiz başarısız oldu lütfen bütün alanları kontrol ettikten sonra tekrar deneyinizı.',
                            icon: 'warning',
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Tamam',
                        }).then((finishResult) => {
                            if (finishResult.value) {

                            }
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Başarılı!',
                            text: 'İşleminiz başarıyla tamamlandı.',
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Tamam',
                        }).then((finishResult) => {
                            if (finishResult.value) {
                                window.history.back();

                            }
                        });
                    }
                }, 500);



            }

        })
    })

})